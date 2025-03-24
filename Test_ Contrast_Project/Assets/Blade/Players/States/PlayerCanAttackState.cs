using Blade.Entities;
using UnityEngine;

namespace Blade.Players.States
{
    public abstract class PlayerCanAttackState : PlayerState
    {
        protected PlayerCanAttackState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            _player.PlayerInput.OnMousePressed += HandleMousePressed;
            _player.PlayerInput.OnAttackKeyPressed += HandleAttackPressed;
        }

        


        public override void Exit()
        {
            _player.PlayerInput.OnMousePressed -= HandleMousePressed;
            _player.PlayerInput.OnAttackKeyPressed -= HandleAttackPressed;
            base.Exit();
        }
        
        private void HandleMousePressed()
        {
            _player.ChangeState("MOVE");
            _player.ChangeAttackUI(false);
        }
        
        
        
        private void HandleAttackPressed()
        {
            _player.isAttack = true;
            _player.ChangeAttackUI(true);
        }
    }
}