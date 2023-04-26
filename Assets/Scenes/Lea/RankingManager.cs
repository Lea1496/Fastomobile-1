using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    //public List<string> ranks = new List<string>();

    private Player joueur;
    private string nomAChanger;
    private int compteur = 0;
    private List<string> joueursPassés = new List<string>(12);
    

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == 6 )
        {
            joueur = collider.GetComponentInParent<Player>();
      
            if (joueur.Rang != compteur + 1 && joueursPassés.Contains(joueur.Nom))
            {
                nomAChanger = GameData.Ranks[compteur];
                GameData.Ranks[compteur] = joueur.Nom;
                GameData.Ranks[joueur.Rang - 1] = nomAChanger;
                ChangerRangJoueur(nomAChanger, joueur.Rang);
                joueur.Rang = compteur + 1;
                joueursPassés.Add(joueur.Nom);
                ++compteur;
            }
            
            if (compteur == 12)
            {
                compteur = 0;
                joueursPassés.Clear();
            }
            /*if (!ranks.Contains(joueur.Nom))
            {
                ranks.Add(joueur.Nom);
            }
            
            
            if (joueur.IsMainPlayer)
            {
                for (int i = 0; i < ranks.Count; i++)
                {
                    if (ranks[i] == joueur.Nom)
                    {
                        joueur.Rang = i + 1;
                    }
                }
            }
            if (ranks.Count == 12)
            {
                ranks.Clear();
            }*/
        }
    }  
        

    private void ChangerRangJoueur(string nom, int rang)
    {
        for (int i = 0; i < GameData.LesJoueurs.Count; i++)
        {
            if (GameData.LesJoueurs[i].Nom == nom)
            {
                GameData.LesJoueurs[i].Rang = rang;
            }
        }
    }

}
