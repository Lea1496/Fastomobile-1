using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CréateurDébutPartie
{
    private List<GameObject> lesAutos;
    private List<Player> joueurs;
    private Player joueur;
    private Vector3 position;
    private int compteurAutos = 0;
    private Vector3 posPremier;
    private GameObject mainPlayer1;
    private GameObject mainPlayer2;
    public GameObject MainPlayer1
    {
        get => mainPlayer1;
    }
    public GameObject MainPlayer2
    {
        get => mainPlayer2;
    }
    public CréateurDébutPartie(List<Player> autos, GameObject arc, GameObject ligne, Vector3 pos) //behavior auto pour le moment mais surement à changer
    {
        
        posPremier =  new Vector3(335, 0, -50);
        lesAutos = new List<GameObject>();
        joueurs = autos;
        for (int i = 0; i < autos.Count; i++)
        {
            lesAutos.Add(autos[i].Chassis);
        }
        position = pos;
        GameObject.Instantiate(arc, new Vector3(position.x + 30, 0, position.z), arc.transform.rotation);
        GameObject.Instantiate(ligne, new Vector3(position.x + 30, 0, position.z), ligne.transform.rotation);
        InstancierAutos();
        
    }

    private void InstancierAutos()
    {
      
        for (int i = 0; i < lesAutos.Count / 3; i++)
        {
            for (int j = 0; j < lesAutos.Count / 4; j++)
            {
                GameObject thisJoueur = GameObject.Instantiate(lesAutos[compteurAutos],
                    new Vector3(position.x - 35 * j - 4 * i, 5, (position.z - 37) + 32 * i - 6 * j),
                    lesAutos[compteurAutos].transform.rotation);
                
                thisJoueur.GetComponent<Player>().CréerPlayer(
                    joueurs[compteurAutos].Vie, joueurs[compteurAutos].Nom,
                    joueurs[compteurAutos].IdVéhicule, joueurs[compteurAutos].IdMoteur,
                    joueurs[compteurAutos].Chassis, joueurs[compteurAutos].Puissance,
                    joueurs[compteurAutos].Poids, joueurs[compteurAutos++].IsMainPlayer);
                Debug.Log(thisJoueur.GetComponent<Player>().Nom);
                if (compteurAutos - 1 == 0)
                {
                    mainPlayer1 = thisJoueur;
                    thisJoueur.GetComponent<PlayerController>().enabled = true;
                    thisJoueur.GetComponent<GestionnaireTouches>().Poids = joueurs[compteurAutos - 1].Poids;
                    thisJoueur.GetComponent<GestionnaireTouches>().Puissance = joueurs[compteurAutos - 1].Puissance;
                    
                }

                if (compteurAutos - 1 == 1 && joueurs[1].IsMainPlayer)
                {
                    mainPlayer2 = thisJoueur;
                    thisJoueur.GetComponent<GestionnaireTouches>().enabled = true;
                    thisJoueur.GetComponent<GestionnaireTouches>().Poids = joueurs[compteurAutos - 1].Poids;
                    thisJoueur.GetComponent<GestionnaireTouches>().Puissance = joueurs[compteurAutos - 1].Puissance;
                }
            }

            

            
        }
    }
    
}
