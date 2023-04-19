using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[RequireComponent(typeof(Player))]

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
    private const int MOTO = 2;
    private Player joueur;

    public float vitesse = 0;
    void Start()
    {
        joueur = GetComponent<Player>();
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.transform.localPosition;
//        Poids = GameData.P1.GetComponentInParent<Player>().Poids;
// Puissance = GameData.P1.GetComponentInParent<Player>().Puissance;
        //AssignerColliders();
    }

    private float currentSteerAngle;
    private float currentbreakForce;
    private float currentAcceleration;
    public bool isBreaking;
    public bool isAccelerating;

    [SerializeField] private float accelerationForce; // reste à déterminer
    [SerializeField] private float breakForce; 
    [SerializeField] private float maxSteerAngle; 

    [SerializeField] WheelCollider frontLeftWheelCollider;
    [SerializeField] WheelCollider frontRightWheelCollider;
    [SerializeField] WheelCollider rearLeftWheelCollider;
    [SerializeField] WheelCollider rearRightWheelCollider;

    

    [SerializeField] Transform frontLeftWheelTransform;
    [SerializeField] Transform frontRightWheelTransform;
    [SerializeField] Transform rearLeftWheelTransform;
    [SerializeField] Transform rearRightWheelTransform;


    
    public void HandleMotor(float verticalI)
    {
       
        
        vitesse = verticalI * Puissance;
        frontLeftWheelCollider.motorTorque = verticalI * Puissance;
        frontRightWheelCollider.motorTorque = verticalI * Puissance;
        rearLeftWheelCollider.motorTorque = verticalI * Puissance;
        rearRightWheelCollider.motorTorque = verticalI * Puissance;
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
        currentAcceleration = isAccelerating ? accelerationForce/Poids : 0f; // parce F=m*a
        ApplyAcceleration(verticalI);
        
    }

    private void ApplyBreaking()
    {
       
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
        
    }

    private void ApplyAcceleration(float verticalI)
    {
        frontRightWheelCollider.motorTorque = verticalI * Puissance * currentAcceleration; 
        frontLeftWheelCollider.motorTorque = verticalI * Puissance * currentAcceleration;
       
    }

    public void HandleSteering(float horizontalI)
    {
        currentSteerAngle = maxSteerAngle * horizontalI;
        
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
        
       
    }

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
