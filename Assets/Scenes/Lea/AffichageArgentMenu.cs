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

    private DataCoin dataC;
    private string text;
    private void Start()
    {
        dataC = new DataCoin();
        AfficherCoin();
        
    }

    public void AfficherCoin()
    {
        if (SceneManager.GetActiveScene().buildIndex == scenePlayer1)
        {
            coin.text = $"{dataC.AccederNbCoins("InfoPlayer1.txt")}$";
        }
        else
        {
            if (SceneManager.GetActiveScene().buildIndex == scenePlayer2)
            {
                coin.text = $"{dataC.AccederNbCoins("InfoPlayer2.txt")}$";
            }
        }

        
    }
}

