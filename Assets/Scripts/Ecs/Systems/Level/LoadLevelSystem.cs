using Leopotam.EcsLite;
using UnityEngine.SceneManagement;

public class LoadLevelSystem : IEcsRunSystem
{
    private LevelsConfig _levelsConfig;

    public LoadLevelSystem(LevelsConfig levelsConfig)
    {
        _levelsConfig = levelsConfig;
    }

    public void Run(EcsSystems systems)
    {
        EcsWorld world = systems.GetWorld();
        EcsPool<LoadLevelSignal> loadSignalPool = world.GetPool<LoadLevelSignal>();
        EcsFilter loadSignalFilter = world.Filter<LoadLevelSignal>().End();
        foreach (int entity in loadSignalFilter)
        {
            int levelNum = loadSignalPool.Get(entity).LevelNumber;
            Scene levelToLoad = _levelsConfig.GetLevel(levelNum);
            SceneManager.LoadScene(levelToLoad.buildIndex);
            break;
        }
    }
}
