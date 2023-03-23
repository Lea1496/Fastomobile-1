using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Player))]
public class RankingManager : MonoBehaviour
{
    public List<string> Ranks { private get;  set; }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider collider)
    {
        Ranks.Add(collider.GetComponent<Player>().Nom);
    }
}
