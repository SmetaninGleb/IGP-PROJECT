using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;


namespace LeoEcsGui
{
    public static class EcsSystemsExtensions
    {
        public static EcsSystems DelHereGuiEvents(this EcsSystems ecsSystems, string worldName = null)
        {
            ecsSystems.DelHere<OnButtonClickedEvent>(worldName);
            
            return ecsSystems;
        }
    }
}