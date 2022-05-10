using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private PlayerSpawner _playerSpawner;
    [SerializeField] private PlayerMovementConfig _movementConfig;
    
    public override void InstallBindings()
    {
        Player player = Container
            .InstantiatePrefabForComponent<Player>(
            _playerSpawner.PlayerPrefab, 
            _playerSpawner.transform.position, 
            _playerSpawner.transform.rotation, 
            null);

        Container
            .Bind<Player>()
            .FromInstance(player)
            .AsSingle();

        Container
            .Bind<PlayerMovementConfig>()
            .FromInstance(_movementConfig)
            .AsSingle();
    }
}