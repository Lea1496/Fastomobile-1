using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerData :  IPlayer
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
    
    public PlayerData(/*int vie, string nom, int idVéhicule, int idMoteur, GameObject chassis, int puissance*/)
    {
        IsMainPlayer = false;
        Argent = 0;
        Vie = 100;
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

