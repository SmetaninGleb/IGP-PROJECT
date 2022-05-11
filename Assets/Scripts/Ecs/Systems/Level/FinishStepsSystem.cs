using Leopotam.EcsLite;
using LeoEcsPhysics;
using System.Collections.Generic;

public class FinishStepsSystem : IEcsRunSystem
{
    public void Run(EcsSystems systems)
    {
        EcsWorld world = systems.GetWorld();
        EcsPool<OnTriggerEnterEvent> eventPool = world.GetPool<OnTriggerEnterEvent>();
        EcsFilter eventFilter = world.Filter<OnTriggerEnterEvent>().End();
        foreach (int entity in eventFilter)
        {
            ref OnTriggerEnterEvent eventComponent = ref eventPool.Get(entity);
            PlayerCubeStack stack = eventComponent.senderGameObject.GetComponent<PlayerCubeStack>();
            FinishStep step = eventComponent.collider.GetComponent<FinishStep>();
            if (stack && step)
            {
                List<PlayerCube> removeCubes = stack.PopFromStack(1);
                foreach (PlayerCube cube in removeCubes)
                {
                    cube.transform.parent = null;
                }
                int multiplyerEntity = world.NewEntity();
                EcsPool<MultiplyPointsSignal> multiplyPool = world.GetPool<MultiplyPointsSignal>();
                ref MultiplyPointsSignal multiplySignal = ref multiplyPool.Add(multiplyerEntity);
                multiplySignal.Multiplyer = step.Multiplyer;
                EcsPool<HasFinishedSignal> finishedPool = world.GetPool<HasFinishedSignal>();
                int finishedEntity = world.NewEntity();
                finishedPool.Add(finishedEntity);
            }
        }
    }
}
