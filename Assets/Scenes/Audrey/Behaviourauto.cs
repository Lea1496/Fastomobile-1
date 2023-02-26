using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourAuto : MonoBehaviour/*GestionnairePlayer*/
{
    //source.https://www.youtube.com/watch?v=Ul01SxwPIvk&t=1407s&ab_channel=CyberChroma
    //savoir valeur de la vitesse pour l'odomètre

    private int Poids;
    private int Puissance;
    //private Rigidbody rb;

    void Start()
    {
        Poids = GetComponent<Player>().Poids;
        Puissance = GetComponent<Player>().Puissance;
    }

    //public void GestionBonus() // ou mettre fonction dans gestionnaire du player ? 
    //{
    //    // faudrait que fonction reçoit le type de Bonus
    //    // je mets la fonction la parce que je peux pas mettre plus de deux classes de base dans gestionnaire des touches
    //}

    

}
