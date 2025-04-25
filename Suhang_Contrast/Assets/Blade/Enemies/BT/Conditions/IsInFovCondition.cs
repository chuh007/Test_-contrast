using System;
using Unity.Behavior;
using UnityEngine;

namespace Blade.Enemies.BT.Conditions
{
    [Serializable, Unity.Properties.GeneratePropertyBag]
    [Condition(name: "IsInFOV", story: "[Target] is in [Self] VOF", category: "Conditions", id: "a62f45d191181b772ef25cc2bad17f41")]
    public partial class IsInFovCondition : Condition
    {
        [SerializeReference] public BlackboardVariable<Transform> Target;
        [SerializeReference] public BlackboardVariable<Enemy> Self;

        public override bool IsTrue()
        {
            Vector3 dir = Target.Value.position - Self.Value.transform.position;
            bool isHit = Physics.Raycast(Self.Value.transform.position, dir.normalized, out RaycastHit hit);
            return isHit && hit.transform.CompareTag("Player");
        }
    }
}
