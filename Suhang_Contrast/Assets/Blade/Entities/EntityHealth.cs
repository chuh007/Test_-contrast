using System;
using Blade.Enemies;
using Blade.Entities;
using UnityEngine;

namespace Code.Entities
{
    public class EntityHealth : MonoBehaviour, IEntityComponent, IAfterInit
    {
        public float maxHealth;
        private float _currentHealth;

        public event Action<float> DamageEvt;
        
        private Entity _entity;


        #region Initialize section

        public void Initialize(Entity entity)
        {
            _entity = entity;
        }
        
        public void AfterInit()
        {
            _currentHealth = maxHealth;
            _entity.OnDamage += ApplyDamage;
        }

        private void OnDestroy()
        {
            _entity.OnDamage -= ApplyDamage;
        }

        #endregion
        


        public void ApplyDamage(float damage, Vector2 direction)
        {
            if (_entity.IsDead) return;
            _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, maxHealth);
            Debug.Log(_currentHealth);
            AfterHitFeedbacks(direction);
        }

        private void AfterHitFeedbacks(Vector2 direction)
        {
            _entity.OnHit?.Invoke();
            DamageEvt?.Invoke(_currentHealth);

            if (_currentHealth <= 0)
            {
                _entity.IsDead = true;
                _entity.OnDead?.Invoke();
            }
        }
    }
}