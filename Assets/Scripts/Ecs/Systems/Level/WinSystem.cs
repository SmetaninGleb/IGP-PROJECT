using LeoEcsGui;
using Leopotam.EcsLite;
using UnityEngine;

public class WinSystem : IEcsInitSystem, IEcsRunSystem
{
    private PlayerPrefsConfig _playerPrefsConfig;
    private WinScreen _winScreen;

    public WinSystem(PlayerPrefsConfig playerPrefsConfig, WinScreen winScreen)
    {
        _playerPrefsConfig = playerPrefsConfig;
        _winScreen = winScreen;
    }

    public void Init(EcsSystems systems)
    {
        _winScreen.gameObject.SetActive(false);
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
            _winScreen.gameObject.SetActive(true);
        }
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
