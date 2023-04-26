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
    public bool DéjàAchetéAvancé { get; set; }
    public bool DéjàAchetéExpert { get; set; }
    
    public bool IsMainPlayer { get; set; }
    public bool IsMainPlayer1 { get; set; }
    public bool IsMainPlayer2 { get; set; }
    public int Tour { get; set; }

    public bool IsFinished { get; set; }
    public PlayerData(/*int vie, string nom, int idVéhicule, int idMoteur, GameObject chassis, int puissance*/)
    {
        IsMainPlayer1 = false;
        IsMainPlayer2 = false;
        IsMainPlayer = false;

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

