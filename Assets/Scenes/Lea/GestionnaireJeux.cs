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
    [SerializeField] private GameObject coin;
    
    private ScriptSpline CréerRoute;
    
    private List<Vector3> chemin;
    private GameObject mainPlayer;
    private List<Player> autos;
    Vector3 offSet = new Vector3(-30, 30, 0);
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
        if (chemin == null)
        {
            Refaire();
        }
        chemin = new ScriptBézier(chemin).PointsSpline;
        CréerRoute.FaireMesh(point1, point);
        
        
        new GénérateurObstacles(chemin, obstalce1, obstacle2);
        GetComponent<GénérateurCoins>().GénérerCoins(15);
        autos = new GestionnairePlayer(auto, moto, camion, 1).Joueurs; //à changer
        mainPlayer = new CréateurDébutPartie(autos, arc, ligneArrivée, chemin[chemin.Count - 7]).MainPlayer1;
   }

   private void Update()
   {
       Vector3 desiredPosition = mainPlayer.transform.position + offSet; 
       //Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, (15f * Time.smoothDeltaTime) );
       cam1.transform.position = desiredPosition;
       cam1.transform.LookAt(mainPlayer.transform);
//       if (GetComponent<GestionnaireCollision>().CompteurTour == 3) // y a t'il y autre façon de faire ça
       {
           
       }
   }
   
}
