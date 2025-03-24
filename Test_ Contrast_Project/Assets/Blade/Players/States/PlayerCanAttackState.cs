using Blade.Entities;
using UnityEngine;

namespace Blade.Players.States
{
    public abstract class PlayerCanAttackState : PlayerState
    {
        protected Vector3 PlayerDirection;
        protected Vector3 TargetPos;
        
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
            PlayerDirection = GetPlayerDirection();
            Debug.Log(PlayerDirection);
            _player.ChangeState("MOVE");
        }
        
        private Vector3 GetPlayerDirection()
        {
            TargetPos = _player.PlayerInput.GetWorldPosition();
            Debug.Log(TargetPos);
            Vector3 direction = TargetPos - _player.transform.position;
            direction.y = 0;
            return direction.normalized;
        }
        

    }
}