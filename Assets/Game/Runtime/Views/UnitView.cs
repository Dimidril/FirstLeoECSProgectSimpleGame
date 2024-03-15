using Game.Runtime.Components;
using Game.Runtime.CustomStructs;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Runtime.Views
{
    public class UnitView : MonoBehaviour
    {
        private static readonly int Up = Animator.StringToHash("up");
        private static readonly int Walk = Animator.StringToHash("walk");

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Animator _animator;

        private int _entity;
        private EcsWorld _world;
        
        public EcsVector3 Position => new EcsVector3(transform.position.x, transform.position.y, transform.position.z);
        
        public void Construct(int entity, EcsWorld world)
        {
            _entity = entity;
            _world = world;
        }
        
        private void OnCollisionEnter2D(Collision2D _)
        {
            var entity = _world.NewEntity();
            var pool = _world.GetPool<CollisionEvent>();
            ref var evt = ref pool.Add(entity);
            evt.CollidedEntity = _entity;
        }
        
        public void Move(EcsVector3 translation)
        {
            var uVector = new Vector3(translation.X, translation.Y, translation.Z);
            transform.Translate(uVector);
        }

        public void SetDestination(EcsVector3 velocity)
        {
            _spriteRenderer.flipX = velocity.X < 0;
            _spriteRenderer.flipY = velocity.Y < 0;
        }

        public void UpdateAnimationState(EcsVector3 velocity)
        {
            _animator.SetBool(Up, velocity.Y != 0);
            _animator.SetBool(Walk, velocity.X != 0 && velocity.Y == 0);
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public void SetPosition(EcsVector3 position)
        {
            transform.position = new Vector3(position.X, position.Y, position.Z);
        }

        public void RotateTo(EcsVector3 position)
        {
            var direction = position - Position;
            var angle = Mathf.Atan2(direction.Y, direction.X) * Mathf.Rad2Deg;
            var rotation = Quaternion.Euler(0, 0, angle - 90);
            transform.rotation = rotation;
        }
    }
}