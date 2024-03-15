using Game.Runtime.Components;
using Game.Runtime.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Runtime.Systems
{
    public class LifetimeSystem: IEcsRunSystem
    {
        private readonly EcsWorldInject _world = default;
        private readonly EcsCustomInject<SceneService> _sceneService = default;
        private readonly EcsPoolInject<Unit> _unitPool = default;
        private readonly EcsPoolInject<Lifetime> _lifetimePool = default;
        private readonly EcsFilterInject<Inc<Lifetime>> _lifetimeFilter = default;


        public void Run(IEcsSystems systems)
        {
            if(_sceneService.Value.GameIsOver)
                return;
            foreach (var entity in _lifetimeFilter.Value)
            {
                ref var lifetime = ref _lifetimePool.Value.Get(entity);
                lifetime.Value -= TimeService.DeltaTime;
                
                if(lifetime.Value > 0)
                    continue;

                ref var unit = ref _unitPool.Value.Get(entity);
                _sceneService.Value.ReleaseEnemy(unit.View);
                _world.Value.DelEntity(entity);
            }
        }
    }
}