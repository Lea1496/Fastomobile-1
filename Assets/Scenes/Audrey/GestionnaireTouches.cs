using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Users;
using UnityEngine.XR;
using InputDevice = UnityEngine.XR.InputDevice;

/// Source:https://www.youtube.com/watch?v=Z4HA8zJhGEk&t=587s&ab_channel=GameDevChef

public class GestionnaireTouches : BehaviourAuto
{
    private float horizontalInput;
    private float verticalInput;

    private PlayerControls controls;
    private Vector2 move;
    private CharacterController controller;
    private int playerNb = 1;
    private void Awake()
    {
        controls = new PlayerControls();
        if (gameObject.GetComponent<Player>().IsMainPlayer2)
        {
            playerNb = 2;
        }
        
        
    }

    private void Start()
    {
        
        Debug.Log(gameObject.GetComponent<Player>().IsMainPlayer2);            
        if (gameObject.GetComponent<Player>().IsMainPlayer2)                   
        {                                                                      
            playerNb = 2;                                                      
        }                                                                      
        Debug.Log(playerNb);                                                   
        Debug.Log(Gamepad.all[0]);                                             
        Debug.Log(Gamepad.all[1]);                                             
        if (gameObject.GetComponent<Player>().IsMainPlayer2)
        {
            Debug.Log("HERE");
            gameObject.GetComponent<PlayerInput>().defaultControlScheme = "keyboard";
            gameObject.GetComponent<PlayerInput>().defaultActionMap = "keyboardGameplay";
        }
     
    }
    private void Update()
    {
        if (Gamepad.all.Count > 0)
        {
            if (playerNb == 1)
            {
                move = Gamepad.all[0].leftStick.ReadValue();
                // move = Gamepad.current.leftStick.ReadValue();
//                Debug.Log(Gamepad.current);
                //Debug.Log(move);
               
            }
            else
            {
                move = Gamepad.all[1].leftStick.ReadValue();
            }

        }
        //move = controls.Gameplay.Move.ReadValue<Vector2>();

        
        
         
        

        
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
        if (Gamepad.all.Count > 0)
        {
            if (playerNb == 1)
            {
                if (Gamepad.all[0].rightTrigger.isPressed)
                {
                    isBreaking = true;
                }

                if (Gamepad.all[0].leftTrigger.IsPressed())
                {
                    isAccelerating = true;
                }
            }
            else
            {
                if (Gamepad.all[1].rightTrigger.IsPressed())
                {
                    isBreaking = true;
                }

                if (Gamepad.all[1].leftTrigger.IsPressed())
                {
                    isAccelerating = true;
                }
            }

        }
        horizontalInput = move.x;
        verticalInput = move.y;
        //isBreaking = controls.Gameplay.Break.IsPressed();
        //isAccelerating = controls.Gameplay.Accelerate.IsPressed();
        
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
