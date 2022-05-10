using UnityEngine;
using Zenject;

public class MainPathInstaller : MonoInstaller
{
    [SerializeField] private MainPlayerPath _mainPlayerPath;

    public override void InstallBindings()
    {
        Container
            .Bind<MainPlayerPath>()
            .FromInstance(_mainPlayerPath)
            .AsSingle();
    }
}