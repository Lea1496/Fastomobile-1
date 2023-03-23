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
    [SerializeField] private GameObject auto;
    [SerializeField] private GameObject moto;
    [SerializeField] private GameObject camion;
    [SerializeField] private GameObject arc;
    [SerializeField] private GameObject ligneArrivée;
    [SerializeField] private Camera cam1;
    [SerializeField] private Camera cam2;
    [SerializeField] private GameObject coin;
    [SerializeField] private GameObject obj;
    [SerializeField] private Text textCoin;
    private ScriptSpline CréerRoute;
    
    private List<Vector3> chemin;
    private GameObject mainPlayer1;
    private GameObject mainPlayer2;
    private List<PlayerData> autos;
    Vector3 offSet = new Vector3(-30, 20,0 );
    private CréateurDébutPartie créateur;
    private Vector3 desiredPos;
    private Vector3 desiredPos2;
    private GameObject ligne;
    private int compteur = 0;
    private Transform target;
    public List<string> ranking;
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
    /*private void WorkThreadFunction1()
    {
        try
        {
            //new CréateurTerrain(largeur, terrain);
            Debug.Log("ici!!");
            chemin = new ScriptBézier(chemin).PointsSpline;
            Debug.Log(Chemin.Count);
            CréerRoute.FaireMesh(chemin, point1, point);
            Debug.Log("ici2");
            Vector3[] sommets = CréerRoute.sommets;
            for (int i = 0; i < chemin.Count; i++)
            {
                //Instantiate(obj,  Vector3.Lerp(sommets[compteur++], sommets[compteur++], 0.5f), obj.transform.rotation).GetComponentInChildren<GénérateurCheckPoints>().FaireMesh(i, chemin);
            }
            new GénérateurObstacles(chemin, obstalce1, obstacle2, sommets);
            Debug.Log(sommets.Length);
        
            new GénérateurCoins().GénérerCoins(15, chemin, coin);
            autos = new GestionnairePlayer(auto, moto, camion, 1).Joueurs; //à changer
            //créateur = new CréateurDébutPartie(autos, arc, ligneArrivée, chemin[chemin.Count - 1], coin, chemin);
            mainPlayer1 = créateur.MainPlayer1;
            mainPlayer2 = créateur.MainPlayer2;
        }
        catch 
        {
           
        }
    }
    private void Refaire1()
    {
        Thread thread = new Thread(new ThreadStart(WorkThreadFunction1)) ;
       
        thread.Start();
        if (!thread.Join(new TimeSpan(0, 0, 1)))
        {
            Debug.Log("Ça a pas marché");
            thread.Abort();
           
            
            
        }
    }*/

    
   void Awake()
   {
       
       CréerRoute = GetComponent<ScriptSpline>();
       Refaire();
       //Refaire1();
        new CréateurTerrain(largeur, terrain);
        créateur = GetComponent<CréateurDébutPartie>();
        chemin = new ScriptBézier(chemin).PointsSpline;
        CréerRoute.FaireMesh(chemin,point1, point);
        Vector3[] sommets = CréerRoute.sommets;
        GameObject checkpoint;
        for (int i = 0; i < chemin.Count -2; i++)
        {

            checkpoint = Instantiate(obj, Vector3.Lerp(sommets[compteur++], sommets[compteur++], 0.5f),
                obj.transform.rotation);
            checkpoint.GetComponentInChildren<GénérateurCheckPoints>().FaireMesh(i* 2, chemin, sommets);
          

        }
        new GénérateurObstacles(chemin, obstalce1, obstacle2, sommets);

        new GénérateurCoins().GénérerCoins(15, chemin, coin);
        autos = new GestionnairePlayer(auto, moto, camion, 1).Joueurs; //à changer
        Debug.Log(Chemin.Count);
        créateur.CréerDébutPartie(autos, arc, ligneArrivée, chemin[chemin.Count - 1], chemin);
        mainPlayer1 = créateur.MainPlayer1;
        mainPlayer2 = créateur.MainPlayer2;
        //mainPlayer1 =Instantiate(auto, new Vector3(75, 0, 75), auto.transform.rotation);


   }

   private void LateUpdate()
   {
       //Ce code vient de :https://github.com/bhavik66/Unity3D-Ranking-System/tree/master/Assets/RankingSystem/Scripts
       // Calculate the current rotation angles
       target = mainPlayer1.transform;
       float wantedRotationAngle = target.eulerAngles.y;
       float wantedHeight = target.position.y + 30;

       float currentRotationAngle = cam1.transform.eulerAngles.y;
       float currentHeight = cam1.transform.position.y;

       // Damp the rotation around the y-axis
       currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, 3f * Time.deltaTime);

       // Damp the height
       currentHeight = Mathf.Lerp(currentHeight, wantedHeight, 2f * Time.deltaTime);

       // Convert the angle into a rotation
       var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

       // Set the position of the camera on the x-z plane to:
       // distance meters behind the target
       cam1.transform.position = target.position;
       cam1.transform.position -= currentRotation * Vector3.forward * 30;

       cam1.transform.rotation = Quaternion.Slerp(cam1.transform.rotation, currentRotation, 3f * Time.deltaTime);

       // Set the height of the camera
       cam1.transform.position = new Vector3(cam1.transform.position.x, currentHeight, cam1.transform.position.z);

       // Always look at the target
       cam1.transform.LookAt(target);
       textCoin.text = mainPlayer1.GetComponent<Player>().Argent.ToString();
       
       
       
       
       /*desiredPos = mainPlayer1.transform.position + offSet; 
       //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, (15f * Time.smoothDeltaTime) );
       cam1.transform.position = desiredPos;
       cam1.transform.rotation = mainPlayer1.transform.rotation;
       var cameraRotation = mainPlayer1.transform.rotation;
       cameraRotation.x = 0;
       cameraRotation.z = 0;
       cam1.transform.rotation = cameraRotation;
       cam1.transform.LookAt(mainPlayer1.transform);
       /*if (mainPlayer2 != null) //faire mieux que ça
       {
           desiredPos2 = mainPlayer2.transform.position + offSet;
           cam1.transform.position = desiredPos;
           cam1.transform.LookAt(mainPlayer1.transform);
       }*/
//       if (GetComponent<GestionnaireCollision>().CompteurTour == 3) // y a t'il y autre façon de faire ça
       {
           
       }
   }
   
}
