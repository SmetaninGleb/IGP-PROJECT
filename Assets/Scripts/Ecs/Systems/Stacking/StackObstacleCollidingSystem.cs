using System.Collections.Generic;
using LeoEcsPhysics;
using Leopotam.EcsLite;

public class StackObstacleCollidingSystem : IEcsRunSystem
{
    public void Run(EcsSystems systems)
    {
        EcsWorld world = systems.GetWorld();
        EcsPool<OnTriggerEnterEvent> eventPool = world.GetPool<OnTriggerEnterEvent>();
        EcsFilter eventFilter = world.Filter<OnTriggerEnterEvent>().End();
        foreach (int entity in eventFilter)
        {
            ref OnTriggerEnterEvent onTriggerComponent = ref eventPool.Get(entity);
            PlayerCubeStack stack = onTriggerComponent.senderGameObject.GetComponent<PlayerCubeStack>();
            Obstacle obstacle = onTriggerComponent.collider.GetComponent<Obstacle>();
            if (stack && obstacle)
            {
                float sumHeight = 0;
                int cubeRemoveNumber = 0;
                List<PlayerCube> stackList = stack.Stack;
                while (sumHeight < obstacle.Height && cubeRemoveNumber < stackList.Count)
                {
                    cubeRemoveNumber++;
                    sumHeight += stackList[stackList.Count - cubeRemoveNumber].Height;
                }
                List<PlayerCube> removedCubes = stack.PopFromStack(cubeRemoveNumber);
                foreach (PlayerCube cube in removedCubes)
                {
                    cube.transform.parent = null;
                }
            }
        }
    }
}
