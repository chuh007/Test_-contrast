using Code.Entities;
using Code.FSM;

namespace Code.Player.States
{
    public class PlayerState : EntityState
    {
        protected Player Player;
        protected PlayerMovement _movement;   

        public PlayerState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            Player = entity as Player;
            _movement = entity.GetCompo<PlayerMovement>();
        }
    
    }    
}

