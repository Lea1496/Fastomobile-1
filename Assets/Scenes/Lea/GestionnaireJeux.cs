using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using UnityEngine.InputSystem;


[RequireComponent(typeof(ScriptSpline))]
[RequireComponent(typeof(CréateurDébutPartie))]
public class GestionnaireJeux : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int largeur;  
    public GameObject terrain;
    [SerializeField] private GameObject point;
    [SerializeField] private GameObject point1;
    [SerializeField] public GameObject obstalce1; // à changer
    [SerializeField] private GameObject obstacle2; // à changer
    [SerializeField] private Camera cam1;
    [SerializeField] private Camera cam2;
    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject bonus;
    [SerializeField] private GameObject checkpoint;
    [SerializeField] private Text textCoin;
    [SerializeField] private Text textRang;
    [SerializeField] private Text textRg;
    [SerializeField] private Text textVie;
    [SerializeField] private Text textCoin2;
    [SerializeField] private Text textRang2;
    [SerializeField] private Text textRg2;
    [SerializeField] private Text textVie2;
    [SerializeField] private Text textLaps;
    [SerializeField] private Text textLaps2;
    [SerializeField] private Text textFinish;
    [SerializeField] private Text textFinish2;
    [SerializeField] private Text textGameOver;
    [SerializeField] private Text textGameOver2;

    [SerializeField] private Speedometer speedometer;
    [SerializeField] private Speedometer speedometer2;
    [SerializeField] private GameObject PlayerData2;
    [SerializeField] private PlayerInputManager inputManager;
    private ScriptSpline créerRoute;
    
    private List<Vector3> chemin;
    private GameObject mainPlayer1;
    private GameObject mainPlayer2;
    private List<PlayerData> autos;
    private CréateurDébutPartie créateur;
    private Vector3 desiredPos;
    private Vector3 desiredPos2;
    private GameObject ligne;
    private int compteur = 0;
    private Transform target;
    private Transform target2;
    private Player mainPlayer1Live;
    private Player mainPlayer2Live;
   
    private bool isGameOver1 = false;
    private bool isGameOver2 = false;

    private string gameOver = "Game Over!";
    private string finish = "Finish!";

    private bool existsMainPlayer2 = false;
    private int speedP1 = 0;
    private int speedP2 = 0;
    public Player MainPlayer1
    {
        get => mainPlayer1.GetComponent<Player>();
    }
    public Player MainPlayer2
    {
        get => mainPlayer2.GetComponent<Player>();
    }
    public List<Vector3> Chemin
    {
        get => chemin;
    }
    public GameObject Coin
    {
        get => coin;
    }
    public List<PlayerData> Autos
    {
        get => autos;
    }
    
    // les deux prochaine fonctions viennent de
    // https://stackoverflow.com/questions/4501838/terminate-a-thread-after-an-interval-if-not-returned
    
    private void WorkThreadFunction()
    {
        try
        {
            chemin = new CréateurChemin(largeur).ListePos;
        }
        catch 
        {
            Refaire();
        }
    }
    private void Refaire()
    {
        Thread thread = new Thread(new ThreadStart(WorkThreadFunction)) ;
       
        thread.Start();
        if (!thread.Join(new TimeSpan(0, 0, 1)) && chemin == null)
        {
            thread.Abort();
            Debug.Log("Ça a pas marché");
            Refaire();
            
        }
    }

    void Awake()
   {
       
       créerRoute = GetComponent<ScriptSpline>();
       Refaire();
       new CréateurTerrain(largeur, terrain);
        créateur = GetComponent<CréateurDébutPartie>();
        chemin = new ScriptBézier(chemin).PointsSpline;
        créerRoute.FaireMesh(chemin);
        Vector3[] sommets = créerRoute.sommets;
        GameObject checkpoint;
        
        if (GameData.P2.IsMainPlayer)
        {
            cam2.gameObject.SetActive(true);
            cam1.rect = new Rect(0.5f, 0, 0.5f, 1);
            PlayerData2.SetActive(true);
        }
        
        //Instancie les checkpoints
        for (int i = 0; i < chemin.Count -2; i++)
        {
            checkpoint = Instantiate(this.checkpoint, new Vector3(0,0,0),
                this.checkpoint.transform.rotation);
            checkpoint.GetComponentInChildren<GénérateurCheckPoints>().FaireMesh(i* 2, sommets);
        }
        
        gameObject.GetComponentInChildren<CréateurLigneArrivée>().FaireMesh(new Vector3(chemin[chemin.Count -1].x, 0, chemin[chemin.Count -1].z +70),new Vector3(chemin[chemin.Count -1].x, 0, chemin[chemin.Count -1].z -70));
        //Instancie les coins, obstacles et bonus
        new GénérateurObjets().GénérerObjets(obstalce1, obstacle2, coin, bonus, sommets);
        //Crée la liste de joueurs
        autos = new GestionnairePlayer().Joueurs; //à changer
        //Crée le début de la partie
        créateur.CréerDébutPartie(autos, chemin);
       
        //TerrainData theTerrain = new Object2Terrain().CreateTerrain1(gameObject);
        //GameObject terrainObject = Terrain.CreateTerrainGameObject(theTerrain);
       // terrainObject.transform.SetLocalPositionAndRotation(new Vector3(-68,-1,-68), terrainObject.transform.rotation);
      
        mainPlayer1 = créateur.MainPlayer1;
        mainPlayer2 = créateur.MainPlayer2;
       /* if (mainPlayer2.GetComponentInChildren<Player>().IsMainPlayer2)
        {
            inputManager.JoinPlayer(1, -1, "Controller2");
        }*/
        
   }
    
    
    private void LateUpdate()
   {
       
       mainPlayer1Live = mainPlayer1.GetComponent<Player>();
       existsMainPlayer2 = GameData.P2.IsMainPlayer;

        speedometer.speed = (int)Math.Floor(mainPlayer1Live.GetComponent<Rigidbody>().velocity.magnitude) * 3;
   
       if (mainPlayer1Live.IsFinished)
       {
           //mainPlayer1.GetComponent<GestionnaireTouches>().enabled = false;
       
           ChangerAffichageÉcran(mainPlayer1, cam1, 1, finish, textFinish);
       }
       else
       {
           if (mainPlayer1Live.Vie <= 0)
           {
               //mainPlayer1.GetComponent<GestionnaireTouches>().enabled = false;
               ChangerAffichageÉcran(mainPlayer1, cam1, 1, gameOver, textGameOver);
               isGameOver1 = true;
           }
       }
   
       GérerCaméra(mainPlayer1Live, cam1);
   
       textCoin.text = $"Coin: {mainPlayer1Live.Argent.ToString()}";
       textRang.text = mainPlayer1Live.Rang.ToString();
       textVie.text = $"HP: {mainPlayer1Live.Vie.ToString()}";
       textLaps.text = $"{mainPlayer1Live.Tour}/3";
       
       

       if (existsMainPlayer2) 
       {
           mainPlayer2Live = mainPlayer2.GetComponent<Player>();
           speedometer2.speed = (int)Math.Floor(mainPlayer2Live.GetComponent<Rigidbody>().velocity.magnitude) * 3;
           if (mainPlayer2Live.IsFinished)
           {
               ChangerAffichageÉcran(mainPlayer2, cam2, 2, finish, textFinish2);
               if (isGameOver1)
               {
                   StartCoroutine(FinirPartie());
                   StopCoroutine(FinirPartie());
               }
           }
           else
           {
               if (mainPlayer2Live.Vie <= 0)
               {
                   mainPlayer2.GetComponent<GestionnaireTouches>().enabled = false;
                   ChangerAffichageÉcran(mainPlayer2, cam2, 2, gameOver, textGameOver2);
                   isGameOver2 = true;
                   if (mainPlayer1Live.IsFinished)
                   {
                       StartCoroutine(FinirPartie());
                       StopCoroutine(FinirPartie());
                   }

               }   
           }

           GérerCaméra(mainPlayer2Live, cam2);
          
           textCoin2.text = $"Coin: {mainPlayer2Live.Argent.ToString()}";
           textRang2.text = mainPlayer2Live.Rang.ToString();
           textVie2.text = $"HP: {mainPlayer2Live.Vie.ToString()}";
           textLaps2.text = $"{mainPlayer2Live.Tour}/3";
           if (mainPlayer1Live.Vie == 0 && mainPlayer2Live.Vie == 0)
           {
               SceneManager.LoadScene(5);
           }
       }

       if (mainPlayer1Live.Vie == 0 && !GameData.P2.IsMainPlayer )
       {
           SceneManager.LoadScene(5);
       }

       
   }

   //Cette fonction vient de :https://github.com/bhavik66/Unity3D-Ranking-System/tree/master/Assets/RankingSystem/Scripts
   private void GérerCaméra(Player mainPlayer, Camera cam)
   {
       // Calculate the current rotation angles
       Transform target = mainPlayer.transform;
       float wantedRotationAngle = target.eulerAngles.y;
       float wantedHeight = target.position.y + 25;

       float  currentRotationAngle = cam.transform.eulerAngles.y;
       float currentHeight = cam.transform.position.y;

       // Damp the rotation around the y-axis
       currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, 3f * Time.deltaTime);

       // Damp the height
       currentHeight = Mathf.Lerp(currentHeight, wantedHeight, 2f * Time.deltaTime);

       // Convert the angle into a rotation
       Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

       // Set the position of the camera on the x-z plane to:
       // distance meters behind the target
       cam.transform.position = target.position;
       cam.transform.position -= currentRotation * Vector3.forward * 40;

       cam.transform.rotation = Quaternion.Slerp(cam.transform.rotation, currentRotation, 3f * Time.deltaTime);

       // Set the height of the camera
       cam.transform.position = new Vector3(cam.transform.position.x, currentHeight, cam.transform.position.z);

       // Always look at the target
       cam.transform.LookAt(target);
   }
   private IEnumerator FinirPartie()
   {
       yield return new WaitForSeconds(2f);
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }
   private void ChangerAffichageÉcran(GameObject player, Camera cam, int indice, string action, Text text)
   {
       player.GetComponent<GestionnaireTouches>().enabled = false;

       MeshRenderer[] renderers = player.GetComponentsInChildren<MeshRenderer>();

       for (int i = 0; i < renderers.Length; i++)
       {
           renderers[i].enabled = false;
       }
       
       cam.clearFlags = CameraClearFlags.SolidColor;
       DésactiverTextes(indice);
       cam.cullingMask = 0;
       text.text = action;
       if (existsMainPlayer2)
       {
           text.alignment = TextAnchor.MiddleRight;
       }

       
   }

   private void DésactiverTextes(int indice)
   {
       if (indice == 1)
       {
           textCoin.enabled = false;
           textRang.enabled = false;
           textVie.enabled = false;
           textLaps.enabled = false;
           textRg.enabled = false;
           
       }
       else
       {
           textCoin2.enabled = false;
           textRang2.enabled = false;
           textVie2.enabled = false;
           textLaps2.enabled = false;
           textRg2.enabled = false;
           
       }
   }
   
   
}
