using Blade.Entities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Blade.Players
{
    public class PlayerAttackCompo : MonoBehaviour, IEntityComponent
    {
        private Entity _entity;
        private EntityAnimator _entityAnimator;

        private readonly int _attackSpeedHash = Animator.StringToHash("ATTACK_SPEED");

        private float _attackSpeed = 1f;
        private float _lastAttackTime;

        public bool useMouseDirection;
        

        
        public float AttackSpeed
        {
            get => _attackSpeed;
            set
            {
                _attackSpeed = value;
                _entityAnimator.SetParam(_attackSpeedHash, _attackSpeed);
            }
        }
        
        public void Initialize(Entity entity)
        {
            _entity = entity;
            _entityAnimator = entity.GetCompo<EntityAnimator>();
            AttackSpeed = 1f;
        }

        public void Attack()
        {
            
        }

        public void EndAttack()
        {
            _lastAttackTime = Time.time;
        }
    }
}