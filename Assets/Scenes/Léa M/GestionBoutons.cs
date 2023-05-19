///Ce code contient toutes les fonctions nécessaires pour les boutons et toggles se trouvant
/// sur les différentes scènes de notre projet.
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GestionBoutons : MonoBehaviour
{
    private DataCoin data = new DataCoin();
    private MenusData menusData = new MenusData();
    private const string CheminPlayer1 = "InfoPlayer1.txt";
    private const string CheminPlayer2 = "InfoPlayer2.txt";
    private const string CheminAvancé1 = "InfoAchetéAvancé1.txt";
    private const string CheminAvancé2 = "InfoAchetéAvancé2.txt";
    private const string CheminExpert1 = "InfoAchetéExpert1.txt";
    private const string CheminExpert2 = "InfoAchetéExpert2.txt";
    public Button avancé;
    public Button expert;
    public Toggle avancéToggle;
    public Toggle expertToggle;
    
    /// <summary>
    /// Cette fonction a été créée afin que nous puissions quitter le build pour la présentation finale
    /// </summary>
    public void FermerPartie()
    {
        Application.Quit();
    }
    /// <summary>
    /// Ce code permet de transformer un char en bool afin qu'il soit compatible
    /// avec notre système de conservation de l'information.
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    private bool ChangerCharEnBool(char c)
    {
        bool rep = false;
        if (c == 't')
        {
            rep = true;
        }
        else
        {
            if (c == 'f')
            {
                rep = false;
            }
        }

        return rep;
    }
    private bool avancéDéjàAcheté;
    private bool expertDéjàAcheté;
    private bool avancéDéjàAcheté2;
    private bool expertDéjàAcheté2;
    private void Awake()
    {
         avancéDéjàAcheté = ChangerCharEnBool(menusData.AfficherInfo(CheminAvancé1));
        expertDéjàAcheté = ChangerCharEnBool(menusData.AfficherInfo(CheminExpert1));
        avancéDéjàAcheté2 = ChangerCharEnBool(menusData.AfficherInfo(CheminAvancé2));
        expertDéjàAcheté2 = ChangerCharEnBool(menusData.AfficherInfo(CheminExpert2));
    }

    /// <summary>
    /// Ce code permet de passer d'une scène à l'autre afin d'afficher les menus requis
    /// selon le nombre de joueurs choisi.
    /// </summary>
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

    /// <summary>
    /// Ce code permet de créer une nouvelle partie avec les paramètres de base.
    /// </summary>
    public void NouvellePartie()
    {
        data.EnleverCoin(CheminPlayer1, CheminPlayer2);
        SceneManager.LoadScene(0);
        
         menusData.ChangerInfo(CheminAvancé1, 'f');
         menusData.ChangerInfo(CheminAvancé2, 'f');
         menusData.ChangerInfo(CheminExpert1, 'f');
         menusData.ChangerInfo(CheminExpert2, 'f');
        
    }
    /// <summary>
    /// Ce code permet de retourner au choix des véhicules sans passer par le menu de démarrage.
    /// </summary>
    public void RecommencerJeux()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Cette fonction permet de conserver les moteurs déverrouillés par le ou les joueurs et leur argent
    /// accumulé pendant les parties précédentes 
    /// </summary>
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
    
    /// <summary>
    /// Ces codes sont associés aux toggles du menu de démarrage. Ils permettent de choisir
    /// si le jeu sera joué seul ou en écran partagé.
    /// </summary>
    /// <param name="isUnJoueur"></param>
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
    
    /// <summary>
    /// Les codes suivants sont associés aux toggles du menu de choix de véhicules pour le joueur 1.
    /// Le véhicule par défaut est la voiture.
    /// </summary>
    /// <param name="isVoiture"></param>
    
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
    
    /// <summary>
    /// Les codes ci-dessous sont associés aux toggles du menu de choix de moteurs du joueur 1.
    /// Le moteur sélectionné par défaut est le moteur de base.
    /// </summary>
    /// <param name="isBase"></param>
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

    /// <summary>
    /// Cette fonction permet de valider si le joueur possède assez d'argent pour
    /// acheter un moteur. Après validation, la fonction enlèvera le montant au joueur puis affichera
    /// le nouveau nombre d'argent que le joueur possède après achat.
    /// </summary>
    /// <param name="prix"></param>
    /// <param name="player"></param>
    /// <returns>peutAcheter</returns>
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

    /// <summary>
    /// Cette fonction est lié au bouton du prix avancé. Elle permet au 2 joueurs d'acheter un moteur avancé.
    /// </summary>
    public void UnlockAvancé()
    {
        int prix = 50;
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            bool peutAcheter = Acheter(prix, CheminPlayer1);
            
            if (peutAcheter)
            {
                avancéToggle.gameObject.SetActive(true);
                avancéToggle.enabled = true;
                EventSystem.current.SetSelectedGameObject(avancéToggle.gameObject);
                avancé.gameObject.SetActive(false);
                menusData.ChangerInfo(CheminAvancé1, 't');
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            bool peutAcheter = Acheter(prix, CheminPlayer2);
            if (peutAcheter)
            {
                avancéToggle.gameObject.SetActive(true);
                avancéToggle.enabled = true;
                EventSystem.current.SetSelectedGameObject(avancéToggle.gameObject);
                avancé.gameObject.SetActive(false);
                menusData.ChangerInfo(CheminAvancé2, 't');
            }
        }
    }
    
    /// <summary>
    /// Cette fonction est lié au bouton du prix expert. Elle permet au 2 joueurs d'acheter un moteur expert.
    /// </summary>
    public void UnlockExpert()
    {
        int prix = 100;
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            bool peutAcheter = Acheter(prix,CheminPlayer1);
            if (peutAcheter)
            {
                expertToggle.gameObject.SetActive(true);
                expertToggle.enabled = true;
                EventSystem.current.SetSelectedGameObject(expertToggle.gameObject);
                expert.gameObject.SetActive(false);
               
                menusData.ChangerInfo(CheminExpert1, 't');
                
            }        
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            bool peutAcheter = Acheter(prix,CheminPlayer2);
            if (peutAcheter)
            {
                expertToggle.gameObject.SetActive(true);
                expertToggle.enabled = true;
                EventSystem.current.SetSelectedGameObject(expertToggle.gameObject);
                expert.gameObject.SetActive(false);
                
                menusData.ChangerInfo(CheminExpert2, 't');
            }        
        }
    }
    
    /// <summary>
    /// Les codes suivants sont associés aux toggles du menu de choix de véhicules pour le joueur 2.
    /// Le véhicule par défaut est la voiture.
    /// </summary>
    /// <param name="isVoiture"></param>
    //choix voiture
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
            GameData.P2.IdVéhicule = 1;
            GameData.P2.Vie = 125;
        }
    }
    public void TogglePolice2(bool isPolice)
    {
        if (isPolice)
        {
            GameData.P2.IdVéhicule = 2;
            GameData.P2.Vie = 75;
        }
    }

    /// <summary>
    /// Les codes ci-dessous sont associés aux toggles du menu de choix de moteurs du joueur 2.
    /// Le moteur sélectionné par défaut est le moteur de base.
    /// </summary>
    /// <param name="isBase"></param>
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
}

