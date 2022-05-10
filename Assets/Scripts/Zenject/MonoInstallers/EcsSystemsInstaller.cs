using Zenject;

public class EcsSystemsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<MainPathInitializingSystem>()
            .FromNew()
            .AsSingle()
            .NonLazy();

        Container
            .Bind<MainPathMovingSystem>()
            .FromNew()
            .AsSingle()
            .NonLazy();

        Container
            .Bind<PlayerSideMovementSystem>()
            .FromNew()
            .AsSingle()
            .NonLazy();

        Container
            .Bind<CubeStackingSystem>()
            .FromNew()
            .AsSingle()
            .NonLazy();

        Container
            .Bind<StackObstacleCollidingSystem>()
            .FromNew()
            .AsSingle()
            .NonLazy();

        Container
            .Bind<StackLavaCollidingSystem>()
            .FromNew()
            .AsSingle()
            .NonLazy();

        Container
            .Bind<FinishStepsSystem>()
            .FromNew()
            .AsSingle()
            .NonLazy();

        Container
            .Bind<WinSystem>()
            .FromNew()
            .AsSingle()
            .NonLazy();

        Container
            .Bind<LoseSystem>()
            .FromNew()
            .AsSingle()
            .NonLazy();

        Container
            .Bind<LoadLevelSystem>()
            .FromNew()
            .AsSingle()
            .NonLazy();

        Container
            .Bind<PointsCollectingSystem>()
            .FromNew()
            .AsSingle()
            .NonLazy();
    }
}