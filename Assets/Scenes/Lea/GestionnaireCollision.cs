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
    private Player joueur;
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
    private DataCoin data = new DataCoin();
    private float temps = 0;
    private int triggerCount = 0;
    public int CompteurTour
    {
        get => compteurTour;
    }

    private void Update()
    {
        temps += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider collider)
    {
        
        if (collider.gameObject.layer == 6 && (triggerCount < 1 || temps > 10 ))
        {
            triggerCount = 0;
            Debug.Log(collider.isTrigger);
            if (nbJoueur == 2)
            {
                if (collider.gameObject.GetComponentInParent<Player>().IsMainPlayer) //à changer
                {
                    compteurJoueursPassés++;
                }

                if (compteurJoueursPassés ==2)
                {
                    triggerCount++;
                    compteurTour++;  //mettre fin à la partie ici?
                    compteurJoueursPassés = 0;
                    
                }

                if (compteurTour == 4)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
            else
            {
            
                if (collider.gameObject.GetComponentInParent<Player>().IsMainPlayer) //à changer
                {
                    compteurTour++;
                    triggerCount++;
                }

                if (compteurTour == 4)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            
            
            }
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint point = collision.GetContact(0);
        Debug.Log(point.otherCollider.gameObject.layer + " o");
        Debug.Log(point.thisCollider.gameObject.layer + " t");
        if (point.thisCollider.gameObject.layer == coucheCollisionObstacle) //Obstacle
        {
            Debug.Log(point.otherCollider.gameObject.layer);
            point.thisCollider.GetComponentInParent<Player>().EnleverVie(5); //changer cbm de vie
       
            
        }
        if (point.otherCollider.gameObject.layer == coucheCollisionCoin) //Argent
        {
            joueur = point.thisCollider.GetComponentInParent<Player>();
            //joueur.AjouterArgent(1);
            if (joueur.IsMainPlayer)
            {
                if (joueur.Nom == GetComponent<GestionnaireJeux>().MainPlayer1.name)
                {
                    data.AjouterCoin("InfoPlayer1.txt", 1);
                }
                else
                {
                    if (joueur.Nom == GetComponent<GestionnaireJeux>().MainPlayer2.name)
                    {
                        data.AjouterCoin("InfoPlayer2.txt", 1);
                    }
                }
            }
            Destroy(point.otherCollider.gameObject);
            GetComponent<GénérateurCoins>().GénérerCoins(1);
            
        }
        if (point.otherCollider.gameObject.layer == coucheCollisionBonus) //Bonus
        {
            
        }
        
    }
    
}
