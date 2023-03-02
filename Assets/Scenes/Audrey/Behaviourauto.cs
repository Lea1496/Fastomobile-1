using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourAuto : MonoBehaviour/*GestionnairePlayer*/
{
    //source.https://www.youtube.com/watch?v=Ul01SxwPIvk&t=1407s&ab_channel=CyberChroma
    //source.https://www.youtube.com/watch?v=Z4HA8zJhGEk&t=183s&ab_channel=GameDevChef
    //savoir valeur de la vitesse pour l'odomètre

    // faire un if pour la moto car deux roues et non quatre
    // calcul avec Poids ? Influence dans le breakForce? Moins d'accelerationForce?

    private int Poids;
    private int Puissance;
   

    void Start()
    {
        Poids = GetComponent<Player>().Poids;
        Puissance = GetComponent<Player>().Puissance;
    }

    private float currentSteerAngle;
    private float currentbreakForce;
    private float currentAcceleration;
    public bool isBreaking;
    public bool isAccelerating;

    [SerializeField] private float accelerationForce; // reste a déterminer
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheeTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;


    public void HandleMotor(float verticalI)
    {
        frontLeftWheelCollider.motorTorque = verticalI * Puissance;
        frontRightWheelCollider.motorTorque = verticalI * Puissance;
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
        currentAcceleration = isAccelerating ? accelerationForce : 0f;
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
        frontRightWheelCollider.motorTorque =  verticalI * Puissance * currentAcceleration;
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
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheeTransform);
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
