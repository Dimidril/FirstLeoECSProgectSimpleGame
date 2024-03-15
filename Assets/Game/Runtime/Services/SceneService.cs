using Game.Runtime.CustomStructs;
using Game.Runtime.Views;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

namespace Game.Runtime.Services
{
    public class SceneService : MonoBehaviour
    {
        [field: SerializeField] public UnitView PlayerView { get; private set; }
        [field: SerializeField] public float PlayerMoveSpeed { get; private set; }
        [field: SerializeField] public UnitView EnemyViewPrefab { get; private set; }
        [field: SerializeField] public Camera Camera { get; private set; }
        [field: SerializeField] public float EnemyMoveSpeed { get; private set; } = 13f;
        [field: SerializeField] public float EnemySpawnInterval { get; private set; } = .5f;
        [field: SerializeField] public float EnemyLifetime { get; private set; } = 5f;
        [field: SerializeField] public CounterView CounterView { get; private set; }
        [field: SerializeField] public PopupView PopupView { get; private set; }
        
        public bool GameIsOver { get; set; }
        
        private ObjectPool<UnitView> _unitsPool;

        private void Awake()
        {
            _unitsPool = new ObjectPool<UnitView>(() => Instantiate(EnemyViewPrefab));
        }
        
        public UnitView GetEnemy()
        {
            var view = _unitsPool.Get();
            view.SetActive(true);
            return view;
        }

        public void ReleaseEnemy(UnitView view)
        {
            view.SetActive(false);
            _unitsPool.Release(view);
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        public EcsVector3 GetOutOfScreenPosition()
        {
            var randomX = Random.Range(-1000, 1000);
            var randomY = Random.Range(-1000, 1000);
            var randomPosition = new Vector3(randomX, randomY);
            var randomDirection = (Camera.transform.position - randomPosition).normalized;
            var cameraHeight = Camera.orthographicSize * 2;
            var cameraWith = cameraHeight * Camera.aspect;
            
            return new EcsVector3(randomDirection.x * cameraHeight, randomDirection.y * cameraWith, 0);
        }
    }
}
