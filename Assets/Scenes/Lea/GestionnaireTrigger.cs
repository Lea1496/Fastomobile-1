
using System.Collections;

using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionnaireTrigger : MonoBehaviour
{
    private int nbJoueur = 0;
    private Player joueur;
    public Player mainPlayer1;
    public Player mainPlayer2;
    public int indiceDernierCheckpoint;
    private const string CheminPlayer1 = "InfoPlayer1.txt";
    private const string CheminPlayer2 = "InfoPlayer2.txt";
    private void Start()
    {
        nbJoueur = 1;
        if (GameData.P2.IsMainPlayer)
        {
            nbJoueur = 2;
        }

        data = new DataCoin();
    }

    private int compteurTourJoueur1;
    private int compteurTourJoueur2;
    private int compteurTour = 0;
    private DataCoin data;

    private float temps1 = 0;
    private float temps2 = 0;
    

    private void Update()
    {
        temps1 += Time.deltaTime;
        temps2 += Time.deltaTime;
        if (compteurTour == 4 || temps1 > 10)
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    
    
    private void OnTriggerEnter(Collider collider)
    {
        joueur = collider.gameObject.GetComponentInParent<Player>();
        
        
        if (collider.gameObject.layer == 6)
        {

            if (nbJoueur == 2)
            {
                if (joueur.Nom == mainPlayer1.Nom && (joueur.DernierCheckpointVisité == indiceDernierCheckpoint || compteurTourJoueur1 == 0) && (temps1 > 15 || compteurTourJoueur1 == 0))
                {
                    joueur.Tour++;
                    compteurTourJoueur1++;
                    temps1 = 0;
                    if (compteurTourJoueur1 == 4)
                    {
                        data.AjouterCoin(CheminPlayer1, joueur.Argent);
                        joueur.IsFinished = true;
                    }
                }
                else
                {
                    if (joueur.Nom == mainPlayer2.Nom && (joueur.DernierCheckpointVisité == indiceDernierCheckpoint || compteurTourJoueur2 == 0) && (temps2 > 15 || compteurTourJoueur2 == 0))
                    {
                        joueur.Tour++;
                        temps2 = 0;
                        compteurTourJoueur2++;
                        if (compteurTourJoueur2 == 4)
                        {
                            data.AjouterCoin(CheminPlayer2, joueur.Argent);
                            joueur.IsFinished = true;
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
                    StartCoroutine(FinirPartie());
                    StopCoroutine(FinirPartie());
                }
            }
            else
            {
            
                if ((joueur.IsMainPlayer && joueur.DernierCheckpointVisité == indiceDernierCheckpoint ) || ((joueur.IsMainPlayer && compteurTour == 0))) //à changer
                {
                    joueur.Tour++;
                    compteurTour++;
                    temps1 = 0;
                }
                
                if (compteurTour == 4)
                {
                    joueur.IsFinished = true;
                    StartCoroutine(FinirPartie());
                    StopCoroutine(FinirPartie());
                    
                }
            }
        }
    }
    private IEnumerator FinirPartie()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
