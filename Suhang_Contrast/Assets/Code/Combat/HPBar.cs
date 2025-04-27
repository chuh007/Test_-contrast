using System;
using Code.Entities;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Code.Combat
{
    public class HPBar : MonoBehaviour
    {
        [SerializeField] private Image fillImage;
        [SerializeField] private EntityHealth healthCompo;

        private void Awake()
        {
            healthCompo.DamageEvt += SetHP;
        }


        private void OnDestroy()
        {
            healthCompo.DamageEvt -= SetHP;
        }
        private void SetHP(float hp)
        {
            fillImage.fillAmount = hp / 100f;
        }
        
        void Update()
        {
            transform.LookAt(Camera.main.transform);
            transform.Rotate(0, 180, 0);
        }
    }
}