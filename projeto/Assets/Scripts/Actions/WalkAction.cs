using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


//[CreateAssetMenu(fileName = "New Walk Action", menuName = "Action/Walk")]
public class WalkAction: Action
{
    
    [SerializeField] private new Rigidbody2D rigidbody2D = null;
    [SerializeField] private GroundCheck groundCheck = null;
    
    [SerializeField, Range(0f, 100f)] private float walkSpeed = 0;
    
    [SerializeField, Range(0f, 100f)] private float maxWalkAccelerationUnits = 0;
    [SerializeField, Range(0f, 100f)] private float maxWalkDecelerationUnits = 0;
    [SerializeField, Range(0f, 100f)] private float maxWalkAirAccelerationUnits = 0;
    [SerializeField, Range(0f, 100f)] private float maxWalkAirDecelerationUnits = 0;
    
    [SerializeField] private bool conserveMomentum = false;
    
    private float _acceleration, _speedChange, _targetVelocity, _speedDiff;
    
    
    
    
    
    public void Do(float direction)
    {
        _targetVelocity = direction * Mathf.Max(walkSpeed - groundCheck.GetFriction(), 0f);

        
        //Checagem de OnGround e de se esta acelerando ou desacelerando
        if (groundCheck.GetOnGround())
        {
            _acceleration = Mathf.Abs(_targetVelocity) > 0.01 ? maxWalkAccelerationUnits : maxWalkDecelerationUnits;
        }
        else
        {
            _acceleration = Mathf.Abs(_targetVelocity) > 0.01 ? maxWalkAirAccelerationUnits : maxWalkAirDecelerationUnits;
        }

        
        //Conservacao de Momento
        if (conserveMomentum && (Mathf.Abs(rigidbody2D.velocity.x) > Mathf.Abs(_targetVelocity)) &&
            (Mathf.Sign(rigidbody2D.velocity.x) == Mathf.Sign(_targetVelocity)) && (direction != 0f))
        {
            _acceleration = 0f;
        }

        
        _speedDiff = _targetVelocity - rigidbody2D.velocity.x;
        
        _speedChange = _acceleration * _speedDiff * rigidbody2D.mass;
        
        
        rigidbody2D.AddForce(_speedChange * Vector2.right, ForceMode2D.Force);
    }
}
