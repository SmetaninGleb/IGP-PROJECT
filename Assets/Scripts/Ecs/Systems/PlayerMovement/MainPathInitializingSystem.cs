using Leopotam.EcsLite;
using PathCreation;

public class MainPathInitializingSystem : IEcsInitSystem
{
    private MainPlayerPath _mainPlayerPath;
    private Player _player;
    private PlayerMovementConfig _config;

    public MainPathInitializingSystem(MainPlayerPath mainPlayerPath, Player player, PlayerMovementConfig config)
    {
        _mainPlayerPath = mainPlayerPath;
        _player = player;
        _config = config;
    }

    public void Init(EcsSystems systems)
    {
        EcsWorld world = systems.GetWorld();
        CreateMainPathComponent(world);
        CreatePlayerCurveFollower(world);
    }

    private void CreateMainPathComponent(EcsWorld world)
    {
        EcsPool<MainPathComponent> pathPool = world.GetPool<MainPathComponent>();
        int entity = world.NewEntity();
        ref MainPathComponent pathComponent = ref pathPool.Add(entity);
        pathComponent.Path = _mainPlayerPath.GetComponent<PathCreator>();
    }

    private void CreatePlayerCurveFollower(EcsWorld world)
    {
        EcsPool<CurveFollowerComponent> followerPool = world.GetPool<CurveFollowerComponent>();
        int entity = world.NewEntity();
        ref CurveFollowerComponent followerComponent = ref followerPool.Add(entity);
        followerComponent.Follower = _player.GetComponent<CurveFollower>();
        followerComponent.Speed = _config.Speed;
        followerComponent.Path = _mainPlayerPath.GetComponent<PathCreator>();
        followerComponent.PathPassedDistance = 0;
    }
}