using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// Source:https://www.youtube.com/watch?v=Z4HA8zJhGEk&t=587s&ab_channel=GameDevChef

public class GestionnaireTouches : BehaviourAuto
{
    private float horizontalInput;
    private float verticalInput;

    private PlayerControls controls;
    private Vector2 move;
    private CharacterController controller;
    private void Awake()
    {
        
        controls = new PlayerControls();
    }

    private void Start()
    {
        if (gameObject.GetComponent<Player>().IsMainPlayer2)
        {
            Debug.Log("HERE");
            gameObject.GetComponent<PlayerInput>().defaultControlScheme = "keyboard";
            gameObject.GetComponent<PlayerInput>().defaultActionMap = "keyboardGameplay";
        }
    }

    private void Update()
    {
        move = controls.Gameplay.Move.ReadValue<Vector2>();
        
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor(verticalInput);
        HandleSteering(horizontalInput);
        UpdateWheels();
    }

    private void GetInput()
    {  
        horizontalInput = move.x;
        verticalInput = move.y;
       // Debug.Log(verticalInput);
        isBreaking = controls.Gameplay.Break.IsPressed();
        isAccelerating = controls.Gameplay.Accelerate.IsPressed();
        
    }
    private void OnEnable()
    {
        controls.Enable();
    }

    
    private void OnDisable()
    {
        controls.Disable();
    }
}
