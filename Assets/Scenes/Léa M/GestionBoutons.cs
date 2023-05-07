
using UnityEngine;
using UnityEngine.EventSystems;
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
    // public Button avancé2;
    // public Button expert2;
    // public Toggle avancéToggle2;
    // public Toggle expertToggle2;

  
    
    private bool avancéDéjàAcheté = GameData.P1.DéjàAchetéAvancé;
    private bool expertDéjàAcheté = GameData.P1.DéjàAchetéExpert;
    private bool avancéDéjàAcheté2 = GameData.P2.DéjàAchetéAvancé;
    private bool expertDéjàAcheté2 = GameData.P2.DéjàAchetéExpert;
    // public void Start()
    // {
    //     avancé = Instantiate(avancé);
    //     avancé.enabled = true;
    //     avancé.gameObject.SetActive(true);
    //     expert = Instantiate(expert);
    //     expert.enabled = true;
    //     expert.gameObject.SetActive(true);
    //
    //     // avancé2 = Instantiate(avancé2);
    //     // avancé2.enabled = true;
    //     // avancé2.gameObject.SetActive(true);
    //     // expert2 = Instantiate(expert2);
    //     // expert2.enabled = true;
    //     // expert2.gameObject.SetActive(true);
    // }

    // public void DefaultSettings(bool avancéDéjàAcheté, bool expertDéjàAcheté,
    //     bool expertDéjàAcheté2, bool avancéDéjàAcheté2)
    // {
    //     avancéToggle.gameObject.SetActive(avancéDéjàAcheté);
    //     avancé.enabled = !avancéDéjàAcheté;
    //     avancéToggle.enabled = avancéDéjàAcheté;
    //     avancé.gameObject.SetActive(!avancéDéjàAcheté);
    //     
    //     expertToggle.gameObject.SetActive(expertDéjàAcheté);
    //     expert.enabled = !expertDéjàAcheté;
    //     expertToggle.enabled = expertDéjàAcheté;
    //     expert.gameObject.SetActive(!expertDéjàAcheté);
    //     
    //     avancéToggle2.gameObject.SetActive(avancéDéjàAcheté2);
    //     avancé2.enabled = !avancéDéjàAcheté2;
    //     avancéToggle2.enabled = avancéDéjàAcheté2;
    //     avancé2.gameObject.SetActive(!avancéDéjàAcheté2);
    //     
    //     expertToggle2.gameObject.SetActive(expertDéjàAcheté2);
    //     expert2.enabled = !expertDéjàAcheté2;
    //     expertToggle2.enabled = expertDéjàAcheté2;
    //     expert2.gameObject.SetActive(!expertDéjàAcheté);
    // }

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
        //GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        //for (int i = 0; i < players.Length; i++)
        {
            //Destroy(players[i]);
        }
        data.EnleverCoin(CheminPlayer1, CheminPlayer2);
        SceneManager.LoadScene(0);
        // avancéDéjàAcheté = false;
        // expertDéjàAcheté = false;
        // avancéDéjàAcheté2 = false;
        // expertDéjàAcheté2 = false;
        // avancéToggle.gameObject.SetActive(avancéDéjàAcheté);
        // avancé.enabled = !avancéDéjàAcheté;
        // avancéToggle.enabled = avancéDéjàAcheté;
        // avancé.gameObject.SetActive(!avancéDéjàAcheté);
        //
        // expertToggle.gameObject.SetActive(expertDéjàAcheté);
        // expert.enabled = !expertDéjàAcheté;
        // expertToggle.enabled = expertDéjàAcheté;
        // expert.gameObject.SetActive(!expertDéjàAcheté);
        GameData.P1.DéjàAchetéAvancé = false;
        GameData.P1.DéjàAchetéExpert = false;
        GameData.P2.DéjàAchetéAvancé = false;
        GameData.P2.DéjàAchetéExpert = false;
    }
    public void RecommencerJeux()
    {
        GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < checkpoints.Length; i++)
        {
            Destroy(checkpoints[i]);
        }
        SceneManager.LoadScene(1);
        
        // avancéDéjàAcheté = GameData.P1.DéjàAchetéAvancé;
        // expertDéjàAcheté = GameData.P1.DéjàAchetéExpert;
        // avancéDéjàAcheté2 = GameData.P2.DéjàAchetéAvancé;
        // expertDéjàAcheté2 = GameData.P2.DéjàAchetéExpert;
        
        // Debug.Log(avancéDéjàAcheté);
        // Debug.Log(avancéDéjàAcheté2);
        // Debug.Log(expertDéjàAcheté);
        // Debug.Log(expertDéjàAcheté2);

        //avancéToggle = Instantiate(original: avancéToggle);
        // avancéToggle.gameObject.SetActive(avancéDéjàAcheté);
        // avancé.enabled = !avancéDéjàAcheté;
        // avancéToggle.enabled = avancéDéjàAcheté;
        // avancé.gameObject.SetActive(!avancéDéjàAcheté);
        //
        // expertToggle.gameObject.SetActive(expertDéjàAcheté);
        // expert.enabled = !expertDéjàAcheté;
        // expertToggle.enabled = expertDéjàAcheté;
        // expert.gameObject.SetActive(!expertDéjàAcheté);
        
        
        
        // avancéToggle2.gameObject.SetActive(avancéDéjàAcheté2);
        // avancé2.enabled = !avancéDéjàAcheté2;
        // avancéToggle2.enabled = avancéDéjàAcheté2;
        // avancé2.gameObject.SetActive(!avancéDéjàAcheté2);
        //
        // expertToggle2.gameObject.SetActive(expertDéjàAcheté2);
        // expert2.enabled = !expertDéjàAcheté2;
        // expertToggle2.enabled = expertDéjàAcheté2;
        // expert2.gameObject.SetActive(!expertDéjàAcheté);
        
    }

    public void MenuMoteurProchainePartie()
    {
        
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            avancéToggle.gameObject.SetActive(avancéDéjàAcheté);
            avancé.enabled = !avancéDéjàAcheté;
            avancéToggle.enabled = avancéDéjàAcheté;
            avancé.gameObject.SetActive(!avancéDéjàAcheté);

            expertToggle.gameObject.SetActive(expertDéjàAcheté);
            expert.enabled = !expertDéjàAcheté;
            expertToggle.enabled = expertDéjàAcheté;
            expert.gameObject.SetActive(!expertDéjàAcheté);
        }

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            avancéToggle.gameObject.SetActive(avancéDéjàAcheté2);
            avancé.enabled = !avancéDéjàAcheté2;
            avancéToggle.enabled = avancéDéjàAcheté2;
            avancé.gameObject.SetActive(!avancéDéjàAcheté2);

            expertToggle.gameObject.SetActive(expertDéjàAcheté2);
            expert.enabled = !expertDéjàAcheté2;
            expertToggle.enabled = expertDéjàAcheté2;
            expert.gameObject.SetActive(!expertDéjàAcheté2);
            
        }

    }
    public void ToggleUnJoueur(bool isUnJoueur)
    {
        if (isUnJoueur)
        {
            GameData.P1.IsMainPlayer = true;
            GameData.P2.IsMainPlayer = false;
        }
    }
    public void ToggleDeuxJoueurs(bool isDeuxJoueurs)
    {
        if (isDeuxJoueurs)
        {
            GameData.P1.IsMainPlayer = true;
            GameData.P2.IsMainPlayer = true;
        }
    }
    
    //Choix voiture
    public void ToggleVoiture(bool isVoiture)
    {
        
        if (isVoiture)
        {
            //Debug.Log("ICI");
            GameData.P1.IdVéhicule = 0;
            GameData.P1.Vie = 100;
        }
    }
    public void ToggleCamion(bool isCamion)
    {
        if (isCamion)
        {
            GameData.P1.IdVéhicule = 1;
            GameData.P1.Vie = 125;
        }
    }
    public void TogglePolice(bool isPolice)
    {
        if (isPolice)
        {
            GameData.P1.IdVéhicule = 2;
            GameData.P1.Vie = 75;
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
            //Debug.Log(gameObject.name);
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
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            bool peutAcheter = Acheter(prix, CheminPlayer1);
            //avancé.interactable = false;
            if (peutAcheter)
            {
                //avancé.interactable = true;
                avancéToggle.gameObject.SetActive(true);
                avancéToggle.enabled = true;
                EventSystem.current.SetSelectedGameObject(avancéToggle.gameObject);
                avancé.gameObject.SetActive(false);
                GameData.P1.DéjàAchetéAvancé = true;
                
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            bool peutAcheter = Acheter(prix, CheminPlayer2);
            //avancé.interactable = false;
            if (peutAcheter)
            {
                //avancé.interactable = true;
                avancéToggle.gameObject.SetActive(true);
                avancéToggle.enabled = true;
                EventSystem.current.SetSelectedGameObject(avancéToggle.gameObject);
                avancé.gameObject.SetActive(false);
                GameData.P2.DéjàAchetéAvancé = true;
            }
        }
        
        //avancéDéjàAcheté = true;
        
        // if (avancé.isActiveAndEnabled)
        // {
        //     avancéToggle.enabled = peutAcheter; 
        //     avancé.gameObject.SetActive(!peutAcheter);
        // }
    }
    public void UnlockExpert()
    {
        int prix = 100;
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            bool peutAcheter = Acheter(prix,CheminPlayer1);
            if (peutAcheter)
            {
                //avancé.interactable = true;
                expertToggle.gameObject.SetActive(true);
                expertToggle.enabled = true;
                EventSystem.current.SetSelectedGameObject(expertToggle.gameObject);
                expert.gameObject.SetActive(false);
                GameData.P1.DéjàAchetéExpert = true;
            }        
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            bool peutAcheter = Acheter(prix,CheminPlayer2);
            if (peutAcheter)
            {
                //avancé.interactable = true;
                expertToggle.gameObject.SetActive(true);
                expertToggle.enabled = true;
                EventSystem.current.SetSelectedGameObject(expertToggle.gameObject);
                expert.gameObject.SetActive(false);
                GameData.P2.DéjàAchetéExpert = true;
            }        
        }
        
        //expertDéjàAcheté = true;
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
            //Debug.Log("camion");
            GameData.P2.IdVéhicule = 1;
            GameData.P2.Vie = 200;
        }
    }
    public void TogglePolice2(bool isPolice)
    {
        if (isPolice)
        {
            //Debug.Log("ICI");
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
    // public void UnlockAvancé2()
    // {
    //     int prix = 50;
    //     bool peutAcheter = Acheter(prix,CheminPlayer2);
    //     //expertToggle.enabled = true;
    //     if (peutAcheter)
    //     {
    //         //avancé.interactable = true;
    //         avancéToggle2.gameObject.SetActive(true);
    //         avancéToggle2.enabled = true;
    //         avancé2.gameObject.SetActive(false);
    //     }
    //
    //     avancéDéjàAcheté2 = true;
    // }
    // public void UnlockExpert2()
    // {
    //     int prix = 100;
    //     bool peutAcheter = Acheter(prix,CheminPlayer2);
    //     if (peutAcheter)
    //     {
    //         //avancé.interactable = true;
    //         expertToggle2.gameObject.SetActive(true);
    //         expertToggle2.enabled = true;
    //         expert2.gameObject.SetActive(false);
    //     }
    //
    //     expertDéjàAcheté2 = true;
    // }
    
}

