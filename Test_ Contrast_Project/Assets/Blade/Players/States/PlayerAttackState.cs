using Blade.Entities;
using UnityEngine;

namespace Blade.Players.States
{
    public class PlayerAttackState : PlayerState
    {
        private PlayerAttackCompo _attackCompo;
        
        public PlayerAttackState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _attackCompo = entity.GetCompo<PlayerAttackCompo>();
        } 

        public override void Enter()
        {
            base.Enter();
            _attackCompo.Attack();
            _movement.CanManualMovement = false;
            _player.isAttack = false;
        }
        
        public override void Exit()
        {
            _attackCompo.EndAttack();
            _movement.CanManualMovement = true;
            _movement.StopImmediately();
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            if (_isTriggerCall)
                _player.ChangeState("IDLE");
        }
    }
}