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
using UnityEngine.UI;
using Debug = UnityEngine.Debug;


[RequireComponent(typeof(ScriptSpline))]
[RequireComponent(typeof(CréateurDébutPartie))]
public class GestionnaireJeux : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int largeur;
    [SerializeField] private GameObject terrain;
    [SerializeField] private GameObject point;
    [SerializeField] private GameObject point1;
    [SerializeField] public GameObject obstalce1; // à changer
    [SerializeField] private GameObject obstacle2; // à changer
   /* [SerializeField] private GameObject auto;
    [SerializeField] private GameObject moto;
    [SerializeField] private GameObject camion;
    [SerializeField] private GameObject arc;
    [SerializeField] private GameObject ligneArrivée;*/
    [SerializeField] private Camera cam1;
    [SerializeField] private Camera cam2;
    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject checkpoint;
    [SerializeField] private Text textCoin;
    [SerializeField] private Text textRang;
    [SerializeField] private Text textVie;
    [SerializeField] private Text textCoin2;
    [SerializeField] private Text textRang2;
    [SerializeField] private Text textVie2;
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
    public List<string> ranking;
    private Player mainPlayer1Live;
    private Player mainPlayer2Live;
    private float wantedRotationAngle;
    private float wantedHeight;

    private float currentRotationAngle;
    private float currentHeight;
    private Quaternion currentRotation;
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
        
        //Instancie les checkpoints
        for (int i = 0; i < chemin.Count -2; i++)
        {
            checkpoint = Instantiate(this.checkpoint, new Vector3(0,0,0),
                this.checkpoint.transform.rotation);
            checkpoint.GetComponentInChildren<GénérateurCheckPoints>().FaireMesh(i* 2, sommets);
        }
        
        //Instancie les obstacles 
        new GénérateurObstacles(obstalce1, obstacle2, sommets);
        //Instancie les coins
        new GénérateurCoins().GénérerCoins(15, sommets, coin);
        //Crée la liste de joueurs
        autos = new GestionnairePlayer(/*auto, moto, camion,*/ ).Joueurs; //à changer
        //Crée le début de la partie
        créateur.CréerDébutPartie(autos, chemin, sommets);
        mainPlayer1 = créateur.MainPlayer1;
        mainPlayer2 = créateur.MainPlayer2;
   }

   private void LateUpdate()
   {
       mainPlayer1Live = mainPlayer1.GetComponent<Player>();
       
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

       if (mainPlayer2 != null) 
       {
           mainPlayer2Live = mainPlayer2.GetComponent<Player>();
           
           //Ce code vient de :https://github.com/bhavik66/Unity3D-Ranking-System/tree/master/Assets/RankingSystem/Scripts
          
           // Calculate the current rotation angles
           target = mainPlayer2.transform;
           wantedRotationAngle = target.eulerAngles.y;
           wantedHeight = target.position.y + 20;

           currentRotationAngle = cam2.transform.eulerAngles.y;
           currentHeight = cam2.transform.position.y;

           // Damp the rotation around the y-axis
           currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, 3f * Time.deltaTime);

           // Damp the height
           currentHeight = Mathf.Lerp(currentHeight, wantedHeight, 2f * Time.deltaTime);

           // Convert the angle into a rotation
           currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

           // Set the position of the camera on the x-z plane to:
           // distance meters behind the target
           cam2.transform.position = target.position;
           cam2.transform.position -= currentRotation * Vector3.forward * 30;

           cam1.transform.rotation = Quaternion.Slerp(cam1.transform.rotation, currentRotation, 3f * Time.deltaTime);

           // Set the height of the camera
           cam2.transform.position = new Vector3(cam2.transform.position.x, currentHeight, cam2.transform.position.z);

           // Always look at the target
           cam2.transform.LookAt(target);
       
           //Mon code
           textCoin2.text = mainPlayer2Live.Argent.ToString();
           textRang2.text = mainPlayer2Live.Rang.ToString();
           textVie2.text = mainPlayer2Live.Vie.ToString();
       }
       
   }
   
}
