using LeoEcsGui;
using Leopotam.EcsLite;
using UnityEngine;

public class WinSystem : IEcsInitSystem, IEcsRunSystem
{
    private PlayerPrefsConfig _playerPrefsConfig;
    private WinScreen _winScreen;
    private PlayerCubeStack _playerStack;
    private Player _player;

    public WinSystem(Player player, PlayerPrefsConfig playerPrefsConfig, WinScreen winScreen)
    {
        _player = player;
        _playerPrefsConfig = playerPrefsConfig;
        _winScreen = winScreen;
    }

    public void Init(EcsSystems systems)
    {
        _winScreen.gameObject.SetActive(false);
        _playerStack = _player.GetComponent<PlayerCubeStack>();
    }

    public void Run(EcsSystems systems)
    {
        ProcessWinChecking(systems);
        ProcessNextButtonClicking(systems);
    }

    private void ProcessWinChecking(EcsSystems systems)
    {
        EcsWorld world = systems.GetWorld();
        EcsFilter finishCurveFilter = world.Filter<FinishCurveSignal>().End();
        if (finishCurveFilter.GetEntitiesCount() > 0)
        {
            ShowWinScreen(systems);
        }
        EcsFilter finishedFilter = world.Filter<HasFinishedSignal>().End();
        if (_playerStack.Stack.Count == 0 && finishedFilter.GetEntitiesCount() != 0)
        {
            int stopPlayerEntity = world.NewEntity();
            EcsPool<StopPlayerSignal> stopPlayerPool = world.GetPool<StopPlayerSignal>();
            stopPlayerPool.Add(stopPlayerEntity);
            ShowWinScreen(systems);
        }
    }

    private void ShowWinScreen(EcsSystems systems)
    {
        EcsWorld world = systems.GetWorld();
        EcsPool<PointsComponent> pointsPool = world.GetPool<PointsComponent>();
        EcsFilter pointsFilter = world.Filter<PointsComponent>().End();
        int levelPoints = 0;
        foreach (int pointsEntity in pointsFilter)
        {
            ref PointsComponent pointsComponent = ref pointsPool.Get(pointsEntity);
            levelPoints = pointsComponent.Points;
            break;
        }
        _winScreen.SetLevelScore(levelPoints);
        _winScreen.SetAllScore(PlayerPrefs.GetInt(_playerPrefsConfig.PointsNumberPrefsName) + levelPoints);
        _winScreen.gameObject.SetActive(true);
    }

    private void ProcessNextButtonClicking(EcsSystems systems)
    {
        EcsWorld world = systems.GetWorld();
        EcsPool<OnButtonClickedEvent> clickedPool = world.GetPool<OnButtonClickedEvent>();
        EcsFilter clickedFilter = world.Filter<OnButtonClickedEvent>().End();
        foreach (int entity in clickedFilter)
        {
            ref OnButtonClickedEvent clickedComponent = ref clickedPool.Get(entity);
            NextLevelButton nextLevelButton = clickedComponent.Sender.GetComponent<NextLevelButton>();
            if (nextLevelButton)
            {
                int loadLevelEntity = world.NewEntity();
                EcsPool<LoadLevelSignal> loadLevelPool = world.GetPool<LoadLevelSignal>();
                ref LoadLevelSignal loadLevelSignal = ref loadLevelPool.Add(loadLevelEntity);
                int currentLevelNumber = PlayerPrefs.GetInt(_playerPrefsConfig.LevelNumberPrefsName);
                currentLevelNumber++;
                PlayerPrefs.SetInt(_playerPrefsConfig.LevelNumberPrefsName, currentLevelNumber);
                loadLevelSignal.LevelNumber = currentLevelNumber;
            }
        }
    }
}
