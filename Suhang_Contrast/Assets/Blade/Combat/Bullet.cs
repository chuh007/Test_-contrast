using System;
using Blade.Combat;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Combat
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particle;
        private Rigidbody _rb;
        private float _damage;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void InitAndFire(Vector3 dir, float damage)
        {
            _damage = damage;
            _rb.linearVelocity = dir.normalized * 15f;
        }

        private void OnCollisionEnter(Collision other)
        {
            ContactPoint contact = other.contacts[0];
            Vector3 pos = contact.point;
            Vector3 normal = contact.normal;
            Debug.Log("hit");
            if (other.collider.TryGetComponent(out IDamageable hitable))
            {
                hitable.ApplyDamage(_damage, other.contacts[0].normal);
                Quaternion rotation = Quaternion.FromToRotation(Vector3.up, normal);
                Instantiate(particle, pos, rotation);
                Destroy(gameObject);
            }
        }

    }
}