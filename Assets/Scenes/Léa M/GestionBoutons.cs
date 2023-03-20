using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GestionBoutons : MonoBehaviour
{
    private DataCoin data = new DataCoin();
    public void DémarrerJeu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Choix voiture
    public void ToggleVoiture(bool isVoiture)
    {
        if (isVoiture)
        {
            GameData.P1.IdVéhicule = 0;
            GameData.P1.Vie = 100;
        }
    }
    public void ToggleCamion(bool isCamion)
    {
        if (isCamion)
        {
            GameData.P1.IdVéhicule = 1;
            GameData.P1.Vie = 200;
        }
    }
    public void ToggleMoto(bool isMoto)
    {
        if (isMoto)
        {
            GameData.P1.IdVéhicule = 2;
            GameData.P1.Vie = 50;
        }
    }

    //Choix Moteur
    public void ToggleBase(bool isBase)
    {
        if (isBase)
        {
            GameData.P1.IdMoteur = 0;
        }
    }
    public void ToggleAvancé(bool isAvancé)
    {
        if (isAvancé)
        {
            GameData.P1.IdMoteur = 1;
        }
    }
    public void ToggleExpert(bool isExpert)
    {
        if (isExpert)
        {
            GameData.P1.IdMoteur = 2;
        }
    }

    public bool Acheter(int prix)
    {
        bool peutAcheter = false;
        if (data.AccederNbCoins("InfoPlayer1.txt") >= prix)
        {
            data.AjouterCoin("InfoPlayer1.txt", -prix); 
            peutAcheter = true;
        }

        return peutAcheter;
    }
    public void UnlockAvancé()
    {
        int prix = 1000;
        bool PeutAcheter = Acheter(prix);
        
    }
    public void UnlockExpert()
    {
        int prix = 2000;
        bool PeutAcheter = Acheter(prix);
        
    }
    
}

