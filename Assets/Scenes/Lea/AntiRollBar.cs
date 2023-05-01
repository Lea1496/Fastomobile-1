using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Ce code provient de : http://projects.edy.es/trac/edy_vehicle-physics/wiki/TheStabilizerBars
public class AntiRollBar : MonoBehaviour
{
    [SerializeField] private WheelCollider wheelL;
    [SerializeField] private WheelCollider wheelR;
    [SerializeField] private Rigidbody rg;
    [SerializeField] private float antiRoll = 50000f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        WheelHit hit;
        float travelL = 1f;
        float travelR = 1f;

        bool groundedL = wheelL.GetGroundHit(out hit);

        if (groundedL)
        {
            travelL = (-wheelL.transform.InverseTransformPoint(hit.point).y - wheelL.radius) /
                      wheelL.suspensionDistance;
        }
        bool groundedR = wheelR.GetGroundHit(out hit);

        if (groundedL)
        {
            travelR = (-wheelR.transform.InverseTransformPoint(hit.point).y - wheelR.radius) /
                      wheelR.suspensionDistance;
        }

        float antiRollForce = (travelL - travelR) * antiRoll;

        if (groundedL)
        {
            rg.AddForceAtPosition(wheelL.transform.up * - antiRollForce, wheelL.transform.position);
        }
        if (groundedR)
        {
            rg.AddForceAtPosition(wheelR.transform.up * - antiRollForce, wheelR.transform.position);
        }
    }
}