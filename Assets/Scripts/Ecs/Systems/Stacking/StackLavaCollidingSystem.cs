using Leopotam.EcsLite;
using LeoEcsPhysics;

public class StackLavaCollidingSystem : IEcsRunSystem
{
    public void Run(EcsSystems systems)
    {
        EcsWorld world = systems.GetWorld();
        EcsPool<OnTriggerEnterEvent> eventPool = world.GetPool<OnTriggerEnterEvent>();
        EcsFilter eventFilter = world.Filter<OnTriggerEnterEvent>().End();
        foreach (int entity in eventFilter)
        {
            ref OnTriggerEnterEvent eventComponent = ref eventPool.Get(entity);
            PlayerCube cube = eventComponent.senderGameObject.GetComponent<PlayerCube>();
            Lava lava = eventComponent.collider.GetComponent<Lava>();
            PlayerCubeStack stack = null;
            if (cube)
            {
                stack = cube.PlayerCubeStack;
            }
            if (cube && lava && stack)
            {
                stack.RemoveFromStack(cube);
                cube.gameObject.SetActive(false);
            }
        }
    }
}
