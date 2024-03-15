using Game.Runtime.Components;
using Game.Runtime.Services;
using Game.Runtime.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using UnityEngine;

namespace Game.Runtime
{
    internal sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField] private SceneService _sceneService;

        private EcsWorld _world;
        private IEcsSystems _systems;
        
        private void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _systems
                .Add(new PlayerInputSystem())
                .Add(new MovementSystem())
                .Add(new EnemiesSystem())
                .Add(new LifetimeSystem())
                .Add(new ScoreCounterSystem())
                .Add(new EndGameSystem())
                .DelHere<CollisionEvent>()
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .Inject(_sceneService)
                .Init();
        }

        private void Update()
        {
            _systems?.Run();
        }

        private void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
            }

            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }
        }
    }
}