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
        private string _currentState = "IDLE";

        private bool _isMovingToAttack = false;
        private bool _isKeepingAttack = false;

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
            
            if (_attackTarget && _isMovingToAttack)
            {
                bool hasArrived = !_agent.pathPending &&
                                  _agent.remainingDistance <= _agent.stoppingDistance &&
                                  (!_agent.hasPath || _agent.velocity.sqrMagnitude < 0.01f);

                if (hasArrived)
                {
                    _isMovingToAttack = false;
                    _agent.isStopped = true;
                    ChangeState("ATTACK");
                    return;
                }
            }
            
            if (_attackTarget && _isKeepingAttack && _currentState != "ATTACK")
            {
                ChangeState("ATTACK");
                return;
            }
            
            if (_agent.isOnOffMeshLink && _currentState != "JUMP" && !_isJumping)
            {
                ChangeState("JUMP");
                return;
            }
            
            if (_attackTarget != null && _currentState == "ATTACK")
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
            _isMovingToAttack = true;

            Vector3 direction = (transform.position - target.transform.position).normalized;
            float stoppingDistance = 1.5f;
            Vector3 destination = target.transform.position + direction * stoppingDistance;

            _agent.isStopped = false;
            _agent.SetDestination(destination);
            SmoothRotateToTarget();
        }
        
        private void SmoothRotateToTarget()
        {
            if (!_attackTarget) return;

            Vector3 direction = (_attackTarget.transform.position - transform.position).normalized;
            direction.y = 0f;

            if (direction == Vector3.zero) return;

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        
        private void ClearAttackTarget()
        {
            _attackTarget = null;
            _isMovingToAttack = false;
        }

        public void ChangeState(string newState)
        {
            _stateMachine.ChangeState(newState);
            _currentState = newState;
            OnStateChanged?.Invoke(newState);
        }

        public string GetCurrentState() => _currentState;
    
    }
}

