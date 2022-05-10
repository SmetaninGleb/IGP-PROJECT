using UnityEngine;
using Zenject;

public class LevelsConfigInstaller : MonoInstaller
{
    [SerializeField] LevelsConfig _levelsConfig;

    public override void InstallBindings()
    {
        Container
            .Bind<LevelsConfig>()
            .FromInstance(_levelsConfig)
            .AsSingle();
    }
}