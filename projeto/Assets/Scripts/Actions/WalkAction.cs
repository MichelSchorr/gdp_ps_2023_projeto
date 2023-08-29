using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WalkAction: MonoBehaviour
{

    [SerializeField] private Rigidbody2D _rigidbody2D;
    //[SerializeField] private CharacterData _data;
    [SerializeField] private GroundCheck _groundCheck;
    
    [SerializeField, Range(0f, 100f)] private float _walkSpeed ;
    [SerializeField, Range(0f, 100f)] private float _maxWalkAccelerationUnits;
    [SerializeField, Range(0f, 100f)] private float _maxWalkAirAccelerationUnits;

    private Vector2 _targetVelocity;
    private Vector2 _newVelocity;
    private float _acceleration;
    
    //[SerializeField, Range(0f, 100f)] private float _maxSpeed;
    //[SerializeField, Range(0f, 100f)] private float _maxAccelerationUnits;
    //[SerializeField, Range(0f, 100f)] private float _maxAirAccelerationUnits;

    //private Vector2 _direction;

    /*
    public void Initialize(CharacterData data)
    {
        _data = data;
    }
    */
    
    public void Do(Vector2 direction)
    {
        _targetVelocity = new Vector2(direction.x, 0f) * Mathf.Max(_walkSpeed - _groundCheck.GetFriction(), 0f);

        _newVelocity = _rigidbody2D.velocity;
        
        _acceleration = _groundCheck.GetOnGround()
            ? _maxWalkAccelerationUnits
            : _maxWalkAirAccelerationUnits;

        _newVelocity.x = Mathf.MoveTowards(_newVelocity.x, _targetVelocity.x, _acceleration);
        _rigidbody2D.velocity = _newVelocity;

    }
    
    
}
