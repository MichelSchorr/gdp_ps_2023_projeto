using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
        
    [SerializeField] private CharacterData _data;
    [SerializeField] private InputControllerTutorial input = null;
    [SerializeField] private WalkAction _walkAction;
    
    private Vector2 horizontalDirection;


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
        horizontalDirection.x = input.RetrieveMoveInput();
    }

    private void FixedUpdate()
    {
        _walkAction.Do(horizontalDirection);
    }
}
