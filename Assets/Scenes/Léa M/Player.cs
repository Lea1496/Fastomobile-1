using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : IPlayer
{
    
    public int Vie { get; set; }
    public string Nom { get; set; }
    public int Argent { get; private set; }
    public int IdVéhicule { get; set; }
    public int IdMoteur { get; set; }
    public GameObject Chassis { get; set; }
    
    public Player()
    {
        Chassis.tag = Nom;
    }

    public void AjouterVie(int vieAjoutée)
    {
        GameData.P1.Vie += vieAjoutée;
    }
    public void EnleverVie(int vieEnlevée)
    {
        GameData.P1.Vie += vieEnlevée;
    }

    public void Acheter(int prix)
    {
        bool peutAcheter = false;
        if (GameData.P1.Argent >= prix)
        {
            GameData.P1.Argent -= prix;
            peutAcheter = true;
            
        }
    }
    

}
