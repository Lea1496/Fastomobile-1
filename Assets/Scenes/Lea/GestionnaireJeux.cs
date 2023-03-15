using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;


[RequireComponent(typeof(ScriptSpline))]
[RequireComponent(typeof(GénérateurCoins))]
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
    
    private ScriptSpline CréerRoute;
    
    private List<Vector3> chemin;
    private GameObject mainPlayer1;
    private GameObject mainPlayer2;
    private List<Player> autos;
    Vector3 offSet = new Vector3(-30, 30, 30);
    private CréateurDébutPartie créateur;
    private Vector3 desiredPos;
    private Vector3 desiredPos2;
    private GameObject ligne;
    private int compteur = 0;
    public List<Vector3> Chemin
    {
        get => chemin;
    }
    public GameObject Coin
    {
        get => coin;
    }
    public List<Player> Autos
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
       CréerRoute = GetComponent<ScriptSpline>();
       Refaire();
        new CréateurTerrain(largeur, terrain);
       
        chemin = new ScriptBézier(chemin).PointsSpline;
        CréerRoute.FaireMesh(point1, point);
        Vector3[] sommets = CréerRoute.sommets;
        for (int i = 0; i < chemin.Count; i++)
        {
           //Instantiate(obj,  Vector3.Lerp(sommets[compteur++], sommets[compteur++], 0.5f), obj.transform.rotation).GetComponentInChildren<GénérateurCheckPoints>().FaireMesh(i);
        }
        new GénérateurObstacles(chemin, obstalce1, obstacle2, sommets);
        Debug.Log(sommets.Length);
        
        GetComponent<GénérateurCoins>().GénérerCoins(15);
        autos = new GestionnairePlayer(auto, moto, camion, 1).Joueurs; //à changer
        créateur = new CréateurDébutPartie(autos, arc, ligneArrivée, chemin[chemin.Count - 1]);
        mainPlayer1 = créateur.MainPlayer1;
        mainPlayer2 = créateur.MainPlayer2;
        
        

   }

   private void Update()
   {
       desiredPos = mainPlayer1.transform.position + offSet; 
       //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, (15f * Time.smoothDeltaTime) );
       cam1.transform.position = desiredPos;
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
