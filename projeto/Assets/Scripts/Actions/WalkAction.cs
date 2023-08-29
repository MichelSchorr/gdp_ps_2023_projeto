using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WalkAction: MonoBehaviour
{

    [SerializeField] private Rigidbody2D _rigidbody2D = null;
    //[SerializeField] private CharacterData _data;
    [SerializeField] private GroundCheck _groundCheck = null;

    [SerializeField] private bool conserveMomentum = true;
    [SerializeField, Range(0f, 100f)] private float _walkSpeed = 0;
    [SerializeField, Range(0f, 100f)] private float _maxWalkAccelerationUnits = 0;
    [SerializeField, Range(0f, 100f)] private float _maxWalkAirAccelerationUnits = 0;
    [SerializeField, Range(0f, 100f)] private float _maxWalkDecelerationUnits = 0;
    [SerializeField, Range(0f, 100f)] private float _maxWalkAirDecelerationUnits = 0;
    
    private Vector2 _targetVelocity, _SpeedDiff;
    private float _acceleration, _speedChange;
    
    public void Do(Vector2 direction)
    {
        _targetVelocity = new Vector2(direction.x, 0f) * Mathf.Max(_walkSpeed - _groundCheck.GetFriction(), 0f);

        //Checagem de OnGround e de se esta acelerando ou desacelerando
        if (_groundCheck.GetOnGround())
        {
            _acceleration = Mathf.Abs(_targetVelocity.x) > 0.01 ? _maxWalkAccelerationUnits : _maxWalkDecelerationUnits;
        }
        else
        {
            _acceleration = Mathf.Abs(_targetVelocity.x) > 0.01 ? _maxWalkAirAccelerationUnits : _maxWalkAirDecelerationUnits;
        }

        //Conservacao de Momento
        if (conserveMomentum && (Mathf.Abs(_rigidbody2D.velocity.x) > Mathf.Abs(_targetVelocity.x)) &&
            (Mathf.Sign(_rigidbody2D.velocity.x) == Mathf.Sign(_targetVelocity.x)) && (direction.x != 0f))
        {
            _acceleration = 0f;
        }

        _SpeedDiff = new Vector2(_targetVelocity.x - _rigidbody2D.velocity.x, 0f);
        
        
        _speedChange = _acceleration * _SpeedDiff.x;
        
        _rigidbody2D.AddForce(_speedChange * Vector2.right, ForceMode2D.Force);

    }
    
    
}
