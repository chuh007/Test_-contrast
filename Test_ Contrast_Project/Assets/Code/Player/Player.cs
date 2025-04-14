using System;
using Code.Entities;
using Code.FSM;
using UnityEngine;
using UnityEngine.AI;

namespace Code.Player
{
    public class Player : Entity
    {
        [SerializeField] private StateDataSO[] stateList;
        [SerializeField] private float rotationSpeed = 10f;

        public NavMeshAgent _agent;
        private EntityStateMachine _stateMachine;
        private GameObject _attackTarget;
        private bool _isJumping;

        public event Action<string> OnStateChanged;
        private string currentState = "IDLE";

        private bool isMovingToAttack = false;
        private bool isKeepingAttack = false;

        protected override void Awake()
        {
            base.Awake();
            _agent = GetComponent<NavMeshAgent>();
            _stateMachine = new EntityStateMachine(this, stateList);
            _agent.updateRotation = false;
        }

        private void Start()
        {
            ChangeState("IDLE");
        }

        private void Update()
        {
            _stateMachine.UpdateStateMachine();
            
            if (_attackTarget != null && isMovingToAttack)
            {
                bool hasArrived = !_agent.pathPending &&
                                  _agent.remainingDistance <= _agent.stoppingDistance &&
                                  (!_agent.hasPath || _agent.velocity.sqrMagnitude < 0.01f);

                if (hasArrived)
                {
                    isMovingToAttack = false;
                    _agent.isStopped = true;
                    ChangeState("ATTACK");
                    return;
                }
            }
            
            if (_attackTarget != null && isKeepingAttack && currentState != "ATTACK")
            {
                ChangeState("ATTACK");
                return;
            }
            
            if (_agent.isOnOffMeshLink && currentState != "JUMP" && !_isJumping)
            {
                ChangeState("JUMP");
                return;
            }
            
            if (_attackTarget != null && currentState == "ATTACK")
            {
                SmoothRotateToTarget();
            }
        }

        public void MoveToPosition(Vector3 position)
        {
            if (_agent == null) return;
            _agent.isStopped = false;
            _agent.SetDestination(position);
            ClearAttackTarget();
            ChangeState("MOVE");
        }

        public void Attack(GameObject target)
        {
            if (_agent == null) return;

            _attackTarget = target;
            isMovingToAttack = true;

            Vector3 direction = (transform.position - target.transform.position).normalized;
            float stoppingDistance = 1.5f;
            Vector3 destination = target.transform.position + direction * stoppingDistance;

            _agent.isStopped = false;
            _agent.SetDestination(destination);
            SmoothRotateToTarget();
        }
        
        public void SmoothRotateToTarget()
        {
            if (_attackTarget == null) return;

            Vector3 direction = (_attackTarget.transform.position - transform.position).normalized;
            direction.y = 0f;

            if (direction == Vector3.zero) return;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        
        public void ClearAttackTarget()
        {
            _attackTarget = null;
            isMovingToAttack = false;
        }

        public void ChangeState(string newState)
        {
            _stateMachine.ChangeState(newState);
            currentState = newState;
            OnStateChanged?.Invoke(newState);
        }

        public string GetCurrentState() => currentState;
    
    }
}

