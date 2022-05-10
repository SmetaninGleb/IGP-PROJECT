using UnityEngine;
using Zenject;

public class ScreensInstaller : MonoInstaller
{
    [SerializeField] private WinScreen _winScreen;
    [SerializeField] private LoseScreen _loseScreen;

    public override void InstallBindings()
    {
        Container
            .Bind<WinScreen>()
            .FromInstance(_winScreen)
            .AsSingle();

        Container
            .Bind<LoseScreen>()
            .FromInstance(_loseScreen)
            .AsSingle();
    }
}