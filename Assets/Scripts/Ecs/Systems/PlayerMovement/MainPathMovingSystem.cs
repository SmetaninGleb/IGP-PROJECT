using Leopotam.EcsLite;
using PathCreation;
using UnityEngine;

public class MainPathMovingSystem : IEcsInitSystem, IEcsRunSystem
{
    public void Init(EcsSystems systems)
    {

        EcsWorld world = systems.GetWorld();
        EcsPool<CurveFollowerComponent> followerPool = world.GetPool<CurveFollowerComponent>();
        EcsFilter followerFilter = world.Filter<CurveFollowerComponent>().End();
        foreach (int entity in followerFilter)
        {
            ref CurveFollowerComponent followerComponent = ref followerPool.Get(entity);
            followerComponent.Follower.transform.position = followerComponent.Path.path.GetPoint(0);
            followerComponent.PathPassedDistance = 0f;
        }
    }

    public void Run(EcsSystems systems)
    {
        if (ShouldStop(systems)) return;
        EcsWorld world = systems.GetWorld();
        EcsPool<CurveFollowerComponent> followerPool = world.GetPool<CurveFollowerComponent>();
        EcsFilter followerFilter = world.Filter<CurveFollowerComponent>().End();
        foreach (int entity in followerFilter)
        {
            ref CurveFollowerComponent followerComponent = ref followerPool.Get(entity);
            ProcessFollower(world, ref followerComponent);
        }
    }

    private bool ShouldStop(EcsSystems systems)
    {
        EcsWorld world = systems.GetWorld();
        EcsFilter stopPlayerFilter = world.Filter<StopPlayerSignal>().End();
        return stopPlayerFilter.GetEntitiesCount() != 0;
    }

    private void ProcessFollower(EcsWorld world, ref CurveFollowerComponent followerComponent)
    {
        if (followerComponent.PathPassedDistance >= followerComponent.Path.path.length)
        {
            return;
        }
        float speed = followerComponent.Speed;
        float distanceDelta = speed * Time.deltaTime;
        PathCreator path = followerComponent.Path;
        float newDistance = followerComponent.PathPassedDistance + distanceDelta;
        Transform followerTransform = followerComponent.Follower.transform;
        if (newDistance >= path.path.length)
        {
            followerComponent.PathPassedDistance = path.path.length;
            followerTransform.position = path.path.GetPoint(path.path.NumPoints-1);
            followerTransform.rotation = path.path.GetRotation(1f);
            CreateFinishCurveComponent(world, ref followerComponent);
        }
        Vector3 replacement = distanceDelta * path.path.GetDirectionAtDistance(followerComponent.PathPassedDistance);
        Quaternion newRot = path.path.GetRotationAtDistance(newDistance);
        followerTransform.position += replacement;
        followerTransform.rotation = newRot;
        followerComponent.PathPassedDistance = newDistance;
    }

    private void CreateFinishCurveComponent(EcsWorld world, ref CurveFollowerComponent followerComponent)
    {
        int entity = world.NewEntity();
        EcsPool<FinishCurveSignal> pool = world.GetPool<FinishCurveSignal>();
        ref FinishCurveSignal signal = ref pool.Add(entity);
        signal.Curve = followerComponent.Path;
        signal.Follower = followerComponent.Follower;
    }
}
