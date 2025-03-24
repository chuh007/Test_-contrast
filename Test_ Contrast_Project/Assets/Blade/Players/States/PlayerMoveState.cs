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

        public override void Update()
        {
            base.Update();
            
            _movement.SetMovementDirection(PlayerDirection);
            if (Vector3.Distance(TargetPos,_player.transform.position) < _inputThreshold)
                _player.ChangeState("IDLE");
        }

    }
}

