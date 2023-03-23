using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class GestionnairePlayer
{ 
    private GameObject auto;
    private GameObject moto;
    private GameObject camion;
    private int indiceChassis;
    private Random gen = new Random();
    private List<PlayerData> joueurs = new List<PlayerData>(12);

    public List<PlayerData> Joueurs
    {
        get => joueurs;
    }
    public GestionnairePlayer(GameObject Auto, GameObject Moto, GameObject Camion, int nbJoueurs)
    {
        GameData.P1.IsMainPlayer = true; // à changer
        GameData.P1.Nom = "YAY";
        joueurs.Add(GameData.P1);
        auto = Auto;
        moto = Moto;
        camion = Camion;
        joueurs[0].Chassis = AssignerChassis(joueurs[0].IdVéhicule);
        joueurs[0].Puissance = AssignerPuissance(joueurs[0].IdMoteur);
        joueurs[0].Poids = AssignerPoids(joueurs[0].IdVéhicule);
        
        for (int i = nbJoueurs; i < 12; i++)
        {
            joueurs.Add(new PlayerData());
            joueurs[i].Nom = $"bot{i}";
            joueurs[i].Vie = 100;
            joueurs[i].IdMoteur = 0;
            indiceChassis = gen.Next(0, 3);
            joueurs[i].Chassis = AssignerChassis(indiceChassis);
            joueurs[i].Puissance = AssignerPuissance(gen.Next(0,3));
            joueurs[i].Poids = AssignerPoids(indiceChassis);
           
        }
    }

    private int AssignerPuissance(int indice)
    {
        int puissance = 15000;
        if (indice == 1)
        {
            puissance = 50;
        }
        else
        {
            if (indice == 2)
            {
                puissance = 10000;
            }
        }

        return puissance;
    }
    private int AssignerPoids(int indice)
    {
        int poids = 150;
        if (indice == 1)
        {
            poids = 50;
        }
        else
        {
            if (indice == 2)
            {
                poids = 100;
            }
        }

        return poids;
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



}
