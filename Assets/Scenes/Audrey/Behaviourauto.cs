using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviourauto : MonoBehaviour
{
    // savoir valeur de la vitesse pour l'odomètre

    Chassis chassis;
    Moteurs moteur;

    int PtsDeVieDépart = 10; // reste è déterminer
    int ArgentDépart = 50; // reste à déterminer
    bool bonus;

    public int PtsDeVie;
    public int Argent;
    public int Poids;
    public int Puissance;

    public void Awake()
    {
        PtsDeVie = PtsDeVieDépart;
        Argent = ArgentDépart;
        Poids = chassis.Poids;
        Puissance = moteur.Puissance;
    }

    // influence de la puissance et poids
    public void Avancer()
    {
        transform.Translate(Vector3.forward * Time.deltaTime);
    }

    public void Accélérer()
    {

    }

    public void Arrêter()
    {
        transform.Translate(Vector3.back * Time.deltaTime);
    }

    public void TournerGauche()
    {
        transform.Rotate(new Vector3(0, -10, 0) * Time.deltaTime);
    }

    public void TournerDroite()
    {
        transform.Rotate(new Vector3(0, 10, 0) * Time.deltaTime);
    }

}
