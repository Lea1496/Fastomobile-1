using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourAuto : MonoBehaviour/*GestionnairePlayer*/
{
    //source.https://www.youtube.com/watch?v=Ul01SxwPIvk&t=1407s&ab_channel=CyberChroma
    //savoir valeur de la vitesse pour l'odomètre

    Chassis chassis;
    Moteurs moteur;

    int PtsDeVieDépart = 10; // reste è déterminer
    int ArgentDépart = 50; // reste à déterminer
    int Accélération = 20; // reste à déterminer

    public int PtsDeVie;
    public int Argent;
    public int Poids;
    public int Puissance;
    private Rigidbody rb;

    void Awake()
    {
        PtsDeVie = PtsDeVieDépart;
        Argent = ArgentDépart;
        Poids = chassis.Poids;
        Puissance = moteur.Puissance;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void GestionBonus() // ou mettre fonction dans gestionnaire du player ? 
    {
        // faudrait que fonction reçoit le type de Bonus
        // je mets la fonction la parce que je peux pas mettre plus de deux classes de base dans gestionnaire des touches
    }

    // influence de la puissance et poids
    public void Avancer()
    {
        rb.AddRelativeForce((Vector3.forward * Time.deltaTime * Puissance)/Poids);
    }

    public void Accélérer()
    {
        rb.AddRelativeForce((Vector3.forward * Time.deltaTime * Puissance * Accélération)/Poids);
    }

    public void Arrêter()
    {
        rb.AddRelativeForce(-(Vector3.forward * Time.deltaTime * Puissance) / Poids);
    }

    public void TournerGauche()
    {
        rb.AddTorque((Vector3.forward * Time.deltaTime * Puissance * Accélération) / Poids);
    }

    public void TournerDroite()
    {
        rb.AddTorque((Vector3.forward * Time.deltaTime * Puissance * Accélération) / Poids);
    }

}
