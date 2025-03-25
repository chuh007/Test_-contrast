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
            _animatorTrigger.OnAnimationEventTrigger += AnimationEventTrigger;
            _attackCompo.Attack();
            _movement.CanManualMovement = false;
            _player.isAttack = false;
        }

        public override void Exit()
        {
            _animatorTrigger.OnAnimationEventTrigger -= AnimationEventTrigger;
            _attackCompo.EndAttack();
            _movement.CanManualMovement = true;
            _movement.StopImmediately();
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            if (_isTriggerCall)
            {
                _player.ChangeState("IDLE");
            }
        }
        
        private void AnimationEventTrigger()
        {
            _player.StartCoroutine(_player.PlayParticle());
        }
    }
}