using System.Collections.Generic;
using Leopotam.EcsLite;
using LeoEcsPhysics;
using UnityEngine;

public class CubeStackingSystem : IEcsInitSystem, IEcsRunSystem
{
    private Player _player;

    public CubeStackingSystem(Player player)
    {
        _player = player;
    }

    public void Init(EcsSystems systems)
    {
        EcsWorld world = systems.GetWorld();
        EcsPool<PlayerStackComponent> stackPool = world.GetPool<PlayerStackComponent>();
        int stackEntity = world.NewEntity();
        ref PlayerStackComponent stackComponent = ref stackPool.Add(stackEntity);
        stackComponent.Stack = _player.GetComponent<PlayerCubeStack>();
    }

    public void Run(EcsSystems systems)
    {
        EcsWorld world = systems.GetWorld();
        EcsPool<OnTriggerEnterEvent> onTriggerPool = world.GetPool<OnTriggerEnterEvent>();
        EcsFilter onTriggerFilter = world.Filter<OnTriggerEnterEvent>().End();
        foreach (int entity in onTriggerFilter)
        {
            ref OnTriggerEnterEvent onTriggerComponent = ref onTriggerPool.Get(entity);
            PlayerCubeStack stack = onTriggerComponent.senderGameObject.GetComponent<PlayerCubeStack>();
            OnScenePlayerCubeStack onSceneStack = onTriggerComponent.collider.GetComponent<OnScenePlayerCubeStack>();
            if (stack && onSceneStack)
            {
                stack.AddToStack(onSceneStack.Stack);
                foreach (PlayerCube cube in onSceneStack.Stack)
                {
                    Rigidbody rigidbody = cube.GetComponent<Rigidbody>();
                    if (rigidbody) rigidbody.isKinematic = false;
                }
                onSceneStack.gameObject.SetActive(false);
            }
        }
    }
}