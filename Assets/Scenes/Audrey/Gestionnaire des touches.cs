using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gestionnairedestouches : Behaviourauto
{
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("w"))
        {
            Accélerer();
        }
        if (Input.GetKey("s"))
        {
            Décélerer();
        }
        if (Input.GetKey("d"))
        {
            RotateDroit();
        }
        if(Input.GetKey("a"))
        {
            RotateGauche();
        }

    }
}
