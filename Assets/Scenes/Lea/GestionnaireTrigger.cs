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

    private float temps = 0;

    private List<string> Ranks = new List<string>();

    private void Update()
    {
        temps += Time.deltaTime;
    }


    // Start is called before the first frame update
    
    private void OnTriggerEnter(Collider collider)
    {
        joueur = collider.gameObject.GetComponentInParent<Player>();
        /*Ranks.Add(joueur.Nom);
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
        Debug.Log(collider.name);*/
        if (collider.gameObject.layer == 6)
        {
 //AJOUTER QUELQUE CHOSE POUR QUE LE JOUEUR PUISSE PAS REPASSER
            if (nbJoueur == 2)
            {
                if (joueur.Nom == mainPlayer1.Nom)
                {
                    compteurTourJoueur1++;
                }
                if (joueur.Nom == mainPlayer2.Nom)
                {
                    compteurTourJoueur2++;
                }
                if (compteurTourJoueur1 != 0 && compteurTourJoueur2 != 0)
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
                    temps = 0;
                }

                if (compteurTour == 4)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            
            
            }
        }
        
    }
}
