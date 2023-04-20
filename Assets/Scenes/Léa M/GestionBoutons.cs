using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GestionBoutons : MonoBehaviour
{
    private DataCoin data = new DataCoin();
    private const string CheminPlayer1 = "InfoPlayer1.txt";
    private const string CheminPlayer2 = "InfoPlayer2.txt";
    public Button avancé;
    public Button expert;
    public Toggle avancéToggle;
    public Toggle expertToggle;

    [SerializeField] private PlayerInputManager inputManager;

    public bool déjàAcheté;

    public void DémarrerJeu()
    {
        
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            if (GameData.P2.IsMainPlayer)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
            }
        }
        
        
    }

    public void NouvellePartie()
    {
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < checkpoints.Length; i++)
        {
            Destroy(checkpoints[i]);
        }
        data.EnleverCoin(CheminPlayer1, CheminPlayer2);
        SceneManager.LoadScene(0);
        //déjàAcheté = false;
        
    }
    public void RecommencerJeux()
    {
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < checkpoints.Length; i++)
        {
            Destroy(checkpoints[i]);
        }
        SceneManager.LoadScene(1);
    }
    public void ToggleUnJoueur(bool isUnJoueur)
    {
        if (isUnJoueur)
        {
            GameData.P1.IsMainPlayer = true;
            GameData.P2.IsMainPlayer = false;
            GameData.P1.IsMainPlayer = true;
        }
    }
    public void ToggleDeuxJoueurs(bool isDeuxJoueurs)
    {
        if (isDeuxJoueurs)
        {
            GameData.P1.IsMainPlayer = true;
            GameData.P2.IsMainPlayer = true;
            GameData.P1.IsMainPlayer = true;
            GameData.P2.IsMainPlayer = true;
        }
    }
    
    //Choix voiture
    public void ToggleVoiture(bool isVoiture)
    {
        
        if (isVoiture)
        {
            Debug.Log("ICI");
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

    public bool Acheter(int prix,string player)
    {
        bool peutAcheter = false;
        if (data.AccederNbCoins(player) >= prix)
        {
            data.AjouterCoin(player, -prix); 
            peutAcheter = true;
            GetComponentInChildren<AffichageArgentMenu>().AfficherCoin();
        }

        return peutAcheter;
    }
    // public bool Acheter(int prix)
    // {
    //     bool peutAcheter = false;
    //     if (data.AccederNbCoins("InfoPlayer1.txt") >= prix)
    //     {
    //         data.AjouterCoin("InfoPlayer1.txt", -prix); 
    //         peutAcheter = true;
    //         GetComponentInChildren<AffichageArgentMenu>().AfficherCoin();
    //     }
    //
    //     return peutAcheter;
    // }
    public void UnlockAvancé()
    {
        int prix = 50;
        bool peutAcheter = Acheter(prix,CheminPlayer1);
        //avancé.interactable = false;
        if (peutAcheter)
        {
            //avancé.interactable = true;
            avancéToggle.gameObject.SetActive(true);
            avancéToggle.enabled = true;
            avancé.gameObject.SetActive(false);
        }
        // if (avancé.isActiveAndEnabled)
        // {
        //     avancéToggle.enabled = peutAcheter; 
        //     avancé.gameObject.SetActive(!peutAcheter);
        // }
    }
    public void UnlockExpert()
    {
        int prix = 100;
        bool peutAcheter = Acheter(prix,CheminPlayer1);
        if (peutAcheter)
        {
            //avancé.interactable = true;
            expertToggle.gameObject.SetActive(true);
            expertToggle.enabled = true;
            expert.gameObject.SetActive(false);
        }
        // if (expert.isActiveAndEnabled)
        // {
        //     expertToggle.enabled = peutAcheter;
        //     expert.gameObject.SetActive(!peutAcheter);            
        // }
    }
    public void ToggleVoiture2(bool isVoiture)
    {
        if (isVoiture)
        {
            GameData.P2.IdVéhicule = 0;
            GameData.P2.Vie = 100;
        }
    }
    public void ToggleCamion2(bool isCamion)
    {
        if (isCamion)
        {
            Debug.Log("camion");
            GameData.P2.IdVéhicule = 1;
            GameData.P2.Vie = 200;
        }
    }
    public void ToggleMoto2(bool isMoto)
    {
        if (isMoto)
        {
            Debug.Log("ICI");
            GameData.P2.IdVéhicule = 2;
            GameData.P2.Vie = 50;
        }
    }

    //Choix Moteur
    public void ToggleBase2(bool isBase)
    {
        if (isBase)
        {
            GameData.P2.IdMoteur = 0;
        }
    }
    public void ToggleAvancé2(bool isAvancé)
    {
        if (isAvancé)
        {
            GameData.P2.IdMoteur = 1;
        }
    }
    public void ToggleExpert2(bool isExpert)
    {
        if (isExpert)
        {
            GameData.P2.IdMoteur = 2;
        }
    }

    // public bool Acheter2(int prix)
    // {
    //     bool peutAcheter = false;
    //     if (data.AccederNbCoins("InfoPlayer2.txt") >= prix)
    //     {
    //         data.AjouterCoin("InfoPlayer2.txt", -prix); 
    //         peutAcheter = true;
    //         GetComponentInChildren<AffichageArgentMenu>().AfficherCoin();
    //     }
    //
    //     return peutAcheter;
    // }
    public void UnlockAvancé2()
    {
        int prix = 50;
        bool peutAcheter = Acheter(prix,CheminPlayer2);
        expertToggle.enabled = true;

    }
    public void UnlockExpert2()
    {
        int prix = 100;
        bool peutAcheter = Acheter(prix,CheminPlayer2);
        
    }
    
}

