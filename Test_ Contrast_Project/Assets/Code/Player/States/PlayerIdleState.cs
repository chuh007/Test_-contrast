using Code.Entities;
using Code.FSM;
using UnityEngine;

namespace Code.Player.States
{
    public class PlayerIdleState : PlayerState
    {
        public PlayerIdleState(Entity entity, int animationHash) : base(entity, animationHash)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            _movement.StopImmediately();
        }
        
    }
}
