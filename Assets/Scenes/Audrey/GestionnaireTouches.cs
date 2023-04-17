using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// Source:https://www.youtube.com/watch?v=Z4HA8zJhGEk&t=587s&ab_channel=GameDevChef

public class GestionnaireTouches : BehaviourAuto
{
    private float horizontalInput;
    private float verticalInput;

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor(verticalInput);
        HandleSteering(horizontalInput);
        UpdateWheels();
    }

    private void GetInput()
    {
        
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBreaking = Input.GetKey(KeyCode.Joystick1Button6);
        isAccelerating = Input.GetKey(KeyCode.Joystick1Button7);
        
    }
}
