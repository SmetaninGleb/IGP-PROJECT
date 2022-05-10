using UnityEngine;
using Leopotam.EcsLite;

public class PlayerSideMovementSystem : IEcsRunSystem
{
    private PlayerMovementConfig _config;

    public PlayerSideMovementSystem(PlayerMovementConfig config)
    {
        _config = config;
    }

    public void Run(EcsSystems systems)
    {
        EcsWorld world = systems.GetWorld();
        EcsPool<CurveFollowerComponent> pool = world.GetPool<CurveFollowerComponent>();
        EcsFilter curveFollowerFilter = world.Filter<CurveFollowerComponent>().End();
        if (Input.touchCount == 0) return;
        Touch touch = Input.GetTouch(0);
        float movingDistance = touch.deltaPosition.x / Screen.dpi * _config.SideMovingMultiplyer;
        foreach (int entity in curveFollowerFilter)
        {
            ref CurveFollowerComponent curveFollowerComponent = ref pool.Get(entity);
            Transform cfTransform = curveFollowerComponent.Follower.transform;
            Vector3 linePoint = curveFollowerComponent.Path.path.GetPointAtDistance(curveFollowerComponent.PathPassedDistance);
            Vector3 strivePos = cfTransform.position + cfTransform.right * movingDistance;
            Vector3 fromLine = strivePos - linePoint;
            Vector3 clampedFromLine = Vector3.ClampMagnitude(fromLine, _config.SideRange);
            cfTransform.position = linePoint + clampedFromLine;
        }
    }
}