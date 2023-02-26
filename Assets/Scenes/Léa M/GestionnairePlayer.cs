using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class GestionnairePlayer : MonoBehaviour
{ 
    private GameObject auto;
    private GameObject moto;
    private GameObject camion;
    private int indiceChassis;
    private Random gen = new Random();
    private List<Player> joueurs = new List<Player>(15);

    public List<Player> Joueurs
    {
        get => joueurs;
    }
    public GestionnairePlayer(GameObject Auto, GameObject Moto, GameObject Camion, int nbJoueurs)
    {
        joueurs.Add(GameData.P1);
        auto = Auto;
        moto = Moto;
        camion = Camion;
        joueurs[0].Chassis = AssignerChassis(joueurs[0].IdVéhicule);
        joueurs[0].Puissance = AssignerPuissance(joueurs[0].IdMoteur);
        joueurs[0].Poid = AssignerPoid(joueurs[0].IdVéhicule);
        //joueurs[0].Chassis.tag = $"{joueurs[0]}p1";
        for (int i = nbJoueurs; i < joueurs.Capacity; i++)
        {
            joueurs.Add(new Player());
            joueurs[i].Nom = $"b{i}";
            joueurs[i].Vie = 100;
            joueurs[i].IdMoteur = 0;
            indiceChassis = gen.Next(0, 3);
            joueurs[i].Chassis = AssignerChassis(indiceChassis);
            joueurs[i].Puissance = AssignerPuissance(gen.Next(0,3));
            joueurs[i].Poid = AssignerPoid(indiceChassis);
           // joueurs[i].Chassis.tag = $"b{i}";
        }
    }

    private int AssignerPuissance(int indice)
    {
        int puissance = 150;
        if (indice == 1)
        {
            puissance = 50;
        }
        else
        {
            if (indice == 2)
            {
                puissance = 100;
            }
        }

        return puissance;
    }
    private int AssignerPoid(int indice)
    {
        int poid = 150;
        if (indice == 1)
        {
            poid = 50;
        }
        else
        {
            if (indice == 2)
            {
                poid = 100;
            }
        }

        return poid;
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
