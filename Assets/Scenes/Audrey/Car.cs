
using UnityEngine;


/// Source:https://www.youtube.com/watch?v=Z4HA8zJhGEk&t=587s&ab_channel=GameDevChef
/// Source:https://github.com/rNuv/self-driving-car-unity/blob/main/CarAgent.cs
/// Source:https://www.youtube.com/watch?v=m5WsmlEOFiA&list=PLk9uRGhVg-zll72IsBxi2kTTotRdmpV2N&index=14&ab_channel=samyam

public class Car : CarController
{
    private float horizontalI;
    private float verticalI;

    private PlayerControls controls;
    private Vector2 move;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void Update()
    {
        move = controls.Gameplay.Move.ReadValue<Vector2>();
        SetInputs(move.y, move.x);
        isBreaking = Input.GetKey(KeyCode.Joystick1Button7);
    }

    public void SetInputs(float verticulInput, float horizontalIput)
    {
        verticalI = verticulInput;
        horizontalI = horizontalIput;
    }

    private void FixedUpdate()
    {
        HandleMotor(verticalI);
        HandleSteering(horizontalI);
        UpdateWheels();
    }

    public void StopCompletely()
    {
        SetInputs(0f, 0f);
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
