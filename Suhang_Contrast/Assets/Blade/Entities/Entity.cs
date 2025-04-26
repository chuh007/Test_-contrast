using System;
using System.Collections.Generic;
using System.Linq;
using Blade.Combat;
using UnityEngine;
using UnityEngine.Events;

namespace Blade.Entities
{
    public abstract class Entity : MonoBehaviour, IDamageable
    {
        public delegate void OnDamageHandler(float damage, Vector2 direction);
        public event OnDamageHandler OnDamage;
        public bool IsDead { get; set; }
        
        protected Dictionary<Type, IEntityComponent> _components;

        public UnityEvent OnHit;
        public UnityEvent OnDead;
        
        protected virtual void Awake()
        {
            _components = new Dictionary<Type, IEntityComponent>();
            AddComponents();
            InitializeComponents();
            AfterInitialize();
        }

        protected virtual void AfterInitialize()
        {
            _components.Values.OfType<IAfterInit>().ToList().ForEach(compo => compo.AfterInit());
            OnHit.AddListener(HandleHit);
            OnDead.AddListener(HandleDead);
        }
        
        protected virtual void AddComponents()
        {
            GetComponentsInChildren<IEntityComponent>().ToList()
                .ForEach(component => _components.Add(component.GetType(), component));
        }

        protected virtual void InitializeComponents()
        {
            _components.Values.ToList().ForEach(component => component.Initialize(this));
        }

        public T GetCompo<T>() where T : IEntityComponent
            => (T)_components.GetValueOrDefault(typeof(T));

        public IEntityComponent GetCompo(Type type)
            => _components.GetValueOrDefault(type);


        protected abstract void HandleHit();

        protected abstract void HandleDead();

        public void ApplyDamage(float damage, Vector3 direction) => OnDamage?.Invoke(damage, direction);
    }
}