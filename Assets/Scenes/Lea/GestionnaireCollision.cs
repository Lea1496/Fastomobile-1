using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GestionnaireJeux))]
public class GestionnaireCollision : MonoBehaviour
{
    [SerializeField] private int coucheCollisionObstacle;
    [SerializeField] private int coucheCollisionCoin;
    [SerializeField] private int coucheCollisionBonus;

    private List<Player> players;
    private int nbJoueur = 0;

    private void Start()
    {
        
        players = GetComponent<GestionnaireJeux>().Autos;
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].IsMainPlayer)
            {
                nbJoueur++;
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
            if (collider.gameObject.GetComponentInParent<Player>().IsMainPlayer) //à changer
            {
                compteurJoueursPassés++;
            }

            if (compteurJoueursPassés ==2)
            {
                compteurTour++;  //mettre fin à la partie ici?
                compteurJoueursPassés = 0;
            }

            if (compteurTour == 3)
            {
                
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        else
        {
            if (collider.gameObject.GetComponentInParent<Player>().IsMainPlayer) //à changer
            {
                compteurTour++;
            }

            if (compteurTour == 3)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            
            //mettre fin à la partie ici?
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint point = collision.GetContact(0);

        if (point.otherCollider.gameObject.layer == coucheCollisionObstacle) //Obstacle
        {
            point.thisCollider.GetComponentInParent<Player>().EnleverVie(5); //changer cbm de vie
            
        }
        if (point.otherCollider.gameObject.layer == coucheCollisionCoin) //Argent
        {
            point.thisCollider.GetComponentInParent<Player>().AjouterArgent(1);
            Destroy(point.otherCollider.gameObject);
            GetComponent<GénérateurCoins>().GénérerCoins(1);
            
        }
        if (point.otherCollider.gameObject.layer == coucheCollisionBonus) //Bonus
        {
            
        }
        
    }
    
}
