using UnityEngine;
using Zenject;

public class PlayerPrefsConfigInstaller : MonoInstaller
{
    [SerializeField] private PlayerPrefsConfig _playerPrefsConfig;

    public override void InstallBindings()
    {
        Container
            .Bind<PlayerPrefsConfig>()
            .FromInstance(_playerPrefsConfig)
            .AsSingle();
    }
}