using Code.Entities;
using UnityEngine;

namespace Code.Player.States
{
    public class PlayerMoveState : PlayerState
    {
        public PlayerMoveState(Entity entity, int animationHash) : base(entity, animationHash)
        {
        }

        public override void Update()
        {
            base.Update();
            if (_movement.IsArrived)
            {
                Player.ChangeState("IDLE");
            }
        }
        
    
    }
}
