using System;
using System.Collections;
using System.Collections.Generic;
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
           // if (players[i])
            {
                
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
                if (collider.gameObject.tag.Contains("p2"))
                {
                    compteurJoueursPassés++;
                }
            }

            if (compteurJoueursPassés ==2)
            {
                compteurTour++;
                compteurJoueursPassés = 0;
            }
        }
        else
        {
            compteurTour++;
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint point = collision.GetContact(0);

        if (point.otherCollider.gameObject.layer == coucheCollisionObstacle)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (point.thisCollider.gameObject.tag == players[i].Nom) // à modifier
                {
                    
                }
            }
        }
        if (point.otherCollider.gameObject.layer == coucheCollisionCoin)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (point.thisCollider.gameObject.name == players[i].Nom) // à modifier
                {
                    
                }
            }
        }
        if (point.otherCollider.gameObject.layer == coucheCollisionBonus)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (point.thisCollider.gameObject.name == players[i].Nom) // à modifier
                {
                    
                }
            }
        }
        
    }
    
}
