
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RankingManager : MonoBehaviour
{
    //public List<string> ranks = new List<string>();

    private Player joueur;
    private string nomAChanger;
    private int compteur = 0;
    private List<string> joueursPassés = new List<string>(12);
    public int indiceCheckpoint;
    
    private Text textSens;
    private int noText = 0;
    private bool aDeuxJoueurs;
    private bool joueurDéjàPassé;
    private void Start()
    {
        aDeuxJoueurs = GameData.P2.IsMainPlayer;
        if (!aDeuxJoueurs)
        {
            noText = 0;
        }
    }

    private int DéterminerIndice(Player joueur, bool aDeux)
    {
        int no = 0;
        if (aDeux)
        {
            if (joueur.IsMainPlayer)
            {
                no = 1;
                if (joueur.IsMainPlayer2)
                {
                    no = 2;
                }
            }
        }

        return no;
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == 6)
        {
            joueur = collider.GetComponentInParent<Player>();

            noText = DéterminerIndice(joueur, aDeuxJoueurs);
            joueurDéjàPassé = joueursPassés.Contains(joueur.Nom);
            if (joueurDéjàPassé && joueur.IsMainPlayer && joueur.DernierCheckpointVisité > indiceCheckpoint)
            {
                textSens = collider.GetComponent<TextesData>().lesTextes[noText];

                textSens.text = "Mauvais Sens!";
                StartCoroutine(AfficherMessage());
                StopCoroutine(AfficherMessage());
            }
            if (joueur.Rang != compteur + 1 && !joueurDéjàPassé)
            {
                nomAChanger = GameData.Ranks[compteur];
                GameData.Ranks[compteur] = joueur.Nom;
                GameData.Ranks[joueur.Rang - 1] = nomAChanger;
                ChangerRangJoueur(nomAChanger, joueur.Rang);
                joueur.Rang = compteur + 1;
                joueursPassés.Add(joueur.Nom);
                joueur.DernierCheckpointVisité = indiceCheckpoint;
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
    private IEnumerator AfficherMessage()
    {
        yield return new WaitForSeconds(2f);
        textSens.text = "";


    }

}


