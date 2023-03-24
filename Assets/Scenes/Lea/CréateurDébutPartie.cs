using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CréateurDébutPartie: MonoBehaviour
{
    private List<GameObject> lesAutos;
    private List<PlayerData> joueurs;
    private Player joueur;
    private Vector3 position;
    private int compteurAutos = 0;
    private Vector3 posPremier;
    private GameObject mainPlayer1;
    private GameObject mainPlayer2;
    [SerializeField] private GameObject auto;
    [SerializeField] private GameObject camion;
    [SerializeField] private GameObject moto;
    private GameObject ligneArr;
    private List<Vector3> points;
    public GameObject MainPlayer1
    {
        get => mainPlayer1;
    }
    public GameObject MainPlayer2
    {
        get => mainPlayer2;
    }
    private GameObject AssignerChassis(int indice)
    {
        GameObject chassis = auto;
        if (indice == 1)
        {
            chassis = moto;
        }
        else
        {
            if (indice == 2)
            {
                chassis = camion;
            }
        }

        return chassis;
    }
    public void CréerDébutPartie(List<PlayerData> autos, GameObject arc, GameObject ligne, Vector3 pos,List<Vector3> pts) //behavior auto pour le moment mais surement à changer
    {
        Debug.Log(pts.Count);
        points = pts;
        posPremier =  new Vector3(335, 0, -50);
        lesAutos = new List<GameObject>();
        joueurs = autos;
        for (int i = 0; i < autos.Count; i++)
        {
            lesAutos.Add(AssignerChassis(autos[i].IdVéhicule));
        }
        position = pos;
        Instantiate(arc, new Vector3(position.x, 0, position.z), arc.transform.rotation);
        ligneArr = Instantiate(ligne, new Vector3(position.x, 0, position.z), ligne.transform.rotation);

        InstancierAutos();
        
        
    }

    private void InstancierAutos()
    {
      
        for (int i = 0; i < lesAutos.Count / 3; i++)
        {
            for (int j = 0; j < lesAutos.Count / 4; j++)
            {
                GameObject thisJoueur = Instantiate(lesAutos[compteurAutos],
                    new Vector3(position.x - 35 * j - 4 * i - 30, 5, (position.z - 37) + 32 * i - 6 * j),
                    lesAutos[compteurAutos].transform.rotation);
                
                thisJoueur.GetComponent<Player>().CréerPlayer(
                    joueurs[compteurAutos].Vie, joueurs[compteurAutos].Nom,
                    joueurs[compteurAutos].IdVéhicule, joueurs[compteurAutos].IdMoteur,
                    joueurs[compteurAutos].Chassis, joueurs[compteurAutos].Puissance,
                    joueurs[compteurAutos].Poids, joueurs[compteurAutos++].IsMainPlayer, compteurAutos);
                
                GestionnaireCollision collisions = thisJoueur.GetComponent<GestionnaireCollision>();
                
                collisions.points = points;

                if (compteurAutos - 1 == 0)
                {
                    mainPlayer1 = thisJoueur;
                    collisions.isMainPlayer1 = true;
                    ligneArr.GetComponentInChildren<GestionnaireTrigger>().isMainPlayer1 = true;
                    thisJoueur.GetComponent<GestionnaireTouches>().enabled = true;
                    thisJoueur.GetComponent<GestionnaireTouches>().Poids = joueurs[compteurAutos - 1].Poids;
                    thisJoueur.GetComponent<GestionnaireTouches>().Puissance = joueurs[compteurAutos - 1].Puissance;
                    
                }

                if (compteurAutos - 1 == 1 && joueurs[1].IsMainPlayer)
                {
                    ligneArr.GetComponentInChildren<GestionnaireTrigger>().isMainPlayer2 = true;
                    mainPlayer2 = thisJoueur;
                    collisions.isMainPlayer2 = true;
                    thisJoueur.GetComponent<GestionnaireTouches>().enabled = true;
                    thisJoueur.GetComponent<GestionnaireTouches>().Poids = joueurs[compteurAutos - 1].Poids;
                    thisJoueur.GetComponent<GestionnaireTouches>().Puissance = joueurs[compteurAutos - 1].Puissance;
                }
            }

            

            
        }
    }
    
}


