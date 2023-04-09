using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AffichageArgentMenu : MonoBehaviour
{
    private int scenePlayer1 = 1;
    private int scenePlayer2 = 2;

    private StreamReader fluxLecture;
    const string Chemin = "Assets/";
    [SerializeField] private TMP_Text coin;

    private string text;
    private void Start()
    {
        AfficherCoin();
    }

    public void AfficherCoin()
    {
        if (SceneManager.GetActiveScene().buildIndex == scenePlayer1)
        {
            using (fluxLecture = new StreamReader(Chemin + "InfoPlayer1.txt"))
            {
                
                text = $"{fluxLecture.ReadLine()}$";
                coin.text = text;
            }
        }
        else
        {
            if (SceneManager.GetActiveScene().buildIndex == scenePlayer2)
            {
                using (fluxLecture = new StreamReader(Chemin + "InfoPlayer2.txt"))
                {
                    text = $"{fluxLecture.ReadLine()}$";
                    coin.text = text;

                }
            }
        }

        
    }
}

