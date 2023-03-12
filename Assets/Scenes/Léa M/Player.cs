using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IPlayer
{    
    public int Vie { get; set; }
    public string Nom { get; set; }
    public int Argent { get; set; }
    public int IdVéhicule { get; set; }
    public int IdMoteur { get; set; }
    public GameObject Chassis { get; set; }
    public int Puissance { get; set; }
    public int Poids { get; set; }
    
    public bool IsMainPlayer { get; set; }
    
    public Player(/*int vie, string nom, int idVéhicule, int idMoteur, GameObject chassis, int puissance*/)
    {
        /*Vie = vie;
        Nom = nom;
        IdVéhicule = idVéhicule;
        IdMoteur = idMoteur;
        Chassis = chassis;
        Puissance = puissance;*/
        //Chassis.tag = Nom;
        IsMainPlayer = false;
        Argent = 0;
    }

    public void CréerPlayer(int vie, string nom, int idVéhicule, int idMoteur, GameObject chassis, int puissance, int poids)
    {
        Vie = vie;
        Nom = nom;
        IdVéhicule = idVéhicule;
        IdMoteur = idMoteur;
        Chassis = chassis;
        Puissance = puissance;
        Poids = poids;
    }
    public void AjouterVie(int vieAjoutée)
    {
        Vie += vieAjoutée;
    }
    public void EnleverVie(int vieEnlevée)
    {
        Vie += vieEnlevée;
    }

    public void AjouterArgent(int nbArgent)
    {
        Argent += nbArgent;
    }

    // public bool Acheter(int prix)
    // {
    //     bool peutAcheter = false;
    //     if (GameData.P1.Argent >= prix)
    //     {
    //         GameData.P1.Argent -= prix;
    //         peutAcheter = true;
    //         
    //     }
    //
    //     return peutAcheter;
    // }


}
