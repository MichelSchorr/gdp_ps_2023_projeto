using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class CharacterController : MonoBehaviour
{
        
    //[SerializeField] private CharacterData _data;
    [SerializeField] private InputControllerTutorial inputController = null;
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private GroundCheck groundCheck = null;


    [SerializeField, Range(-5f, 0f)] private float fallingTriggerThreshold;
    [SerializeField, Range(-10f, 10f)] private float fallingGravityMultiplier = 1f;
    
    
    [SerializeField, Range(-10f, 10f)] private float jumpLetGoGravityMultiplier = 1f;
    [SerializeField, Range(0f, 10f)] public float defaultGravityScale;


    [SerializeField, Range(-5, 0)] private float lowerAirtimeBoundary;
    [SerializeField, Range(0, 5)] private float upperAirtimeBoundary;
    [SerializeField, Range(0f, 10f)] private float airtimeGravityMultiplier;
    
    [SerializeField, Range(-50f, 50f)] private float maxFallSpeed = -40;
    
    
    public UnityEvent<float> walk;
    public UnityEvent jump;
    public UnityEvent airJump;
    public UnityEvent airJumpReset;
    
    private float _horizontalDirection;
    private bool _desiredJump, _heldJump, _onGround;
    


    
    
    private void Start()
    {
        rigidbody2D.gravityScale = defaultGravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        _desiredJump = _desiredJump||inputController.RetrieveJumpInput();
        _heldJump = inputController.RetrieveJumpHold();
        
        _horizontalDirection = inputController.RetrieveMoveInput();
        
    }

    private void FixedUpdate()
    {

        _onGround = groundCheck.GetOnGround();
        
        rigidbody2D.gravityScale = defaultGravityScale;
        //Aceleracao quando solta o jump
        if (!_heldJump)
        {
            rigidbody2D.gravityScale *= jumpLetGoGravityMultiplier;
        }
        //Aceleracao em queda (sobreescreve a aceleracao quando solta o jump)
        if (rigidbody2D.velocity.y < fallingTriggerThreshold)
        {
            rigidbody2D.gravityScale = defaultGravityScale * fallingGravityMultiplier;
        }
        //Desaceleracao quando no apice do salto
        if (lowerAirtimeBoundary < rigidbody2D.velocity.y && rigidbody2D.velocity.y < upperAirtimeBoundary && !_onGround)
        {
            rigidbody2D.gravityScale = defaultGravityScale * airtimeGravityMultiplier;
        }
        
        
        //Jump
        if (_onGround)
        {
            airJumpReset.Invoke();
        }
        
        if(_desiredJump)
        {
            if (_onGround)
            {
                jump.Invoke();
            }
            else
            {
                airJump.Invoke();
            }
        }
        _desiredJump = false;
        
        if (rigidbody2D.velocity.y < maxFallSpeed)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x,maxFallSpeed);
        }
        
        
        
        //Walk
        walk.Invoke(_horizontalDirection);
        
        
        
    }
}
