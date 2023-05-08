
using System.Collections.Generic;
using TMPro;

using UnityEngine;

public class LeaderboardCreator : MonoBehaviour
{
    private TextMeshProUGUI[] leaderboardTexts;
    //private GameObject[] checkpoints;
    private GameObject ligne;
    private List<Player> joueurs;
    private int compteur;
    private Player joueur;
    private void Start()
    {
        CreateLeaderbord();
    }

    private void CreateLeaderbord()
    {
        leaderboardTexts = GetComponentsInChildren<TextMeshProUGUI>();
        joueurs = GameData.LesJoueurs;
        Debug.Log(joueurs.Count);
        List<string> joueursEnTrop = GameData.ListeJoueursMorts;

        
       /*for (int i = 0; i < joueurs.Length; i++)
       {
           if (joueurs[i].GetComponentInChildren<Player>().Vie <= 0)
           {
               joueursEnTrop.Add(joueurs[i].GetComponentInChildren<Player>());
           }
       }*/
       
       List<string> classement = new List<string>(12);
       
       for (int i = 0; i < 12; i++)
       {
           classement.Add("");
       }
       
       for (int i = 0; i < joueurs.Count; i++)
       {
           
           classement[joueurs[i].Rang - 1] = joueurs[i].Nom;
       }

       if (joueursEnTrop.Count != 0)
       {
           for (int i = 0; i <  joueursEnTrop.Count; i++)
           {
               classement.Remove(joueursEnTrop[i]);
               classement.Add(joueursEnTrop[i]+ " (Mort) " );
           }
           
       }
       Debug.Log(classement.Count);
        for (int i = 0; i < classement.Count; i++)
        {
            leaderboardTexts[i].text = classement[i];
        }
    }
}
