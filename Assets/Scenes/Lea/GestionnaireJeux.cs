using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;


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
    [SerializeField] private Text textCn;
    [SerializeField] private Text textRang;
    [SerializeField] private Text textRg;
    [SerializeField] private Text textVie;
    [SerializeField] private Text textV;
    [SerializeField] private Text textCoin2;
    [SerializeField] private Text textCn2;
    [SerializeField] private Text textRang2;
    [SerializeField] private Text textRg2;
    [SerializeField] private Text textVie2;
    [SerializeField] private Text textV2;
    [SerializeField] private Text textLaps;
    [SerializeField] private Text textLaps2;
    [SerializeField] private Text textFinish;
    [SerializeField] private Text textFinish2;
    [SerializeField] private Text textGameOver;
    [SerializeField] private Text textGameOver2;
    [SerializeField] private GameObject PlayerData2;
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
    private float wantedRotationAngle;
    private float wantedHeight;
    private bool isGameOver1 = false;
    private bool isGameOver2 = false;
    private float currentRotationAngle;
    private float currentHeight;
    private Quaternion currentRotation;
    
    private float wantedRotationAngle2;
    private float wantedHeight2;

    private float currentRotationAngle2;
    private float currentHeight2;
    private Quaternion currentRotation2;
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
        
        //Instancie les coins, obstacles et bonus
        new GénérateurObjets().GénérerObjets(obstalce1, obstacle2, coin, bonus, sommets);
        //Crée la liste de joueurs
        autos = new GestionnairePlayer().Joueurs; //à changer
        //Crée le début de la partie
        créateur.CréerDébutPartie(autos, chemin, sommets);
       
        //TerrainData theTerrain = new Object2Terrain().CreateTerrain1(gameObject);
        //GameObject terrainObject = Terrain.CreateTerrainGameObject(theTerrain);
       // terrainObject.transform.SetLocalPositionAndRotation(new Vector3(-68,-1,-68), terrainObject.transform.rotation);
      
        mainPlayer1 = créateur.MainPlayer1;
        mainPlayer2 = créateur.MainPlayer2;
   }

   private void LateUpdate()
   {
       mainPlayer1Live = mainPlayer1.GetComponent<Player>();

       if (mainPlayer1Live.IsFinished)
       {
           mainPlayer1.GetComponent<GestionnaireTouches>().enabled = false;
           textFinish.enabled = true;
       }

       if (mainPlayer1Live.Vie <= 0)
       {
           mainPlayer1.GetComponent<GestionnaireTouches>().enabled = false;
          GameOver(cam1, 1);
          isGameOver1 = true;
       }
       //Ce code vient de :https://github.com/bhavik66/Unity3D-Ranking-System/tree/master/Assets/RankingSystem/Scripts
       
       // Calculate the current rotation angles
       target = mainPlayer1.transform;
       wantedRotationAngle = target.eulerAngles.y;
       wantedHeight = target.position.y + 20;

       currentRotationAngle = cam1.transform.eulerAngles.y;
       currentHeight = cam1.transform.position.y;

       // Damp the rotation around the y-axis
       currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, 3f * Time.deltaTime);

       // Damp the height
       currentHeight = Mathf.Lerp(currentHeight, wantedHeight, 2f * Time.deltaTime);

       // Convert the angle into a rotation
       currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

       // Set the position of the camera on the x-z plane to:
       // distance meters behind the target
       cam1.transform.position = target.position;
       cam1.transform.position -= currentRotation * Vector3.forward * 30;

       cam1.transform.rotation = Quaternion.Slerp(cam1.transform.rotation, currentRotation, 3f * Time.deltaTime);

       // Set the height of the camera
       cam1.transform.position = new Vector3(cam1.transform.position.x, currentHeight, cam1.transform.position.z);

       // Always look at the target
       cam1.transform.LookAt(target);
       
       //Mon code
       textCoin.text = mainPlayer1Live.Argent.ToString();
       textRang.text = mainPlayer1Live.Rang.ToString();
       textVie.text = mainPlayer1Live.Vie.ToString();
       textLaps.text = $"{mainPlayer1Live.Tour}/3";

       if (GameData.P2.IsMainPlayer) 
       {
           mainPlayer2Live = mainPlayer2.GetComponent<Player>();
           if (mainPlayer2Live.IsFinished)
           {
               mainPlayer2.GetComponent<GestionnaireTouches>().enabled = false;
               textFinish2.enabled = true;
               if (isGameOver1)
               {
                   StartCoroutine(FinirPartie());
                   StopCoroutine(FinirPartie());
               }
           }
           if (mainPlayer2Live.Vie <= 0)
           {
               mainPlayer2.GetComponent<GestionnaireTouches>().enabled = false;
               GameOver(cam2, 2);
               isGameOver2 = true;
               if (mainPlayer1Live.IsFinished)
               {
                   StartCoroutine(FinirPartie());
                   StopCoroutine(FinirPartie());
               }

           }
           
           //Ce code vient de :https://github.com/bhavik66/Unity3D-Ranking-System/tree/master/Assets/RankingSystem/Scripts
       
           // Calculate the current rotation angles
           target2 = mainPlayer2.transform;
           wantedRotationAngle2 = target2.eulerAngles.y;
           wantedHeight2 = target2.position.y + 20;

           currentRotationAngle2 = cam2.transform.eulerAngles.y;
           currentHeight2 = cam2.transform.position.y;

           // Damp the rotation around the y-axis
           currentRotationAngle2 = Mathf.LerpAngle(currentRotationAngle2, wantedRotationAngle2, 3f * Time.deltaTime);

           // Damp the height
           currentHeight2 = Mathf.Lerp(currentHeight2, wantedHeight2, 2f * Time.deltaTime);

           // Convert the angle into a rotation
           currentRotation2 = Quaternion.Euler(0, currentRotationAngle2, 0);

           // Set the position of the camera on the x-z plane to:
           // distance meters behind the target
           cam2.transform.position = target2.position;
           cam2.transform.position -= currentRotation2 * Vector3.forward * 30;

           cam2.transform.rotation = Quaternion.Slerp(cam2.transform.rotation, currentRotation2, 3f * Time.deltaTime);

           // Set the height of the camera
           cam2.transform.position = new Vector3(cam2.transform.position.x, currentHeight2, cam2.transform.position.z);

           // Always look at the target
           cam2.transform.LookAt(target2);
       
           //Mon code
           textCoin2.text = mainPlayer2Live.Argent.ToString();
           textRang2.text = mainPlayer2Live.Rang.ToString();
           textVie2.text = mainPlayer2Live.Vie.ToString();
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

   private void GérerCaméra(Player mainPlayer, Camera cam)
   {
       Player mainPlayerLive = mainPlayer.GetComponent<Player>();
       
       //Ce code vient de :https://github.com/bhavik66/Unity3D-Ranking-System/tree/master/Assets/RankingSystem/Scripts
       
       // Calculate the current rotation angles
       Transform target = mainPlayer.transform;
       float wantedRotationAngle = target.eulerAngles.y;
       float wantedHeight = target.position.y + 20;

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
       cam1.transform.position = target.position;
       cam1.transform.position -= currentRotation * Vector3.forward * 30;

       cam1.transform.rotation = Quaternion.Slerp(cam1.transform.rotation, currentRotation, 3f * Time.deltaTime);

       // Set the height of the camera
       cam1.transform.position = new Vector3(cam1.transform.position.x, currentHeight, cam1.transform.position.z);

       // Always look at the target
       cam1.transform.LookAt(target);
   }
   private IEnumerator FinirPartie()
   {
       yield return new WaitForSeconds(1f);
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }
   private void GameOver(Camera cam, int indice)
   {
       cam.clearFlags = CameraClearFlags.SolidColor;
       cam.cullingMask = 0;
       
       DésactiverTextes(indice);
   }

   private void DésactiverTextes(int indice)
   {
       if (indice == 1)
       {
           textCoin.enabled = false;
           textRang.enabled = false;
           textVie.enabled = false;
           textLaps.enabled = false;
           textCn.enabled = false;
           textRg.enabled = false;
           textV.enabled = false;
           textGameOver.enabled = true;
       }
       else
       {
           textCoin2.enabled = false;
           textRang2.enabled = false;
           textVie2.enabled = false;
           textLaps2.enabled = false;
           textCn2.enabled = false;
           textRg2.enabled = false;
           textV2.enabled = false;
           textGameOver2.enabled = true;
       }
   }
   
   
}
