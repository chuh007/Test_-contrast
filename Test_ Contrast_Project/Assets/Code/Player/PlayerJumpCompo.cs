using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class PlayerJumpCompo : MonoBehaviour
{
    private bool _isJumping;
    private Vector3 _jumpStart;
    private Vector3 _jumpEnd;
    private float _jumpDuration = 0.5f;
    private float _jumpTimer = 0f;

    private Transform _playerTransform;
    private NavMeshAgent _agent;

    public void Initialize(Transform playerTransform, NavMeshAgent agent)
    {
        _playerTransform = playerTransform;
        _agent = agent;
    }

    public void StartJump(Vector3 start, Vector3 end)
    {
        _jumpStart = start;
        _jumpEnd = end;
        _jumpTimer = 0f;
        _isJumping = true;
        _agent.isStopped = true;
    }

    public void UpdateJump()
    {
        if (_isJumping)
        {
            _jumpTimer += Time.deltaTime;
            float t = Mathf.Clamp01(_jumpTimer / _jumpDuration);
            
            Vector3 pos = Vector3.Lerp(_jumpStart, _jumpEnd, t);
            pos.y += Mathf.Sin(t * Mathf.PI) * 2.0f; 

            _playerTransform.position = pos;
            
            Vector3 direction = (_jumpEnd - _jumpStart);
            direction.y = 0;
            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);
                _playerTransform.rotation = Quaternion.Slerp(
                    _playerTransform.rotation,
                    targetRotation,
                    Time.deltaTime * 10f
                );
            }

            if (t >= 1.0f)
            {
                _isJumping = false;
                _agent.CompleteOffMeshLink();
                _agent.isStopped = false;
            }
        }
    }

    public bool IsJumping => _isJumping;
    
}
