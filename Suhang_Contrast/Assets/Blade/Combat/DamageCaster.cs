using Blade.Entities;
using UnityEngine;

namespace Blade.Combat
{
    public class DamageCaster : MonoBehaviour
    {
        [SerializeField] protected int maxHitCount = 1; //최대 피격 가능 객체 수
        [SerializeField] protected LayerMask hitLayerMask;
        [SerializeField] private float damageRadius;
        
        
        private Entity _owner;
        
        public virtual void InitCaster(Entity owner)
        {
            _owner = owner;
        }

        public bool CastDamage(float damageData, Vector3 knockBack)
        {
            Debug.Log("ASD");
            Collider[] targets = Physics.OverlapSphere(transform.position, damageRadius, hitLayerMask);

            foreach (var hitResult in targets)
            {
                Debug.Log(hitResult.gameObject.name);
                Vector3 direction = (hitResult.transform.position - _owner.transform.position).normalized;
            
                knockBack.x *= Mathf.Sign(direction.x);
        
                if (hitResult.TryGetComponent(out IDamageable damageable))
                {
                    damageable.ApplyDamage(damageData, direction);
                }
            }
            
            return targets.Length > 0;
        }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.7f, 0.7f, 0, 1f);
            Gizmos.DrawWireSphere(transform.position, damageRadius);
        }
#endif
    }
}
