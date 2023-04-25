using System.Collections;
using System.Collections.Generic;
using TreeEditor;
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
        
        if (gameObject.GetComponent<Player>().IsMainPlayer2)                   
        {                                                                      
            playerNb = 2;                                                      
        }                                                                      
        
     
    }
    
    private void Update()
    {
        if (Gamepad.all.Count > 0)
        {
            if (playerNb == 1)
            {
                move = Gamepad.all[0].leftStick.ReadValue();
                if (Gamepad.all[0].circleButton.wasPressedThisFrame)
                {
                    transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), new Quaternion(0, transform.rotation.y, 0, transform.rotation.w));
                }
            }
            else
            {
                move = Gamepad.all[1].leftStick.ReadValue();
                if (Gamepad.all[1].circleButton.wasPressedThisFrame)
                {
                    transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), new Quaternion(0, transform.rotation.y, 0, transform.rotation.w));
                }
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
        //ApplyDownForce();
    }

    private void GetInput()
    {
        if (Gamepad.all.Count > 0)
        {
            if (playerNb == 1)
            {
                isBreaking = Gamepad.all[0].rightTrigger.IsPressed();

                isAccelerating = Gamepad.all[0].leftTrigger.IsPressed();
            }
            else
            {
                isBreaking = Gamepad.all[1].rightTrigger.IsPressed();

                isAccelerating = Gamepad.all[1].leftTrigger.IsPressed();
            }

        }
        horizontalInput = move.x;
        verticalInput = move.y;
        

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
