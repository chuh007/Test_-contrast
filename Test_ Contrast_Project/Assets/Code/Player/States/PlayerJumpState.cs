using Code.Entities;
using UnityEngine;

namespace Code.Player.States
{
    public class PlayerJumpState : PlayerState
    {
        private PlayerJumpCompo _jumpCompo;
        private bool _isJumping;
        
        public PlayerJumpState(Entity entity, int animationHash) : base(entity, animationHash)
        {
            _jumpCompo = Player.GetComponentInChildren<PlayerJumpCompo>();
            _jumpCompo.Initialize(Player.transform, Player._agent);
        }

        public override void Enter()
        {
            if (_isJumping) return;
            base.Enter();
            _isJumping = true;
            Player._agent.autoTraverseOffMeshLink = false;
            

            if (Player._agent.isOnOffMeshLink)
            {
                var linkData = Player._agent.currentOffMeshLinkData;
                _jumpCompo.StartJump(linkData.startPos, linkData.endPos);
            }
        }

        public override void Update()
        {
            base.Update();
            
            if (_jumpCompo != null && _jumpCompo.IsJumping)
            {
                _jumpCompo.UpdateJump();
                
                if (!_jumpCompo.IsJumping)
                {
                    if (Player._agent.velocity.sqrMagnitude > 0.1f)
                        Player.ChangeState("MOVE");
                    else
                        Player.ChangeState("IDLE");
                }
            }
        }

        public override void Exit()
        {
            _isJumping = false;
            base.Exit();
        }
    }    
}

