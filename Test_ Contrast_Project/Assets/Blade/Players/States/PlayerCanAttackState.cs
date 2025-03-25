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
        }

        


        public override void Exit()
        {
            base.Exit();
        }
        
    }
}