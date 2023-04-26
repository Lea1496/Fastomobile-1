

using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem;

/// Source:https://www.youtube.com/watch?v=Z4HA8zJhGEk&t=587s&ab_channel=GameDevChef


public class BehaviourAuto : MonoBehaviour
{
    //source.https://www.youtube.com/watch?v=Z4HA8zJhGEk&t=183s&ab_channel=GameDevChef
    //savoir valeur de la vitesse pour l'odomètre

    // faire un if pour la moto car deux roues et non quatre
    public GameObject centerOfMass;
    public Rigidbody rb;

    public int Poids;
    public int Puissance;

    private CharacterController controller;
    public Vector3 Try = new Vector3(1, -0.2f, 0);
    
    

    private float currentSteerAngle;
    private float currentbreakForce;
    private float currentAcceleration;
    public bool isBreaking;
    public bool isAccelerating;
    private float downForceValue = 200f;

    [SerializeField] private float accelerationForce; // reste à déterminer
    [SerializeField] private float breakForce; 
    [SerializeField] private float maxSteerAngle; 

    [SerializeField] public WheelCollider frontLeftWheelCollider;
    [SerializeField] public WheelCollider frontRightWheelCollider;
    [SerializeField] public WheelCollider rearLeftWheelCollider;
    [SerializeField] public WheelCollider rearRightWheelCollider;

    private WheelCollider[] wheelColliders;

    [SerializeField]  Transform frontLeftWheelTransform;
    [SerializeField]  Transform frontRightWheelTransform;
    [SerializeField]  Transform rearLeftWheelTransform;
    [SerializeField]  Transform rearRightWheelTransform;

    private bool estActif;

    
    public void HandleMotor(float verticalI)
    {
        frontLeftWheelCollider.motorTorque = verticalI * Puissance;
        frontRightWheelCollider.motorTorque = verticalI * Puissance;
        rearLeftWheelCollider.motorTorque = verticalI * Puissance;
        rearRightWheelCollider.motorTorque = verticalI * Puissance;
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
        currentAcceleration = isAccelerating ? accelerationForce/Poids : 0f; // parce F=m*a
        ApplyAcceleration(verticalI);
        
    }

    public void ApplyBreaking()
    {
       
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
        
    }

    public void ApplyAcceleration(float verticalI)
    {
        frontRightWheelCollider.motorTorque = verticalI * Puissance * currentAcceleration; 
        frontLeftWheelCollider.motorTorque = verticalI * Puissance * currentAcceleration;
       
    }

    /*public void ApplyDownForce()
    {
        rb.AddForce(-transform.up * downForceValue * - rb.velocity.magnitude);
    }*/
    
    public void HandleSteering(float horizontalI)
    {
        currentSteerAngle = maxSteerAngle * horizontalI;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
        
       
    }

   /* public void FlipOver()
    {
        if (estActif)
        {
            for (int i = 0; i < wheelColliders.Length; i++)
            {
                Debug.Log(wheelColliders[i].isGrounded);
                if (!wheelColliders[i].isGrounded)
                {
                    Debug.Log("ROUE");
                    transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), new Quaternion(0, transform.rotation.y, 0, transform.rotation.w));
                    break;
                }
            }
        }
        
    }*/
    public void UpdateWheels()
    {
        
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        
            
    }

    public void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}
