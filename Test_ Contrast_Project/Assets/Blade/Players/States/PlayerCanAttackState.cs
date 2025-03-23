using Blade.Entities;
using UnityEngine;

namespace Blade.Players.States
{
    public abstract class PlayerCanAttackState : PlayerState
    {
        protected Vector3 _playerDirection;
        
        protected PlayerCanAttackState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            _player.PlayerInput.OnMousePressed += HandleMousePressed;
        }

        

        public override void Exit()
        {
            _player.PlayerInput.OnMousePressed -= HandleMousePressed;
            base.Exit();
        }
        
        private void HandleMousePressed()
        {
            _playerDirection = GetPlayerDirection();
            _player.ChangeState("MOVE");
        }
        
        private Vector3 GetPlayerDirection()
        {
            Vector3 targetPosition = _player.PlayerInput.GetWorldPosition();
            Vector3 direction = targetPosition - _player.transform.position;
            direction.y = 0;
            return direction.normalized;
        }
        

    }
}