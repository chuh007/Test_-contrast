using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace Code.Player
{
    public class PlayerControllers : MonoBehaviour
    {
        [SerializeField] private PlayerInputSO inputSO;
        [SerializeField] private PlayerMovement playerMovement;
        private Player _player;

        private void Awake()
        {
            _player = GetComponent<Player>();
        }

        private void OnEnable()
        {
            inputSO.OnMouseStatusChange += HandleClick;
        }

        private void OnDisable()
        {
            inputSO.OnMouseStatusChange -= HandleClick;
        }

        private void HandleClick(bool isPressed)
        {
            if (isPressed == false) return;
            if (_player.GetCurrentState() == "ATTACK" || _player.GetCurrentState() == "JUMP")
                return;
            
            _player.ChangeState("MOVE");   
            
            if (inputSO.GetWorldPosition(out Vector3 clickPoint))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit rayHit, 100f))
                {
                    GameObject targetObject = rayHit.collider.gameObject;
                    
                    if (targetObject.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                    {
                        _player.Attack(targetObject);
                        return;
                    }
                }

                if (NavMesh.SamplePosition(clickPoint, out NavMeshHit hit, 1f, NavMesh.AllAreas))
                {
                    _player.MoveToPosition(hit.position);
                }
            }
        }

        
    }
}
