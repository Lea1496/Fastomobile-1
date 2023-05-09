
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private int nbPlayer;
    private List<Gamepad> gamepads;
    private List<bool> isGamepadConnected;
    
    private float temps = 0;
    private float tempsDepuisDébut;
    
    private WheelCollider[] wheelColliders;
    private bool estActif;
    private void Awake()
    {
        controls = new PlayerControls();
    }
    void Start()
    {
        nbPlayer = Gamepad.all.Count;
        gamepads = new List<Gamepad>(nbPlayer);
        isGamepadConnected = new List<bool>(nbPlayer);
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            gamepads.Add(Gamepad.all[i]);
            isGamepadConnected.Add(true);
        }
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

    
    private void Update()
    {
        temps += Time.deltaTime;
        tempsDepuisDébut += Time.deltaTime;
        
       
    }

    private void FixedUpdate()
    {
        //Vérifie que les gamepads ne "switch" pas si on les déconnecte
        if (Gamepad.all.Count != nbPlayer)
        {
            VérifierGamepads();
        }
        GetInput();
        if (tempsDepuisDébut < 2 && verticalInput != 0)
        {
            ApplyAccelerationCustom(4f);
            HandleSteering(horizontalInput);
            //HandleMotor();
        }
        else
        {
           
            if (horizontalInput == 0 && verticalInput == 0 )
            {
                ApplyBreakingCustom(30000);
                HandleSteering(horizontalInput);
            }
            else
            {
                HandleMotor(verticalInput);
                HandleSteering(horizontalInput);
            }

            if (rb.velocity.magnitude * 2.237 > 175)
            {
                ApplyBreakingCustom(3000000);
            }
        }

        
        
        ApplyDownForce();
        UpdateWheels();
        //ApplyDownForce();
    }
    public void ApplyAccelerationCustom(float verticalI)
    {
        
        frontRightWheelCollider.motorTorque = verticalI * Puissance * 500; 
        frontLeftWheelCollider.motorTorque = verticalI * Puissance * 500;
        rearLeftWheelCollider.motorTorque = verticalI * Puissance * 500;
        rearRightWheelCollider.motorTorque = verticalI * Puissance * 500;
    }
    public void ApplyBreakingCustom(int breakForce)
    {
        
        frontRightWheelCollider.brakeTorque = breakForce;
        frontLeftWheelCollider.brakeTorque = breakForce;
        rearLeftWheelCollider.brakeTorque = breakForce;
        rearRightWheelCollider.brakeTorque = breakForce;
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
        
       
        if (playerNb == 1)
        {
            Bouger(0);
            
        }
        else
        {
            Bouger(1);
        }
        horizontalInput = move.x;
        verticalInput = move.y;
        

    }

    private void Bouger(int ind)
    {
        if (gamepads.Count > ind )
        {
            if (isGamepadConnected[ind]) 
            {
                isAccelerating = gamepads[ind].rightTrigger.IsPressed();

                isBreaking = gamepads[ind].leftTrigger.IsPressed();
                    
                move = gamepads[ind].leftStick.ReadValue();

                /*if (isBreaking)
                {
                    move = Vector2.zero;
                }*/
                if (gamepads[ind].circleButton.wasPressedThisFrame && temps > 5)
                {
                    FlipOver();
                }
            }
        }
    }
    public void FlipOver()
    {
        position = transform.position;
        rotation = transform.rotation;
        transform.SetPositionAndRotation(new Vector3(position.x, position.y + 5, position.z), new Quaternion(0, rotation.y, 0, rotation.w));
        temps = 0;
    }

    private void VérifierGamepads()
    {
        if (GameData.P2.IsMainPlayer && gamepads.Count == 1 && Gamepad.all.Count == 2 && isGamepadConnected[0])
        {
            Debug.Log("co2");
            gamepads.Add(Gamepad.all[1]);
            isGamepadConnected.Add(true);
            nbPlayer = 2;
        }
        else
        {
            if (nbPlayer == 0 && gamepads.Count == 0)
            {
                for (int i = 0; i < Gamepad.all.Count; i++)
                {
                    Debug.Log("coTout");
                    gamepads.Add(Gamepad.all[i]);
                    isGamepadConnected.Add(true);
                }
                nbPlayer = Gamepad.all.Count;
            }
            else 
            {

                if (Gamepad.all.Count < gamepads.Count)
                {
                    if (nbPlayer == 2)
                    {
                        if (gamepads[1] == Gamepad.all[0])
                        {
                            Debug.Log("deco1");
                            isGamepadConnected[0] = false;
                            
                        }
                        else
                        {
                            Debug.Log("deco2");
                            isGamepadConnected[1] = false;
                        }

                        
                    }
                    else if (Gamepad.all.Count == 0)
                    {
                        for (int i = 0; i < isGamepadConnected.Count; i++)
                        {
                            Debug.Log("decoTOut");
                            isGamepadConnected[i] = false;
                        }
                    }
                    
                    nbPlayer = Gamepad.all.Count;
                }
                else
                {
                    if (gamepads[0] == Gamepad.all[0] && !isGamepadConnected[0])
                    {
                        Debug.Log("co1");
                        isGamepadConnected[0] = true;
                    }
                    else if (gamepads.Count == 2)
                    {
                        if ((gamepads[1] == Gamepad.all[1] ||(!isGamepadConnected[0] && gamepads[1] == Gamepad.all[0])) && !isGamepadConnected[1])
                        {
                            Debug.Log("co22");
                            isGamepadConnected[1] = true;
                        }
                        else if (gamepads[0] == Gamepad.all[1] && !isGamepadConnected[0])
                        {
                            Debug.Log("co12");
                            isGamepadConnected[0] = true;
                        }
                    }
                
                    nbPlayer = Gamepad.all.Count;
                }
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
