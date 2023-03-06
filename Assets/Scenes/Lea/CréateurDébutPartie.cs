using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CréateurDébutPartie : MonoBehaviour
{
    private List<GameObject> lesAutos;
    private List<Player> joueurs;
    private Player joueur;
    private Vector3 position;
    private int compteurAutos = 0;
    private Vector3 posPremier;
    private GameObject mainPlayer1;

    public GameObject MainPlayer1
    {
        get => mainPlayer1;
    }
    public CréateurDébutPartie(List<Player> autos, GameObject arc, GameObject ligne, Vector3 pos) //behavior auto pour le moment mais surement à changer
    {
        
        posPremier =  new Vector3(335, 0, -50);
        lesAutos = new List<GameObject>();
        joueurs = autos;
        for (int i = 0; i < autos.Count; i++)
        {
            lesAutos.Add(autos[i].Chassis);
            Debug.Log(autos[i].Chassis.name);
        }
        position = pos;
        Instantiate(arc, new Vector3(position.x + 30, 0, position.z), arc.transform.rotation);
        Instantiate(ligne, new Vector3(position.x + 30, 0, position.z), ligne.transform.rotation);
        InstancierAutos();
        
    }

    private void InstancierAutos()
    {
      Debug.Log(lesAutos.Count);
        for (int i = 0; i < lesAutos.Count / 3; i++)
        {
            for (int j = 0; j < lesAutos.Count / 4; j++)
            {
                GameObject joueur = Instantiate(lesAutos[compteurAutos],
                    new Vector3(position.x - 35 * j - 4 * i, 0, (position.z - 37) + 32 * i - 6 * j),
                    lesAutos[compteurAutos].transform.rotation);
                joueur.GetComponent<Player>().CréerPlayer(
                    joueurs[compteurAutos].Vie, joueurs[compteurAutos].Nom,
                    joueurs[compteurAutos].IdVéhicule, joueurs[compteurAutos].IdMoteur,
                    joueurs[compteurAutos].Chassis, joueurs[compteurAutos].Puissance,
                    joueurs[compteurAutos++].Poids);
                if (compteurAutos - 1 == 0)
                {
                    mainPlayer1 = joueur;
                   // FindObjectOfType<Camera>().transform.LookAt(joueur.transform); // hummm
                }
            }

            

            
        }
    }
    
}
