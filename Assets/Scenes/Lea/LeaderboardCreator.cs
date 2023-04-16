using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
       /* for (int i = 0; i < checkpoints.Length; i++)
        {
            if (checkpoints[i].layer == 12)
            {
                ligne = checkpoints[i];
            }
        }*/
       Player joueurEnTrop = null;
       if (!joueurs[0].GetComponentInChildren<Player>().IsFinished)
       {
           joueurEnTrop = joueurs[0].GetComponentInChildren<Player>();
       }
       else
       {
           if ( (!joueurs[1].GetComponentInChildren<Player>().IsFinished && joueurs[1].GetComponentInChildren<Player>().IsMainPlayer2))
           {
               joueurEnTrop = joueurs[1].GetComponentInChildren<Player>();
           }
       }

       
       List<string> rang;
       ranking = new List<string>();
       //rang = checkpoints[0].GetComponentInChildren<RankingManager>().ranks;
       rang = new List<string>();

       int compt = 1;
       for (int i = 0; i < joueurs.Length; i++)
       {
           while (rang.Count != joueurs.Length)
           {
               for (int j = 0; j < joueurs.Length; j++)
               {
                   joueur = joueurs[i].GetComponentInChildren<Player>();
                   if (joueur.Rang == compt && !rang.Contains(joueur.Nom))
                   { 
                       rang.Add(joueur.Nom);
                       compt++;
                   }
               }
           }
       }

       if (joueurEnTrop != null)
       {
           rang.Remove(joueurEnTrop.Nom);
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
            leaderboardTexts[i].text = ranking[i];
        }
    }
}
