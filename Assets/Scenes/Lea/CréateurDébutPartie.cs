using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

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
    [SerializeField] private GameObject police;
    [SerializeField] private GameObject arc;
    [SerializeField] private GameObject ligne;
    private GameObject ligneArr;
    private List<Vector3> points;
    private Vector3[] sommets;
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
            chassis = camion;
        }
        else
        {
            if (indice == 2)
            {
                chassis = police;
            }
        }

        return chassis;
    }
    public void CréerDébutPartie(List<PlayerData> autos, List<Vector3> pts, Vector3[] som)
    {
        points = pts;
        posPremier =  new Vector3(335, 0, -50);
        lesAutos = new List<GameObject>();
        joueurs = autos;
        sommets = som;
        for (int i = 0; i < autos.Count; i++)
        {
            lesAutos.Add(AssignerChassis(autos[i].IdVéhicule));
        }
        position = points[points.Count - 1];
        Instantiate(arc, new Vector3(position.x , 0, position.z), arc.transform.rotation);
        ligneArr = Instantiate(ligne, new Vector3(position.x , 0, position.z), ligne.transform.rotation);
        InstancierAutos();
        
        
    }

    private void InstancierAutos()
    {
      
        for (int j = 0; j < lesAutos.Count / 4; j++)
        {
            for (int i = 0; i < lesAutos.Count / 3; i++)
            {
                GameObject thisJoueur = Instantiate(lesAutos[compteurAutos],
                    new Vector3(position.x - 35 * j - 4 * i -30, 5, (position.z - 37) + 32 * i - 6 * j -7),
                    lesAutos[compteurAutos].transform.rotation);
                Player leJoueur = thisJoueur.GetComponent<Player>();
                leJoueur.CréerPlayer(
                    joueurs[compteurAutos].Vie, joueurs[compteurAutos].Nom,
                    joueurs[compteurAutos].IdVéhicule, joueurs[compteurAutos].IdMoteur,
                    joueurs[compteurAutos].Chassis, joueurs[compteurAutos].Puissance,
                    joueurs[compteurAutos].Poids, joueurs[compteurAutos++].IsMainPlayer, compteurAutos);
                DontDestroyOnLoad(thisJoueur);
                GameData.Ranks.Add(leJoueur.Nom);
                GameData.LesJoueurs.Add(leJoueur);
                GestionnaireCollision collisions = thisJoueur.GetComponent<GestionnaireCollision>();
                
                collisions.points = sommets;

                if (compteurAutos == 1)
                {
                    mainPlayer1 = thisJoueur;
                    ligneArr.GetComponentInChildren<GestionnaireTrigger>().mainPlayer1 = leJoueur;
                    ligneArr.GetComponentInChildren<GestionnaireTrigger>().mainPlayer1.IsMainPlayer = true; // Pas nécessaire?
                    thisJoueur.GetComponent<GestionnaireTouches>().Poids = joueurs[compteurAutos - 1].Poids;
                    thisJoueur.GetComponent<GestionnaireTouches>().Puissance = joueurs[compteurAutos - 1].Puissance;
                    
                }

                if (compteurAutos == 2 && joueurs[1].IsMainPlayer)
                {
                    mainPlayer2 = thisJoueur;
                    leJoueur.IsMainPlayer2 = true;
                    ligneArr.GetComponentInChildren<GestionnaireTrigger>().mainPlayer2 = leJoueur;
                    ligneArr.GetComponentInChildren<GestionnaireTrigger>().mainPlayer2.IsMainPlayer = true; // Pas nécessaire ?
                    thisJoueur.GetComponent<GestionnaireTouches>().Poids = joueurs[compteurAutos - 1].Poids;
                    thisJoueur.GetComponent<GestionnaireTouches>().Puissance = joueurs[compteurAutos - 1].Puissance;
                }
            }

            

            
        }
    }
    
}


