using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData: MonoBehaviour
{
    [SerializeField, Range(0f, 100f)] private float _walkSpeed ;
    [SerializeField, Range(0f, 100f)] private float _maxWalkAccelerationUnits;
    [SerializeField, Range(0f, 100f)] private float _maxWalkAirAccelerationUnits;



    public float GetBaseSpeed()
    {
        return _walkSpeed;
    }
    
    public float GetMaxAccelerationUnits()
    {
        return _maxWalkAccelerationUnits;
    }
    
    public float GetMaxAirAccelerationUnits()
    {
        return _maxWalkAirAccelerationUnits;
    }
}
