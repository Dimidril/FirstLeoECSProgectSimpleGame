using Game.Runtime.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Runtime.Systems
{
    public class ScoreCounterSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsCustomInject<SceneService> _sceneService = default;

        private float _timer;
        private int _counterValue;
        
        public void Init(IEcsSystems systems)
        {
            _sceneService.Value.CounterView.SetText(0.ToString());
        }
        
        public void Run(IEcsSystems systems)
        {
            if(_sceneService.Value.GameIsOver)
                return;
            
            if((_timer += TimeService.DeltaTime) < 1)
                return;

            _timer = 0;
            _counterValue++;
            _sceneService.Value.CounterView.SetText(_counterValue.ToString());
        }
    }
}