using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiStuckBehavior : MonoBehaviour
{
    private const int CoucheCollisionMur = 14;
    private const int CoucheCollisionObstacle = 9;
    private const int CoucheCollisionPlayer = 6;
    private Vector3 reculer = new Vector3(0, 0, -1);
    private int layer = 0;
    private float temps = 0;
    private WheelCollider[] wheels;

    private float tempsEnter = 0;

    private void Update()
    {
        tempsEnter += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((layer == CoucheCollisionObstacle || layer == CoucheCollisionMur || layer == CoucheCollisionPlayer) && tempsEnter > 3)
        {
            gameObject.transform.Translate(reculer, Space.Self);
            gameObject.GetComponent<GestionnaireTouches>().ApplyBreakingCustom(80000000);
            
            tempsEnter = 0;
        }

        
    }

    private void OnTriggerStay(Collider collider)
    {
        
        //ContactPoint point = collision.GetContact(0);
        //Debug.Log(point.thisCollider.name);
        layer = collider.gameObject.layer;
        if (layer == CoucheCollisionObstacle || layer == CoucheCollisionMur|| layer == CoucheCollisionPlayer  )
        {
            
//            Debug.Log("Collision");
            //Empêche que le joueur reste pris dans un mur/obstacle
            //gameObject.transform.Translate(reculer, Space.Self);
            //gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            wheels = gameObject.GetComponentsInChildren<WheelCollider>();
            while(Math.Floor(wheels[1].rpm) > 100 && temps < 1)
            {
//                Debug.Log(wheels[1].rpm);
                temps += Time.deltaTime;
                
                gameObject.GetComponent<GestionnaireTouches>().ApplyBreakingCustom(80000000);
                
            }

            if (temps > 1)
            {
                gameObject.GetComponent<GestionnaireTouches>().ApplyAccelerationCustom(-10);
            }
            
        }
        
        
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (layer == CoucheCollisionObstacle || layer == CoucheCollisionMur || layer == CoucheCollisionPlayer)
        {
            gameObject.GetComponent<GestionnaireTouches>().ApplyBreakingCustom(400000);
            temps = 0;
        }

        
    }
}
