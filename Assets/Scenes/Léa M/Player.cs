
using System.Collections;

using UnityEngine;


public class Player : MonoBehaviour, IPlayer
{    
    public int Vie { get; set; }
    public string Nom { get; set; }
    public int Argent { get; set; }
    public int IdVéhicule { get; set; }
    public int IdMoteur { get; set; }
    public GameObject Chassis { get; set; }
    public int Puissance { get; set; }
    public int Poids { get; set; }
    public int Rang { get; set; }
    public bool IsMainPlayer { get; set; }
    public bool IsMainPlayer1 { get; set; }
    public bool IsMainPlayer2 { get; set; }
    public int Tour { get; set; }
    public bool IsFinished { get; set; }

    public int DernierCheckpointVisité { get; set; }
    public Player()
    {
        IsMainPlayer = false;
        IsMainPlayer1 = false;
        IsMainPlayer2 = false;
        Argent = 0;
        Tour = 0;
        IsFinished = false;
        DernierCheckpointVisité = 0;
    }

    public void CréerPlayer(int vie, string nom, int idVéhicule, int idMoteur, GameObject chassis, int puissance, int poids, bool isMainPlayer, int rang)
    {
        Vie = vie;
        Nom = nom;
        IdVéhicule = idVéhicule;
        IdMoteur = idMoteur;
        Chassis = chassis;
        Puissance = puissance;
        Poids = poids;
        IsMainPlayer = isMainPlayer;
        Rang = rang;
    }
    public void AjouterVie(int vieAjoutée)
    {
        Vie += vieAjoutée;
    }
    public void EnleverVie(int vieEnlevée)
    {
        Vie -= vieEnlevée;
    }

    public void AjouterArgent(int nbArgent)
    {
        Argent += nbArgent;
    }

    private void Start()
    {
        StartCoroutine(ActionnerAuto());
        StopCoroutine(ActionnerAuto());
    }

    private IEnumerator ActionnerAuto()
    {
        if (IsMainPlayer)
        {
            yield return new WaitForSeconds(3f);
            gameObject.GetComponent<GestionnaireTouches>().enabled = true;
        }
        else
        {
            yield return new WaitForSeconds(3f);
        }
    }

}
