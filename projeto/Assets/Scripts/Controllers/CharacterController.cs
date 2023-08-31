using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
        
    [SerializeField] private CharacterData _data;
    [SerializeField] private InputControllerTutorial input = null;
    
    [SerializeField] private WalkAction _walkAction = null;
    [SerializeField] private JumpAction _jumpAction = null;
    
    private Vector2 horizontalDirection;
    private bool desiredJump;


    private void Awake()
    {
        /*
        _walkAction = new WalkAction();
        _walkAction.Initialize(_data);
        */
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        desiredJump = desiredJump||input.RetrieveJumpInput();
        horizontalDirection.x = input.RetrieveMoveInput();
    }

    private void FixedUpdate()
    {
        _jumpAction.Do(desiredJump);
        desiredJump = false;
        _walkAction.Do(horizontalDirection);
    }
}
