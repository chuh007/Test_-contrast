using Blade.Entities;
using Blade.FSM;
using System;
using UnityEngine;

namespace Blade.Players.States
{
    public class PlayerIdleState : PlayerCanAttackState
    {

        public PlayerIdleState(Entity entity, int animationHash) : base(entity, animationHash)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _movement.StopImmediately();
        }

        public override void Update()
        {
            base.Update();
        }

    }
}

