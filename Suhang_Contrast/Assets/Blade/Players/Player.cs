using System;
using Blade.Core.Dependencies;
using Blade.Entities;
using Blade.FSM;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Blade.Players
{
    
    public class Player : Entity, IDependencyProvider
    {
        [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }

        [SerializeField] private StateDataSO[] states;
        [SerializeField] private Image hitImage;
        [SerializeField] private Image GameOverImage;
        private EntityStateMachine _stateMachine;

        [Provide]
        public Player ProvidePlayer() => this;
        
        #region Temp region

        public float rollingVelocity = 2.2f;

        #endregion
        protected override void Awake()
        {
            base.Awake();
            _stateMachine = new EntityStateMachine(this, states);
            
            PlayerInput.OnRollingPressed += HandleRollingKeyPressed;
        }

        protected override void HandleHit()
        {
            Debug.Log("마즘");
            hitImage.DOFade(0.6f, 0.15f).onComplete += ReFade;
        }

        private void ReFade()
        {
            hitImage.DOFade(0, 0.3f);
        }

        protected override void HandleDead()
        {
            GameOverImage.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        private void OnDestroy()
        {
            PlayerInput.OnRollingPressed -= HandleRollingKeyPressed;
        }

        private void HandleRollingKeyPressed()
        {
            ChangeState("ROLLING");
        }

        private void Start()
        {
            _stateMachine.ChangeState("IDLE");
        }

        private void Update()
        {
            _stateMachine.UpdateStateMachine();
        }
        
        public void ChangeState(string newStateName) => _stateMachine.ChangeState(newStateName);
        
    }
}