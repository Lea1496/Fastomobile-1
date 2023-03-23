using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Player))]
public class RankingManager : MonoBehaviour
{
    private List<string> Ranks = new List<string>();

    private Player joueur;
    
  
    private void OnTriggerEnter(Collider collider)
    {
        joueur = collider.GetComponentInParent<Player>();
        Ranks.Add(joueur.Nom);
        if (joueur.IsMainPlayer)
        {
            for (int i = 0; i < Ranks.Count; i++)
            {
                if (Ranks[i] == joueur.Nom)
                {
                    joueur.Rang = i + 1;
                }
            }
        }
        if (Ranks.Count == 12)
        {
            Ranks.Clear();
        }
    }
}
