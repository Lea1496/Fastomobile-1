using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GestionnaireCollision : MonoBehaviour
{
    [SerializeField] private int coucheCollisionObstacle;
    [SerializeField] private int coucheCollisionCoin;
    [SerializeField] private int coucheCollisionBonus;

    private List<Player> players;
    private int nbJoueur = 0;
    public GestionnaireCollision(List<Player> liste) // peut être changer pour un getcomponent de GestioonnairePlayer.Players
    {
        players = liste;
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].Chassis.tag.Contains("p1"))
            {
                nbJoueur++;
            }
            else
            {
                if (players[i].Chassis.tag.Contains("p2"))
                {
                    nbJoueur++;
                }
            }
        }
    }

    private int compteurJoueursPassés;
    private int compteurTour = 0;

    public int CompteurTour
    {
        get => compteurTour;
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (nbJoueur == 2)
        {
            if (collider.gameObject.tag.Contains("p1")) //à changer
            {
                compteurJoueursPassés++;
            }
            else
            {
                if (collider.gameObject.tag.Contains("p2")) //à changer
                {
                    compteurJoueursPassés++;
                }
            }

            if (compteurJoueursPassés ==2)
            {
                compteurTour++;  //mettre fin à la partie ici?
                compteurJoueursPassés = 0;
            }
        }
        else
        {
            compteurTour++;
            //mettre fin à la partie ici??
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint point = collision.GetContact(0);

        if (point.otherCollider.gameObject.layer == coucheCollisionObstacle) //Obstacle
        {
            point.thisCollider.GetComponent<Player>().EnleverVie(5); //changer cbm de vie
            
        }
        if (point.otherCollider.gameObject.layer == coucheCollisionCoin) //Argent
        {
            point.thisCollider.GetComponent<Player>().AjouterArgent(1);
            Destroy(point.otherCollider.gameObject);
            
        }
        if (point.otherCollider.gameObject.layer == coucheCollisionBonus) //Bonus
        {
            
        }
        
    }
    
}
