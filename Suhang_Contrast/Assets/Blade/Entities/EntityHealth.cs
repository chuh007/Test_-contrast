using System;
using Blade.Enemies;
using Blade.Entities;
using UnityEngine;

namespace Code.Entities
{
    public class EntityHealth : MonoBehaviour, IEntityComponent, IAfterInit
    {
        // [SerializeField] private StatSO hpStat;
        public float maxHealth;
        private float _currentHealth;
        
        public event Action<Vector2> OnKnockback;
        
        private Entity _entity;

        // private EntityStat _statCompo;
        // private EntityFeedbackData _feedbackData;

        #region Initialize section

        public void Initialize(Entity entity)
        {
            _entity = entity;
            // _statCompo = _entity.GetCompo<EntityStat>();
            // _feedbackData = _entity.GetCompo<EntityFeedbackData>();
        }
        
        public void AfterInit()
        {
            // _statCompo.GetStat(hpStat).OnValueChange += HandleHPChange;
            _currentHealth = maxHealth;
            _entity.OnDamage += ApplyDamage;
        }

        private void OnDestroy()
        {
            // _statCompo.GetStat(hpStat).OnValueChange -= HandleHPChange;
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
            OnKnockback?.Invoke(direction);

            if (_currentHealth <= 0)
            {
                _entity.IsDead = true;
                _entity.OnDead?.Invoke();
            }
        }
    }
}