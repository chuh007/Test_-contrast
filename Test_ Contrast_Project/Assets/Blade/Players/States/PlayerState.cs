using Blade.Entities;
using Blade.FSM;
using System;
using UnityEngine;

namespace Blade.Players.States
{
    public abstract class PlayerState : EntityState
    {
        protected Player _player;
        protected readonly float _inputThreshold = 0.1f;
        protected GameObject _prefab;
        protected Vector3 PlayerDirection;
        protected Vector3 TargetPos;

        protected CharacterMovement _movement;
        
        private GameObject obj;

        public PlayerState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _player = entity as Player;
            _prefab = _player.targetPosPrefab;
            _movement = entity.GetCompo<CharacterMovement>();
        }
        
        protected Vector3 GetPlayerDirection()
        {
            if (obj != null) GameObject.Destroy(obj);
            TargetPos = _player.PlayerInput.GetWorldPosition();
            obj = GameObject.Instantiate(_prefab,TargetPos,Quaternion.identity);
            Vector3 direction = TargetPos - _player.transform.position;
            direction.y = 0;
            return direction.normalized;
        }
    }

}
