using Blade.Entities;
using System;
using TMPro;
using UnityEngine;

public class CharacterMovement : MonoBehaviour, IEntityComponent
{
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float SprintSpeedMultiplier = 2f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private CharacterController controller;

    private Entity _entity;

    public void Initialize(Entity entity)
    {
        _entity = entity;
    }

    public bool isGround => controller.isGrounded;
    public bool CanManualMovement { get; set; } = true;
    private Vector3 _autoMovement;

    private Vector3 _velocity;
    public Vector3 Velocity => _velocity;
    private float _verticalVelocity;
    private Vector3 _movementDirection;

    public void SetMovementDirection(Vector3 movementInput)
    {
        _movementDirection = new Vector3(movementInput.x, 0, movementInput.z).normalized;
    }

    private void FixedUpdate()
    {
        CalculateMovement();
        ApplyGravity();
        Move();
    }
    private void CalculateMovement()
    {
        if (CanManualMovement)
        {
            _velocity = _movementDirection;
            _velocity *= moveSpeed * Time.fixedDeltaTime;
        }
        else
        {
            _velocity = _autoMovement * Time.fixedDeltaTime;
        }
        
        if (_velocity.magnitude > 0)
        {
            Quaternion targetRot = Quaternion.LookRotation(_velocity);
            float rotateSpeed = 8f;
            Transform parent = _entity.transform;
            parent.rotation = Quaternion.Lerp(parent.rotation, targetRot, Time.deltaTime * rotateSpeed);
        }
    }

    private void ApplyGravity()
    {
        if (isGround && _verticalVelocity < 0)
            _verticalVelocity = -0.03f;
        else
            _verticalVelocity += gravity * Time.fixedDeltaTime;

        _velocity.y = _verticalVelocity;
    }

    private void Move()
    {
        controller.Move(_velocity);
    }

    public void StopImmediately()
    {
        _movementDirection = Vector3.zero;
    }
    
}
