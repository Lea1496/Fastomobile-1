using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class LeaderboardCreator : MonoBehaviour
{
    private TextMeshProUGUI[] leaderboardTexts;
    //private GameObject[] checkpoints;
    private GameObject ligne;
    private GameObject[] joueurs;
    private int compteur;
    private Player joueur;
    private void Start()
    {
        CreateLeaderbord();
    }

    private void CreateLeaderbord()
    {
        leaderboardTexts = GetComponentsInChildren<TextMeshProUGUI>();
        joueurs = GameObject.FindGameObjectsWithTag("Player");
        List<Player> joueursEnTrop = new List<Player>();

       for (int i = 0; i < joueurs.Length; i++)
       {
           if (joueurs[i].GetComponentInChildren<Player>().Vie <= 0)
           {
               joueursEnTrop.Add(joueurs[i].GetComponentInChildren<Player>());
           }
       }
       
       List<string> classement = new List<string>(12);
       
       for (int i = 0; i < 12; i++)
       {
           classement.Add("a");
       }
       
       for (int i = 0; i < joueurs.Length; i++)
       {
           for (int j = 0; j < joueurs.Length; j++)
           {
               joueur = joueurs[i].GetComponentInChildren<Player>();
               classement[joueur.Rang - 1] = joueur.Nom;
           }
           
       }

       if (joueursEnTrop.Count != 0)
       {
           for (int i = 0; i <  joueursEnTrop.Count; i++)
           {
               classement.Remove(joueursEnTrop[i].Nom);
               classement.Add(joueursEnTrop[i].Nom + " (Mort) " );
           }
           
       }
       
        for (int i = 0; i < classement.Count; i++)
        {
            leaderboardTexts[i].text = classement[i];
        }
    }
}
