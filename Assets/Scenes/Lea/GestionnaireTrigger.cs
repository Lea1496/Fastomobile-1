using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionnaireTrigger : MonoBehaviour
{
    private int nbJoueur = 0;
    private Player joueur;
    public Player mainPlayer1;

    public Player mainPlayer2;
    private const string CheminPlayer1 = "InfoPlayer1.txt";
    private const string CheminPlayer2 = "InfoPlayer2.txt";
    private void Start()
    {
        nbJoueur = 1;
        if (mainPlayer2 != null)
        {
            nbJoueur = 2;
        }
    }

    private int compteurTourJoueur1;
    private int compteurTourJoueur2;
    private int compteurTour = 0;
    private DataCoin data;

    private float temps = 0;

    private List<string> Ranks = new List<string>();

    private void Update()
    {
        temps += Time.deltaTime;
    }
    
    
    private void OnTriggerEnter(Collider collider)
    {
        joueur = collider.gameObject.GetComponentInParent<Player>();
        Ranks.Add(joueur.Nom);
        if (joueur.IsMainPlayer)
        {
            for (int i = 0; i < Ranks.Count; i++)
            {
                if (Ranks[i] == joueur.Nom)
                {
                    joueur.Rang = i + 1;
                }
            }
        }

        if (Ranks.Count == 12)
        {
            Ranks.Clear();
        }
        Debug.Log(collider.name);
        if (collider.gameObject.layer == 6)
        {

            if (nbJoueur == 2)
            {
                if (joueur.Nom == mainPlayer1.Nom)
                {
                    compteurTourJoueur1++;
                    if (compteurTourJoueur1 == 4)
                    {
                        data.AjouterCoin(CheminPlayer1, joueur.Argent);
                    }
                }
                else
                {
                    if (joueur.Nom == mainPlayer2.Nom)
                    {
                        compteurTourJoueur2++;
                        if (compteurTourJoueur2 == 4)
                        {
                            data.AjouterCoin(CheminPlayer2, joueur.Argent);
                        }
                    }
                }
                
                if (compteurTourJoueur1 > compteurTour && compteurTourJoueur2 > compteurTour)
                {
                    compteurTour++;
                    
                    compteurTourJoueur1 = 0;
                    compteurTourJoueur2 = 0;

                }

                if (compteurTour == 4)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            }
            else
            {
            
                if ((joueur.IsMainPlayer && temps > 15) || ((joueur.IsMainPlayer && compteurTour == 0))) //à changer
                {
                    compteurTour++;
                    Debug.Log(compteurTour);
                    temps = 0;
                }

                if (compteurTour == 4 || Time.deltaTime > 10)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            
            
            }
        }
        
    }
}
