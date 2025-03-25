using Blade.Entities;
using Blade.FSM;
using System;
using System.Collections;
using Blade.Players.States;
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
        [SerializeField] private Image image;
        [SerializeField] private GameObject particle;
        [SerializeField] private Transform debugTrm;
        
        private EntityStateMachine _stateMachine;

        #region Temp region

        public float rollingVelocity = 2.2f;
        public bool isAttack = false;

        #endregion
        
        protected override void Awake()
        {
            base.Awake();
            _stateMachine = new EntityStateMachine(this, states);
            PlayerInput.OnAttackKeyPressed += HandleAttackPressed;
            PlayerInput.OnMousePressed += HandleMousePressed;
        }

        private void Start()
        {
            _stateMachine.ChangeState("IDLE");
        }

        private void Update()
        {
            _stateMachine.UpdateStateMachine();
        }

        private void OnDestroy()
        {
            PlayerInput.OnAttackKeyPressed -= HandleAttackPressed;
            PlayerInput.OnMousePressed -= HandleMousePressed;
        }

        private void HandleAttackPressed()
        {
            if (!(_stateMachine.CurrentState is PlayerIdleState)) return;
            isAttack = !isAttack;
            ChangeAttackUI(isAttack);
        }
        
        private void HandleMousePressed()
        {
            if (!(_stateMachine.CurrentState is PlayerIdleState)) return;
            debugTrm.position = PlayerInput.GetWorldPosition();
            ChangeState("MOVE");
            ChangeAttackUI(false);
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

        public IEnumerator PlayParticle()
        {
            particle.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            particle.gameObject.SetActive(false);
        }
        
    }
}