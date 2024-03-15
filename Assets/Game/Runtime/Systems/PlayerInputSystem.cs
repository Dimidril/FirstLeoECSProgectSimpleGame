using Game.Runtime.Components;
using Game.Runtime.CustomStructs;
using Game.Runtime.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Runtime.Systems
{
    public sealed class PlayerInputSystem: IEcsInitSystem ,IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsPoolInject<Unit> _unitPool = default;
        private readonly EcsPoolInject<PlayerTag> _playerTagPool = default;
        private readonly EcsCustomInject<SceneService> _sceneService = default;

        private int _playerEntity;
        
        public void Init(IEcsSystems systems)
        {
            _playerEntity = _world.Value.NewEntity();
            _playerTagPool.Value.Add(_playerEntity);
            ref var playerComponent = ref _unitPool.Value.Add(_playerEntity);
            playerComponent.View = _sceneService.Value.PlayerView;
            playerComponent.View.Construct(_playerEntity, _world.Value);
        }
        
        public void Run(IEcsSystems systems)
        {
            var playerMoveSpeed = _sceneService.Value.PlayerMoveSpeed;
            var direction = new EcsVector3(InputService.HorizontalInput,
                InputService.VerticalInput, 0).
                Normalized;
            var velocity = direction * playerMoveSpeed;
            
            if(!_unitPool.Value.Has(_playerEntity))
                return;

            ref var player = ref _unitPool.Value.Get(_playerEntity);
            player.Velocity = velocity;
        }
    }
}