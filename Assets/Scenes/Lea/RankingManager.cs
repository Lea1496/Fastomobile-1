using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Player))]
public class RankingManager : MonoBehaviour
{
    public List<string> ranks = new List<string>();

    private Player joueur;
    
  
    private void OnTriggerEnter(Collider collider)
    {
        joueur = collider.GetComponentInParent<Player>();
        if (!ranks.Contains(joueur.Nom))
        {
            ranks.Add(joueur.Nom);
        }
       
        Debug.Log(joueur.Nom);
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
        }
    }
}
