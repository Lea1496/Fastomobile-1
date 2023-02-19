using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Newtonsoft.Json.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;



public class GestionnaireJeux : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int largeur;
    [SerializeField] private GameObject terrain;
    [SerializeField] private GameObject point;
    [SerializeField] private GameObject point1;
    [SerializeField] public GameObject obstalce1; // à changer
    [SerializeField] private GameObject obstacle2;
    private List<Vector3> chemin;
    public List<Vector3> Chemin
    {
        get => chemin;
    }
    
    // les deux prochaine fonctions viennent de
    // https://stackoverflow.com/questions/4501838/terminate-a-thread-after-an-interval-if-not-returned
    private void WorkThreadFunction()
    {
        try
        {
            chemin = new CréateurChemin3D(largeur).ListePos;
        }
        catch 
        {
            
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
       
       Refaire();
        //new CréateurTerrain(largeur, terrain);
        for (int i = 0; i < chemin.Count; i++)
        {
            Instantiate(point, chemin[i], point.transform.rotation);
        }
        chemin = new ScriptBézier(chemin).PointsSpline;
        
        //new GénérateurObstacles(chemin, obstalce1, obstacle2);
        Debug.Log("YAYY");
        Debug.Log(chemin.Count);
        for (int i = 0; i < chemin.Count; i++)
        {
            Debug.Log(chemin[i]);
            Instantiate(point1, chemin[i], point1.transform.rotation);
        }
    }

   
}
