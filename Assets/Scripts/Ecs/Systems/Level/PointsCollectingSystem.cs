using Leopotam.EcsLite;
using UnityEngine;

public class PointsCollectingSystem : IEcsInitSystem, IEcsRunSystem, IEcsDestroySystem
{
    private LevelsConfig _levelsConfig;
    private PlayerPrefsConfig _prefsConfig;

    public PointsCollectingSystem(LevelsConfig levelsConfig, PlayerPrefsConfig prefsConfig)
    {
        _levelsConfig = levelsConfig;
        _prefsConfig = prefsConfig;
    }

    public void Init(EcsSystems systems)
    {
        EcsWorld world = systems.GetWorld();
        int pointsEntity = world.NewEntity();
        EcsPool<PointsComponent> pointsPool = world.GetPool<PointsComponent>();
        ref PointsComponent pointsComponent = ref pointsPool.Add(pointsEntity);
        int currentLevelNumber = PlayerPrefs.GetInt(_prefsConfig.LevelNumberPrefsName);
        pointsComponent.Points = _levelsConfig.GetPointsForLevel(currentLevelNumber);
    }

    public void Run(EcsSystems systems)
    {
        EcsWorld world = systems.GetWorld();
        EcsPool<MultiplyPointsSignal> signalPool = world.GetPool<MultiplyPointsSignal>();
        EcsFilter signalFilter = world.Filter<MultiplyPointsSignal>().End();
        EcsPool<PointsComponent> pointsPool = world.GetPool<PointsComponent>();
        EcsFilter pointsFilter = world.Filter<PointsComponent>().End();
        foreach (int signalEntity in signalFilter)
        {
            ref MultiplyPointsSignal signal = ref signalPool.Get(signalEntity);
            foreach (int pointsEntity in pointsFilter)
            {
                ref PointsComponent pointsComponent = ref pointsPool.Get(pointsEntity);
                int newPoints = (int)(pointsComponent.Points * signal.Multiplyer);
                pointsComponent.Points = newPoints;
            }
        }
    }

    public void Destroy(EcsSystems systems)
    {
        int allPointsNum = PlayerPrefs.GetInt(_prefsConfig.PointsNumberPrefsName);
        EcsWorld world = systems.GetWorld();
        EcsPool<PointsComponent> pointsPool = world.GetPool<PointsComponent>();
        EcsFilter pointsFilter = world.Filter<PointsComponent>().End();
        foreach (int entity in pointsFilter)
        {
            ref PointsComponent pointsComponent = ref pointsPool.Get(entity);
            int points = pointsComponent.Points;
            PlayerPrefs.SetInt(_prefsConfig.PointsNumberPrefsName, points + allPointsNum);
        }
    }
}
