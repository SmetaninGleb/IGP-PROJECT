using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.UI;

namespace LeoEcsGui
{
    public static class EcsGuiEvents
    {
        private static EcsWorld _world;

        public static void SetWorld(EcsWorld world)
        {
            _world = world;
        }

        public static void RegisterOnButtonClickedEvent(Button button, GameObject sender)
        {
            int entity = _world.NewEntity();
            EcsPool<OnButtonClickedEvent> pool = _world.GetPool<OnButtonClickedEvent>();
            ref OnButtonClickedEvent clickedEvent = ref pool.Add(entity);
            clickedEvent.Button = button;
            clickedEvent.Sender = sender;
        }
    }
}