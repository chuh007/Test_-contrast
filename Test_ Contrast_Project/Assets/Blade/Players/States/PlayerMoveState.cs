using Blade.Entities;
using Blade.FSM;
using System;
using UnityEngine;

namespace Blade.Players.States
{
    public class PlayerMoveState : PlayerCanAttackState
    {
        
        public PlayerMoveState(Entity entity, int animationHash) : base(entity, animationHash)
        {
        }

        public override void Enter()
        {
            base.Enter();
            PlayerDirection = GetPlayerDirection();
        }

        public override void Update()
        {
            base.Update();
            _movement.SetMovementDirection(PlayerDirection);
            if (Vector3.Distance(TargetPos,_player.transform.position) < 1.2f)
                if (_player.isAttack) _player.ChangeState("ATTACK");
                else _player.ChangeState("IDLE");
        }

    }
}

