using Blade.Entities;
using Blade.FSM;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Blade.Players
{
    public class Player : Entity
    {
        [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }

        [SerializeField] private StateDataSO[] states;
        [SerializeField] private TextMeshProUGUI StateText;
        public GameObject targetPosPrefab;
        [SerializeField] private Image image;
        
        private EntityStateMachine _stateMachine;

        #region Temp region

        public float rollingVelocity = 2.2f;
        public bool isAttack = false;

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

        public void ChangeState(string newStatName)
        {
            _stateMachine.ChangeState(newStatName);
            StateText.text = newStatName;
        }

        public void ChangeAttackUI(bool attack)
        {
            if(attack) image.color = Color.yellow;
            else image.color = Color.white;
        }

    }
}