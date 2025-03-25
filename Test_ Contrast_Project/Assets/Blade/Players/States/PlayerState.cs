using Blade.Entities;
using Blade.FSM;
using System;
using UnityEngine;

namespace Blade.Players.States
{
    public abstract class PlayerState : EntityState
    {
        protected Player _player;
        protected Vector3 PlayerDirection;
        protected Vector3 TargetPos;

        protected CharacterMovement _movement;
        

        public PlayerState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _player = entity as Player;
            _movement = entity.GetCompo<CharacterMovement>();
        }
        
        protected Vector3 GetPlayerDirection()
        {
            TargetPos = _player.PlayerInput.GetWorldPosition();
            Vector3 direction = TargetPos - _player.transform.position;
            direction.y = 0;
            return direction.normalized;
        }
    }

}
