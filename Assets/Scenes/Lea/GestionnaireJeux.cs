using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Newtonsoft.Json.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

class Program
{
    static void Main(string[] args)
    {
  
       
        
        
    }

   
}

public class GestionnaireJeux : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int largeur;
    [SerializeField] private GameObject terrain;
    [SerializeField] private GameObject point;
    [SerializeField] private GameObject point1;
    private List<Vector3> chemin;
    private float time = 0;
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
        if (!thread.Join(new TimeSpan(0, 0, 1)) && chemin != null)
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
        //Graph graph = new CréateurGraphCarte(largeur).CréerGraph();
        
       
        for (int i = 0; i < chemin.Count; i++)
        {
            Instantiate(point, chemin[i], point.transform.rotation);
        }
        chemin = new ScriptBézier(chemin).PointsSpline;
        //new CréateurDébutPartie(List<Behaviourauto> autos);
        
        Debug.Log("YAYY");
        Debug.Log(chemin.Count);
        for (int i = 0; i < chemin.Count; i++)
        {
            Instantiate(point1, chemin[i], point1.transform.rotation);
        }
    }

   /*private void Update()
   {
       Stopwatch watch = new Stopwatch();
       watch.Start();
       while (watch.ElapsedMilliseconds < 5000 && Thread.CurrentThread.IsAlive)
           ;
             
       Thread.CurrentThread.Abort();
       Console.WriteLine("Unable to connect");
       time += Time.deltaTime;
       if (time > 5 )
       {
           throw new MarchePas();
       }

   }*/
}
