using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionnaireTrigger : MonoBehaviour
{
    private int nbJoueur = 0;
    private Player joueur;
    public bool isMainPlayer1;

    public bool isMainPlayer2 = false;
    private void Start()
    {
        nbJoueur = 1;
        if (isMainPlayer2)
        {
            nbJoueur = 2;
        }
    }

    
    private int compteurJoueursPassés;
    private int compteurTour = 0;
    private float temps = 0;
    

    private void Update()
    {
        temps += Time.deltaTime;
    }

    private List<string> Ranks = new List<string>();


    // Start is called before the first frame update
    
    private void OnTriggerEnter(Collider collider)
    {
        joueur = collider.GetComponentInParent<Player>();
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
 //AJOUTER QUELQUE CHOSE POUR QUE LE JOUEUR PUISSE PAS REPASSER
            if (nbJoueur == 2)
            {
                if (collider.gameObject.GetComponentInParent<Player>().IsMainPlayer) 
                {
                    compteurJoueursPassés++;
                }

                if (compteurJoueursPassés ==2)
                {
                    compteurTour++;  
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
                }

                if (compteurTour == 4)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            
            
            }
        }
        
    }
}
