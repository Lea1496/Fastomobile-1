
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
    private int playerNb = 1;
    private Vector3 position;
    private Quaternion rotation;

    private float temps = 0;
    private WheelCollider[] wheelColliders;
    private bool estActif;
    private void Awake()
    {
        controls = new PlayerControls();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.centerOfMass = centerOfMass.transform.localPosition;
        rb.centerOfMass += Try;
        estActif = GetComponent<Player>().IsMainPlayer;
        if (gameObject.GetComponent<Player>().IsMainPlayer2)                   
        {                                                                      
            playerNb = 2;                                                      
        }                                                                      

        
        if (estActif)
        {
            wheelColliders = new WheelCollider[4]
                { frontLeftWheelCollider, frontRightWheelCollider, rearLeftWheelCollider, rearRightWheelCollider };
        }
        
    }

    public void FlipOver()
    {
        if (estActif)
        {
            //for (int i = 0; i < wheelColliders.Length; i++)
            {
                
               // Debug.Log(wheelColliders[i].isGrounded);
                //if (!wheelColliders[i].isGrounded && wheelColliders[i].transform.localPosition.y > 0.14 && temps > 1.5f)
                {
                   // Debug.Log("ROUE");
                    transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y + 5, transform.position.z), new Quaternion(0, transform.rotation.y, 0, transform.rotation.w));
                    temps = 0;
                    //break;
                }
            }
        }
        
    }
    
    private void Update()
    {
        temps += Time.deltaTime;
        //FlipOver();
        if (Gamepad.all.Count > 0)
        {
            /*if (playerNb == 1)
            {
                move = Gamepad.all[0].leftStick.ReadValue();
                if (Gamepad.all[0].circleButton.wasPressedThisFrame)
                {
                    position = transform.position;
                    rotation = transform.rotation;
                    transform.SetPositionAndRotation(new Vector3(position.x, position.y + 10, position.z), new Quaternion(0, rotation.y, 0, rotation.w));
                }
            }
            else
            {
                if (Gamepad.all.Count > 1)
                {
                    move = Gamepad.all[1].leftStick.ReadValue();
                    if (Gamepad.all[1].circleButton.wasPressedThisFrame)
                    {
                        position = transform.position;
                        rotation = transform.rotation;
                        transform.SetPositionAndRotation(new Vector3(position.x, position.y + 10, position.z), new Quaternion(0, rotation.y, 0, rotation.w));
                    }
                }

                
            }*/

        }
       
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
        //FlipOver();
        if (Mathf.Abs(Vector3.Dot(transform.up, Vector3.down)) > 0)
        {
            //FlipOver();
        }
        else
        {
            if(Mathf.Abs(Vector3.Dot(transform.up, Vector3.down)) < 0.125f)
            {
                // Car is primarily neither up nor down, within 1/8 of a 90 degree rotation
               // FlipOver();
                // Therefore, check whether it's on either side. Otherwise, it's on front/back
                if(Mathf.Abs(Vector3.Dot(transform.right, Vector3.down)) > 0.825f)
                {
                    // Car is within 1/8 of a 90 degree rotation of either side
                }
            }
        }
        
        //if (Gamepad.all.Count > 0)
        {
            if (playerNb == 1)
            {
                /*isBreaking = Gamepad.all[0].rightTrigger.IsPressed();

                isAccelerating = Gamepad.all[0].leftTrigger.IsPressed();
                if (rb.velocity.magnitude > 75)
                {
                    move = Vector2.zero;
                }
                else
                {
                    move = Gamepad.all[0].leftStick.ReadValue();
                }
                
                if (move.x == 0 && move.y == 0)
                {
                    ApplyBreaking();
                }
                if (Gamepad.all[0].circleButton.wasPressedThisFrame && temps > 5)
                {
                    position = transform.position;
                    rotation = transform.rotation;
                    transform.SetPositionAndRotation(new Vector3(position.x, position.y + 10, position.z), new Quaternion(0, rotation.y, 0, rotation.w));
                    temps = 0;
                }*/
                Bouger(0);
                if (move.x == 0 && move.y == 0)
                {
                    ApplyBreaking();
                }
            }
            else
            {
                Bouger(1);
                if (move.x == 0 && move.y == 0)
                {
                    ApplyBreaking();
                }
                /*if (Gamepad.all.Count > 1)
                {
                    isBreaking = Gamepad.all[1].rightTrigger.IsPressed();

                    isAccelerating = Gamepad.all[1].leftTrigger.IsPressed();
                    
                    move = Gamepad.all[1].leftStick.ReadValue();
                    if (move.x == 0 && move.y == 0)
                    {
                        ApplyBreaking();
                    }
                    if (Gamepad.all[1].circleButton.wasPressedThisFrame)
                    {
                        position = transform.position;
                        rotation = transform.rotation;
                        transform.SetPositionAndRotation(new Vector3(position.x, position.y + 10, position.z), new Quaternion(0, rotation.y, 0, rotation.w));
                        temps = 0;
                    }
                }*/
                
            }
            
            

        }
        horizontalInput = move.x;
        verticalInput = move.y;
        

    }

    private void Bouger(int ind)
    {
        if (Gamepad.all.Count > ind)
        {
            isBreaking = Gamepad.all[ind].rightTrigger.IsPressed();

            isAccelerating = Gamepad.all[ind].leftTrigger.IsPressed();
                    
            move = Gamepad.all[ind].leftStick.ReadValue();
            
            if (Gamepad.all[ind].circleButton.wasPressedThisFrame)
            {
                position = transform.position;
                rotation = transform.rotation;
                transform.SetPositionAndRotation(new Vector3(position.x, position.y + 10, position.z), new Quaternion(0, rotation.y, 0, rotation.w));
                temps = 0;
            }
        }
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
