using Game.Runtime.Components;
using Game.Runtime.CustomStructs;
using Game.Runtime.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Runtime.Systems
{
    public class EnemiesSystem: IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsCustomInject<SceneService> _sceneService = default;
        private readonly EcsPoolInject<Unit> _unitPool = default;
        private readonly EcsPoolInject<Lifetime> _lifetimePool = default;

        private float _spawnInterval;

        public void Init(IEcsSystems systems)
        {
            _spawnInterval = _sceneService.Value.EnemySpawnInterval;
        }

        public void Run(IEcsSystems systems)
        {
            if(_sceneService.Value.GameIsOver)
                return;
            CreateEnemy();
        }

        private void CreateEnemy()
        {
            if((_spawnInterval -= TimeService.DeltaTime) >= 0) 
                return;

            _spawnInterval = _sceneService.Value.EnemySpawnInterval;

            var enemyView = _sceneService.Value.GetEnemy();
            var enemyPosition = _sceneService.Value.GetOutOfScreenPosition();
            enemyView.SetPosition(enemyPosition);
            enemyView.RotateTo(_sceneService.Value.PlayerView.Position);

            var enemyEntity = _world.Value.NewEntity();
            ref var enemyUnit = ref _unitPool.Value.Add(enemyEntity);
            enemyUnit.View = enemyView;
            enemyUnit.Velocity = EcsVector3.Up * _sceneService.Value.EnemyMoveSpeed;
            enemyUnit.View.Construct(enemyEntity, _world.Value);

            ref var lifetime = ref _lifetimePool.Value.Add(enemyEntity);
            lifetime.Value = _sceneService.Value.EnemyLifetime; 
        }
    }
}