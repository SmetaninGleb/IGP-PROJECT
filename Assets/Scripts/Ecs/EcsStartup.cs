using LeoEcsGui;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;
using UnityEngine;
using Zenject;

sealed class EcsStartup : MonoBehaviour {

    [Inject] private EcsWorld _world;
    [Inject] private EcsSystems _systems;
    [Inject] private MainPathInitializingSystem _mainPathInitializingSystem;
    [Inject] private MainPathMovingSystem _mainPathMovingSystem;
    [Inject] private PlayerSideMovementSystem _playerSideMovementSystem;
    [Inject] private CubeStackingSystem _cubeStackingSystem;
    [Inject] private StackObstacleCollidingSystem _stackObstacleCollidingSystem;
    [Inject] private StackLavaCollidingSystem _stackLavaCollidingSystem;
    [Inject] private FinishStepsSystem _finishStepSystem;
    [Inject] private WinSystem _winSystem;
    [Inject] private LoseSystem _loseSystem;
    [Inject] private LoadLevelSystem _loadLevelSystem;
    [Inject] private PointsCollectingSystem _pointsCollectingSystem;

    void Start () {

        _systems
            .Add(_mainPathInitializingSystem)
            .Add(_mainPathMovingSystem)
            .Add(_playerSideMovementSystem)
            .Add(_cubeStackingSystem)
            .Add(_stackObstacleCollidingSystem)
            .Add(_stackLavaCollidingSystem)
            .Add(_finishStepSystem)
            .Add(_winSystem)
            .Add(_loseSystem)
            .Add(_loadLevelSystem)
            .Add(_pointsCollectingSystem)
            .DelHerePhysics()
            .DelHereGuiEvents()
            .DelHere<LoadLevelSignal>()
            .DelHere<MultiplyPointsSignal>()
            .DelHere<FinishCurveSignal>()
            .Init ();

        EcsPhysicsEvents.ecsWorld = _world;
        EcsGuiEvents.SetWorld(_world);
    }

    void Update () {
        _systems?.Run ();
    }

    void OnDestroy () {
        if (_systems != null) {
            _systems.Destroy ();
            _systems.GetWorld ().Destroy ();
            _systems = null;
        }
    }
}