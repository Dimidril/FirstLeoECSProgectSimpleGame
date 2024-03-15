using Game.Runtime.Components;
using Game.Runtime.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Game.Runtime.Systems
{
    public class EndGameSystem: IEcsRunSystem
    {
        private readonly EcsPoolInject<CollisionEvent> _collisionEventPool = default;
        private readonly EcsFilterInject<Inc<CollisionEvent>> _collisionEventFilter = default;
        private readonly EcsPoolInject<PlayerTag> _playerTagPool = default;
        private readonly EcsFilterInject<Inc<Unit>> _unitFilter;
        private readonly EcsCustomInject<SceneService> _sceneService = default;
        
        public void Run(IEcsSystems systems)
        {
            if (_sceneService.Value.GameIsOver)
                return;
            
            CheckWinCondition();
            CheckLoseCondition();
        }

        private void CheckLoseCondition()
        {
            foreach (var entity in _collisionEventFilter.Value)
            {
                ref var collisionEvent = ref _collisionEventPool.Value.Get(entity);
                var collidedEntity = collisionEvent.CollidedEntity;
                if(!_playerTagPool.Value.Has(collidedEntity))
                    continue;
                Lose();
            }
        }

        private void CheckWinCondition()
        {
            if (TimeService.TimeSinceLevelLoad < 10)
                return;
            Win();
        }

        private void Lose()
        {
            _sceneService.Value.GameIsOver = true;
            StopAllUnits();
            ShowEndPopup("Вы проиграли");
        }

        private void Win()
        {
            _sceneService.Value.GameIsOver = true;
            StopAllUnits();
            ShowEndPopup("Вы победили");
        }

        private void ShowEndPopup(string text)
        {
            var popup = _sceneService.Value.PopupView;
            
            popup.SetDescription(text);
            popup.SetButtonText("Повторить");
            popup.RemoveAllListeners();
            popup.AddListenerToButton(RestartGame);
            popup.SetActive(true);
        }

        private void StopAllUnits()
        {
            foreach (var entity in _unitFilter.Value)
            {
                _unitFilter.Pools.Inc1.Del(entity);
            }
        }
        
        private void RestartGame()
        {
            _sceneService.Value.ReloadScene();
        }
    }
}