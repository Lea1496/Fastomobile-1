using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[RequireComponent(typeof(Player))]

public class BehaviourAuto : MonoBehaviour
{ 
    //source.https://www.youtube.com/watch?v=Z4HA8zJhGEk&t=183s&ab_channel=GameDevChef
    //savoir valeur de la vitesse pour l'odomètre

    // faire un if pour la moto car deux roues et non quatre

    public int Poids;
    public int Puissance;
    private const int MOTO = 2;
    private Player joueur;

    void Start()
    {
        joueur = GetComponent<Player>();
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

    [SerializeField] WheelCollider frontWheelCollider;
    [SerializeField] WheelCollider rearWheelCollider;

    [SerializeField] Transform frontLeftWheelTransform;
    [SerializeField] Transform frontRightWheelTransform;
    [SerializeField] Transform rearLeftWheelTransform;
    [SerializeField] Transform rearRightWheelTransform;

    [SerializeField] private Transform frontWheelTransfrom;
    [SerializeField] private Transform rearWheelTransfrom;

    /*private void AssignerColliders()
    {
        WheelCollider[] lesColliders = GetComponentsInChildren<WheelCollider>();
        Transform[] lesTransforms = GetComponentsInChildren<Transform>();
        for (int i = 0; i < lesColliders.Length; i++)
        {
            if (lesColliders[i].gameObject.name == "left_front_wheel")
            {
                frontLeftWheelCollider = lesColliders[i];
                frontLeftWheelTransform = lesTransforms[i];
            }
            else
            {
                if (lesColliders[i].gameObject.name == "right_front_wheel")
                {
                    frontRightWheelCollider = lesColliders[i];
                    frontRightWheelTransform = lesTransforms[i];
                }
                else
                {
                    if (lesColliders[i].gameObject.name == "left_rear_wheel")
                    {
                        rearLeftWheelCollider = lesColliders[i];
                        rearLeftWheelTransform = lesTransforms[i];

                    }
                    else
                    {
                        if (lesColliders[i].gameObject.name == "right_rear_wheel")
                        {
                            rearRightWheelCollider = lesColliders[i];
                            rearRightWheelTransform = lesTransforms[i];
                        }
                    }
                }
            }
        }
    }*/
    public void HandleMotor(float verticalI)
    {
       
        if(joueur.IdVéhicule != MOTO)
        {
            frontLeftWheelCollider.motorTorque = verticalI * Puissance;
            frontRightWheelCollider.motorTorque = verticalI * Puissance;
            rearLeftWheelCollider.motorTorque = verticalI * Puissance;
            rearRightWheelCollider.motorTorque = verticalI * Puissance;
            currentbreakForce = isBreaking ? breakForce : 0f;
            ApplyBreaking();
            //currentAcceleration = isAccelerating ? accelerationForce/Poids : 0f; // parce F=m*a
            //ApplyAcceleration(verticalI);
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
        if (joueur.IdVéhicule != MOTO)
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
        if (joueur.IdVéhicule != MOTO)
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
        if(joueur.IdVéhicule != MOTO)
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
        if(joueur.IdVéhicule != MOTO)
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
