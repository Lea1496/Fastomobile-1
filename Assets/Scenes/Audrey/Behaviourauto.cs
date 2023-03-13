using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Player))]
public class BehaviourAuto : MonoBehaviour
{ 
    //source.https://www.youtube.com/watch?v=Z4HA8zJhGEk&t=183s&ab_channel=GameDevChef
    //savoir valeur de la vitesse pour l'odomètre

    // faire un if pour la moto car deux roues et non quatre

    private int Poids;
    private int Puissance;
    private const int MOTO = 2;   

    void Start()
    {
        Poids = GameData.P1.GetComponent<Player>().Poids;
        Puissance = GameData.P1.GetComponent<Player>().Puissance;
    }

    private float currentSteerAngle;
    private float currentbreakForce;
    private float currentAcceleration;
    public bool isBreaking;
    public bool isAccelerating;

    [SerializeField] private float accelerationForce; // reste à déterminer
    [SerializeField] private float breakForce; 
    [SerializeField] private float maxSteerAngle; 

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private WheelCollider frontWheelCollider;
    [SerializeField] private WheelCollider rearWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    [SerializeField] private Transform frontWheelTransfrom;
    [SerializeField] private Transform rearWheelTransfrom;

    public void HandleMotor(float verticalI)
    {
        if(GameData.P1.IdVéhicule != MOTO)
        {
            frontLeftWheelCollider.motorTorque = verticalI * Puissance;
            frontRightWheelCollider.motorTorque = verticalI * Puissance;
            currentbreakForce = isBreaking ? breakForce : 0f;
            ApplyBreaking();
            currentAcceleration = isAccelerating ? accelerationForce/Poids : 0f; // parce F=m*a
            ApplyAcceleration(verticalI);
        }
        else
        {
            frontWheelCollider.motorTorque = verticalI * Puissance;
            currentbreakForce = isBreaking ? breakForce : 0f;
            ApplyBreaking();
            currentAcceleration = isAccelerating ? accelerationForce/Poids : 0f;
            ApplyAcceleration(verticalI);
        }
    }

    private void ApplyBreaking()
    {
        if (GameData.P1.IdVéhicule != MOTO)
        {
            frontRightWheelCollider.brakeTorque = currentbreakForce;
            frontLeftWheelCollider.brakeTorque = currentbreakForce;
            rearLeftWheelCollider.brakeTorque = currentbreakForce;
            rearRightWheelCollider.brakeTorque = currentbreakForce;
        }
        else
        {
            frontWheelCollider.brakeTorque = currentbreakForce;
            rearWheelCollider.brakeTorque = currentbreakForce;
        }
    }

    private void ApplyAcceleration(float verticalI)
    {
        if (GameData.P1.IdVéhicule != MOTO)
        {
            frontRightWheelCollider.motorTorque = verticalI * Puissance * currentAcceleration;
            frontLeftWheelCollider.motorTorque = verticalI * Puissance * currentAcceleration;
        }
        else
        {
            frontWheelCollider.brakeTorque = verticalI * Puissance * currentAcceleration;
        }
    }

    public void HandleSteering(float horizontalI)
    {
        currentSteerAngle = maxSteerAngle * horizontalI;
        if(GameData.P1.IdVéhicule != MOTO)
        {
            frontLeftWheelCollider.steerAngle = currentSteerAngle;
            frontRightWheelCollider.steerAngle = currentSteerAngle;
        }
        else
        {
            frontWheelCollider.steerAngle = currentSteerAngle;
        }  
    }

    public void UpdateWheels()
    {
        if(GameData.P1.IdVéhicule != MOTO)
        {
            UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
            UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
            UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
            UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
        }
        else
        {
            UpdateSingleWheel(frontWheelCollider, frontWheelTransfrom);
            UpdateSingleWheel(rearWheelCollider, rearWheelTransfrom);
        }       
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
