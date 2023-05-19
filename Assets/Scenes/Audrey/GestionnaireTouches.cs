
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


///<summary>
/// Cette classe permet d'utiliser deux manettes pour controler les joueurs
///
/// Cette fonction est sur l'objet du joueur
/// </summary>

public class GestionnaireTouches : BehaviourAuto
{
    private float horizontalInput;
    private float verticalInput;
    
    private Vector2 move;
    private CharacterController controller;
    private int playerNb = 1;
    private Vector3 position;
    private Quaternion rotation;

    
    private List<Gamepad> gamepads;

    private float temps = 0;
    private float tempsDepuisDébut;
    
    
    void Start()
    {

        gamepads = new List<Gamepad>();
        //Crée une liste de gamepads avec tous les gamepads connectés
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            gamepads.Add(Gamepad.all[i]);
        }
           
        //Met le centre de gravité de l'auto à la bonne place
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass += posCentreGrav;
    
        //Si l'objet sur lequel ce code est est le joueur 2 
        if (gameObject.GetComponent<Player>().IsMainPlayer2)                   
        {                                                                      
            playerNb = 2;                                                      
        }                                                                      
   }

    
    private void Update()
    {
        temps += Time.deltaTime;
        tempsDepuisDébut += Time.deltaTime;
    }

    private void FixedUpdate()
    {
        
        GetInput();
        
        //Quand la partie commence, j'applique automatiquement un boost aux joueurs, parce que le début 
        //pouvait être lent des fois
        if (tempsDepuisDébut < 2 && verticalInput != 0)
        {
            ApplyAccelerationCustom(4f);
            HandleSteering(horizontalInput);
        }
        else
        {
           //Si le joueur ne touche pas à sa manette je le fait ralentir parce que des fois il accélérait à l'infini
           // ce qui rendait la conduite difficile
            if (horizontalInput == 0 && verticalInput == 0 )
            {
                ApplyBreakingCustom(60000);
                HandleSteering(horizontalInput);
            }
            else
            {
                //Appelle les fonctions du code de Audrey qui gèrent les Wheel colliders pour faire avancer l'auto
                HandleMotor(verticalInput);
                HandleSteering(horizontalInput);
            }

            //Va ralentir l'auto si elle commence à aller trop vite, car après une certaine vitesse elle devient
            // très difficile à controler
            if (rb.velocity.magnitude * 2.237 > 200)
            {
                ApplyBreakingCustom(3000000);
            }
        }
        ApplyDownForce();
        UpdateWheels();
    }
    
    //Cette fonction permet d'appliquer une accélération avec un "verticalInput" de notre choix (ainsi on peut faire une plus grande accélération)
    public void ApplyAccelerationCustom(float verticalI)
    {
        frontRightWheelCollider.motorTorque = verticalI * Puissance * 500; 
        frontLeftWheelCollider.motorTorque = verticalI * Puissance * 500;
        rearLeftWheelCollider.motorTorque = verticalI * Puissance * 500;
        rearRightWheelCollider.motorTorque = verticalI * Puissance * 500;
    }
    
    //Cette fonction permet de freiner avec une force de notre choix
    public void ApplyBreakingCustom(int breakForce)
    {
        
        frontRightWheelCollider.brakeTorque = breakForce;
        frontLeftWheelCollider.brakeTorque = breakForce;
        rearLeftWheelCollider.brakeTorque = breakForce;
        rearRightWheelCollider.brakeTorque = breakForce;
    }
    //Va appeler la fonction qui fait bouger les autos dépendemment du joueur sur qui ce code est
    private void GetInput()
    {
        if (playerNb == 1)
        {
            Bouger(0);
        }
        else
        {
            Bouger(1);
        }
        
        //Attribue les valeurs du joystick
        horizontalInput = move.x;
        verticalInput = move.y;
    }

    //Cette fonction va prendre les inputs que le joueur va lui donner pour faire bouger l'auto
    private void Bouger(int ind)
    {
        if (gamepads.Count > ind )
        {
            
            isAccelerating = gamepads[ind].rightTrigger.IsPressed();

            isBreaking = gamepads[ind].leftTrigger.IsPressed();
                
            move = gamepads[ind].leftStick.ReadValue();
            
            //la limite de temps s'assure que le joueur ne puisse pas commencer à voler en pesant le bouton à répétition
            if (gamepads[ind].circleButton.wasPressedThisFrame && temps > 5)
            {
                FlipOver();
            }
            
        }
    }
    
    //Permet de retourner l'auto dans le bon sens, si elle tombe sur le côté
    public void FlipOver()
    {
        position = transform.position;
        rotation = transform.rotation;
        transform.SetPositionAndRotation(new Vector3(position.x, position.y + 5, position.z), new Quaternion(0, rotation.y, 0, rotation.w));
        temps = 0;
    }
    
}
/*
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
    */