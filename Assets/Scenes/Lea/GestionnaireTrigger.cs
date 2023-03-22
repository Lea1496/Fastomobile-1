using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionnaireTrigger : MonoBehaviour
{
    private List<Player> players;
    private int nbJoueur = 0;
    private Player joueur;
    private void Start()
    {
        
        //players = GetComponent<GestionnaireJeux>().Autos;
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
        Debug.Log(collider.name);
        if (collider.gameObject.layer == 6 && (triggerCount < 1 || temps > 10 ))
        {
            triggerCount = 0;
            
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
}
