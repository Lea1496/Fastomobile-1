
using System.Collections.Generic;
using UnityEngine;

using Random = System.Random;

public class GestionnairePlayer
{ 
    private GameObject auto;
    private GameObject moto;
    private GameObject camion;
    private int indiceChassis;
    private Random gen = new Random();
    private List<PlayerData> joueurs = new List<PlayerData>(12);
    private int nbJoueurs = 1;
    public List<PlayerData> Joueurs
    {
        get => joueurs;
    }
    public GestionnairePlayer(/*GameObject Auto, GameObject Moto, GameObject Camion*/)
    {
        
        GameData.P1.Nom = "Player1";
        joueurs.Add(GameData.P1);
        nbJoueurs = 1;
        if (GameData.P2.IsMainPlayer)
        {
            nbJoueurs = 2;
            GameData.P2.Nom = "Player2";
            joueurs.Add(GameData.P2);
            joueurs[1].Puissance = AssignerPuissance(joueurs[1].IdMoteur);
            joueurs[1].Poids = AssignerPoids(joueurs[1].IdVéhicule);
        }
    
        
        joueurs[0].Puissance = AssignerPuissance(joueurs[0].IdMoteur);
        joueurs[0].Poids = AssignerPoids(joueurs[0].IdVéhicule);
        
        for (int i = nbJoueurs; i < 12; i++)
        {
            joueurs.Add(new PlayerData());
            joueurs[i].Nom = $"bot{i}";
            joueurs[i].Vie = 100;
            joueurs[i].IdMoteur = 0;
            indiceChassis = gen.Next(0, 3);
            joueurs[i].IdVéhicule = indiceChassis;
            joueurs[i].Puissance = AssignerPuissance(gen.Next(0,3));
            joueurs[i].Poids = AssignerPoids(indiceChassis);
           
        }
        
    }

    private int AssignerPuissance(int indice)
    {
        int puissance = 5000;
        if (indice == 1)
        {
            puissance = 5500;
        }
        else
        {
            if (indice == 2)
            {
                puissance = 6000;
            }
        }

        return puissance;
    }
    private int AssignerPoids(int indice)
    {
        int poids = 15;
        if (indice == 1)
        {
            poids = 5;
        }
        else
        {
            if (indice == 2)
            {
                poids = 10;
            }
        }

        return poids;
    }
    /*private GameObject AssignerChassis(int indice)
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
                chassis = moto;
            }
        }

        return chassis;
    }*/



}
