using Blade.Entities;
using Blade.FSM;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Blade.Players
{
    public class Player : Entity
    {
        [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }

        [SerializeField] private StateDataSO[] states;

        private EntityStateMachine _stateMachine;

        #region Temp region

        public float rollingVelocity = 2.2f;

        #endregion
        
        protected override void Awake()
        {
            base.Awake();
            _stateMachine = new EntityStateMachine(this, states);

        }

        private void OnDisable()
        {
        }


        private void Start()
        {
            _stateMachine.ChangeState("IDLE");
        }

        private void Update()
        {
            _stateMachine.UpdateStateMachine();
        }

        public void ChangeState(string newStatName) => _stateMachine.ChangeState(newStatName);

    }
}