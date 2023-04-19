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
    private List<string> ranking;
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
        //checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        joueurs = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log(leaderboardTexts.Length);
       /* for (int i = 0; i < checkpoints.Length; i++)
        {
            if (checkpoints[i].layer == 12)
            {
                ligne = checkpoints[i];
            }
        }*/
       List<Player> joueursEnTrop = new List<Player>();
       
       /*if (!joueurs[0].GetComponentInChildren<Player>().IsFinished)
       {
           joueurEnTrop = joueurs[0].GetComponentInChildren<Player>();
       }
       else
       {
           if ( (!joueurs[1].GetComponentInChildren<Player>().IsFinished && joueurs[1].GetComponentInChildren<Player>().IsMainPlayer2))
           {
               joueurEnTrop = joueurs[1].GetComponentInChildren<Player>();
           }
       }*/

       for (int i = 0; i < joueurs.Length; i++)
       {
           if (joueurs[i].GetComponentInChildren<Player>().Vie <= 0)
           {
               joueursEnTrop.Add(joueurs[i].GetComponentInChildren<Player>());
           }
       }
       
       List<string> rang;
       ranking = new List<string>();
       //rang = checkpoints[0].GetComponentInChildren<RankingManager>().ranks;
       rang = new List<string>(12);
       for (int i = 0; i < 12; i++)
       {
           rang.Add("a");
       }
        Debug.Log(joueurs.Length);
       int compt = 1;
       for (int i = 0; i < joueurs.Length; i++)
       {
           for (int j = 0; j < joueurs.Length; j++)
           {
               joueur = joueurs[i].GetComponentInChildren<Player>();
               rang[joueur.Rang - 1] = joueur.Nom;
           }
           
       }

       if (joueursEnTrop.Count != 0)
       {
           for (int i = 0; i <  joueursEnTrop.Count; i++)
           {
               rang.Remove(joueursEnTrop[i].Nom);
           }
           
       }
       Debug.Log(rang.Count);
      /* for (int i = 0; i < rang.Count; i++)
       {
           ranking.Add(rang[i]);
       }
       
        
        int compteur = 1;
        
        if (ranking.Count < 12)
        {
            Debug.Log(ranking.Count);
            while (ranking.Count < 2)
            {
                rang = checkpoints[checkpoints.Length - compteur++].GetComponentInChildren<RankingManager>().ranks;
                for (int i = 0; i < rang.Count; i++)
                {
                    if (!ranking.Contains(rang[i]))
                    {
                        ranking.Add(rang[i]);
                        Debug.Log(rang[i]);
                    }
                }
            } 
        }
*/
        for (int i = 0; i < rang.Count; i++)
        {
            leaderboardTexts[i].text = rang[i];
        }
    }
}
