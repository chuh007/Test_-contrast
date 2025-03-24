using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Blade.Players
{
    [CreateAssetMenu(fileName = "PlayerInputSO", menuName = "SO/PlayerInputSO")]
    public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions
    {
        [SerializeField] private LayerMask whatIsGround;
        
        public event Action OnMousePressed;
        public event Action OnAttackKeyPressed;
        public Vector2 MovementKey {get; private set;}

        private Controls _controls;
        private Vector2 _screenPosition; // 마우스 좌표
        private Vector3 _worldPosition;

        private void OnEnable()
        {
            if(_controls == null)
            {
                _controls = new Controls();
                _controls.Player.SetCallbacks(this);
            }
            _controls.Enable();
            
        }

        private void OnDisable()
        {
            _controls.Player.Disable();
        }
        
        public void OnPointer(InputAction.CallbackContext context)
        {
            _screenPosition = context.ReadValue<Vector2>();
        }

        public void OnMouse(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnMousePressed?.Invoke();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if(context.performed)
                OnAttackKeyPressed?.Invoke();
        }

        public Vector3 GetWorldPosition()
        {
            Camera mainCam = Camera.main;
            Debug.Assert(mainCam != null,"No main camera");
            Ray camRay = mainCam.ScreenPointToRay(_screenPosition);
            if (Physics.Raycast(camRay, out RaycastHit hit, mainCam.farClipPlane, whatIsGround))
            {
                _worldPosition = hit.point;
            }
            return _worldPosition;
        }
    }
}
