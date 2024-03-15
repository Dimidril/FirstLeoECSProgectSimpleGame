using Game.Runtime.Components;
using Game.Runtime.CustomStructs;
using Game.Runtime.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Runtime.Systems
{
    public class MovementSystem: IEcsRunSystem
    {
        private readonly EcsPoolInject<Unit> _unitPool = default;
        private readonly EcsFilterInject<Inc<Unit>> _unitFilter = default;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _unitFilter.Value)
            {
                var unit = _unitPool.Value.Get(entity);
                var velocity = unit.Velocity;
                var view = unit.View;
                view.UpdateAnimationState(velocity);
                
                if(velocity == EcsVector3.Zero)
                    continue;

                var translation = velocity * TimeService.DeltaTime;
                view.SetDestination(velocity);
                view.Move(translation);
            }
        }
    }
}