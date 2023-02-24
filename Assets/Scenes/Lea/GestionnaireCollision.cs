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
    public GestionnaireCollision(List<Player> liste) // peut être changer pour un getcomponent de GestioonnairePlayer.Players
    {
        players = liste;
    }
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint point = collision.GetContact(0);

        if (point.otherCollider.gameObject.layer == coucheCollisionObstacle)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (point.thisCollider.gameObject.name == players[i].Nom) // à modifier
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
