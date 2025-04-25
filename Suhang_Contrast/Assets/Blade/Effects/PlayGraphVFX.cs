using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.VFX;

namespace Blade.Effects
{
    public class PlayGraphVFX : MonoBehaviour, IPlayableVFX
    {
        [field : SerializeField] public string VFXName { get; set; }
        [SerializeField] private bool isOnPosition;
        [FormerlySerializedAs("effect")] [SerializeField] private VisualEffect[] effects;
        public void PlayVFX(Vector3 position, Quaternion rotation)
        {
            if(isOnPosition)
                transform.SetPositionAndRotation(position, rotation);
            foreach (var effect in effects)
                effect.Play();
        }

        public void StopVFX()
        {
            foreach (var effect in effects)
                effect.Stop();
        }

        private void OnValidate()
        {
            if(string.IsNullOrEmpty(VFXName) == false)
                gameObject.name = VFXName;
        }
    }
}