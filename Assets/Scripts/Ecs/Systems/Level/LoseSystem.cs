using LeoEcsGui;
using Leopotam.EcsLite;
using UnityEngine;

public class LoseSystem : IEcsInitSystem, IEcsRunSystem
{
    private Player _player;
    private PlayerCubeStack _playerStack;
    private LoseScreen _loseScreen;
    private PlayerPrefsConfig _playerPrefsConfig;

    public LoseSystem(Player player, LoseScreen loseScreen, PlayerPrefsConfig playerPrefsConfig)
    {
        _player = player;
        _loseScreen = loseScreen;
        _playerPrefsConfig = playerPrefsConfig;
    }

    public void Init(EcsSystems systems)
    {
        _loseScreen.gameObject.SetActive(false);
        _playerStack = _player.GetComponent<PlayerCubeStack>();
    }

    public void Run(EcsSystems systems)
    {
        ProcessLoseChecking(systems);
        ProcessRestartButtonClicking(systems);
    }

    private void ProcessLoseChecking(EcsSystems systems)
    {
        EcsWorld world = systems.GetWorld();
        EcsFilter finishedFilter = world.Filter<HasFinishedSignal>().End();
        if (_playerStack.Stack.Count == 0 && finishedFilter.GetEntitiesCount() == 0)
        {
            _loseScreen.gameObject.SetActive(true);
            int stopPlayerEntity = world.NewEntity();
            EcsPool<StopPlayerSignal> stopPlayerPool = world.GetPool<StopPlayerSignal>();
            stopPlayerPool.Add(stopPlayerEntity);
        }
    }

    private void ProcessRestartButtonClicking(EcsSystems systems)
    {
        EcsWorld world = systems.GetWorld();
        EcsPool<OnButtonClickedEvent> clickedPool = world.GetPool<OnButtonClickedEvent>();
        EcsFilter clickedFilter = world.Filter<OnButtonClickedEvent>().End();
        foreach (int entity in clickedFilter)
        {
            ref OnButtonClickedEvent clickedEvent = ref clickedPool.Get(entity);
            RestartButton restartButton = clickedEvent.Sender.GetComponent<RestartButton>();
            if (restartButton)
            {
                int loadLevelEntity = world.NewEntity();
                EcsPool<LoadLevelSignal> loadLevelPool = world.GetPool<LoadLevelSignal>();
                ref LoadLevelSignal signal = ref loadLevelPool.Add(loadLevelEntity);
                int currentLevel = PlayerPrefs.GetInt(_playerPrefsConfig.LevelNumberPrefsName);
                signal.LevelNumber = currentLevel;
            }
        }
    }
}
