using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelSystem : IEcsRunSystem
{
    private LevelsConfig _levelsConfig;

    public LoadLevelSystem(LevelsConfig levelsConfig, PlayerPrefsConfig prefsConfig)
    {
        _levelsConfig = levelsConfig;
        if (!PlayerPrefs.HasKey(prefsConfig.LevelNumberPrefsName))
        {
            PlayerPrefs.SetInt(prefsConfig.LevelNumberPrefsName, 1);
        }
    }

    public void Run(EcsSystems systems)
    {
        EcsWorld world = systems.GetWorld();
        EcsPool<LoadLevelSignal> loadSignalPool = world.GetPool<LoadLevelSignal>();
        EcsFilter loadSignalFilter = world.Filter<LoadLevelSignal>().End();
        foreach (int entity in loadSignalFilter)
        {
            int levelNum = loadSignalPool.Get(entity).LevelNumber;
            int levelIndexToLoad = _levelsConfig.GetLevelIndex(levelNum);
            SceneManager.LoadScene(levelIndexToLoad);
            break;
        }
    }
}
