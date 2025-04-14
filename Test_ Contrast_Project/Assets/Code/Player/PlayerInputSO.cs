using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Player
{
    [CreateAssetMenu(fileName = "PlayerInput", menuName = "SO/PlayerInput", order = 0)]
    public class PlayerInputSO : ScriptableObject, Controls.IPlayerActions
    {
        [SerializeField] private LayerMask whatIsGround;

        public event Action<bool> OnMouseStatusChange;
        
        public Vector2 MovementKey { get; private set; }
        
        private Controls _control;
        private Vector2 _mousePosition;
        private Vector3 _worldPosition;
        
        private void OnEnable()
        {
            if (_control == null)
            {
                _control = new Controls();
                _control.Player.SetCallbacks(this);
            }
            _control.Player.Enable();
        }

        private void OnDisable()
        {
            _control.Player.Disable();
        }
        
        public void OnMove(InputAction.CallbackContext context)
        {
            MovementKey = context.ReadValue<Vector2>();
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnMouseStatusChange?.Invoke(true);
            if (context.canceled)
                OnMouseStatusChange?.Invoke(false);
        }
        
        public bool GetWorldPosition(out Vector3 worldPos)
        {
            Camera mainCam = Camera.main;
            Ray camRay = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(camRay, out RaycastHit hit, mainCam.farClipPlane, whatIsGround))
            {
                worldPos = hit.point;
                return true;
            }

            worldPos = Vector3.zero;
            return false;
        }
        
    }    
}

