using System;
using Blade.Combat;
using Blade.Entities;
using Unity.Behavior;
using UnityEngine;

namespace Blade.Enemies
{
    public class EnemyAttackCompo : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private DamageCaster damageCaster;

        private Enemy _enemy;
        private EntityAnimatorTrigger _animatorTrigger;
        
        public void Initialize(Entity entity)
        {
            _enemy = entity as Enemy;
            _animatorTrigger = entity.GetCompo<EntityAnimatorTrigger>();
            Debug.Assert(_enemy != null, $"Not corrected entity - enemy attack component [{entity.gameObject.name}]");
            damageCaster.InitCaster(entity);

            _animatorTrigger.OnAttackTrigger += HandleAttack;
        }

        private void HandleAttack()
        {
            damageCaster.CastDamage(10, Vector3.zero);
        }


        private void OnDestroy()
        {
            _animatorTrigger.OnAttackTrigger -= HandleAttack;
        }
    }
}
