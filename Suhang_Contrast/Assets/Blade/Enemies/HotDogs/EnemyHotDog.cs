using System;
using Blade.Enemies.BT.Events;
using UnityEngine;
using Unity.Behavior;
namespace Blade.Enemies.HotDogs
{
    public class EnemyHotDog : Enemy
    {
        private StateChange _stateChannel;

        private void Start()
        {
            BlackboardVariable<StateChange> stateChannelVariable =
                GetBlackboardVariable<StateChange>("StateChannel");
            _stateChannel = stateChannelVariable.Value;
        }

        protected override void HandleHit()
        {
            
        }

        protected override void HandleDead()
        {
            _stateChannel.SendEventMessage(EnemyState.DEAD);

        }
    }
}
