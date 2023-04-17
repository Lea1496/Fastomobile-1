using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardCreator : MonoBehaviour
{
    private TextMeshProUGUI[] leaderboardTexts;
    private GameObject[] checkpoints;
    private GameObject ligne;
    private List<string> ranking;

    private void Start()
    {
        CreateLeaderbord();
    }

    private void CreateLeaderbord()
    {
        leaderboardTexts = GetComponentsInChildren<TextMeshProUGUI>();
        checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        Debug.Log(checkpoints.Length);
       /* for (int i = 0; i < checkpoints.Length; i++)
        {
            if (checkpoints[i].layer == 12)
            {
                ligne = checkpoints[i];
            }
        }*/
       List<string> rang;
       ranking = new List<string>();
       rang = checkpoints[0].GetComponentInChildren<RankingManager>().ranks;
       Debug.Log(rang.Count);
       for (int i = 0; i < rang.Count; i++)
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

        for (int i = 0; i < ranking.Count; i++)
        {
            leaderboardTexts[i].text = ranking[i];
        }
    }
}
