using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using UnityEngine;

public class GestionnaireJeux : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int largeur;
    [SerializeField] private GameObject terrain;
    [SerializeField] private GameObject point;
    [SerializeField] private GameObject point1;
    private List<Vector3> chemin;
    public List<Vector3> Chemin
    {
        get => chemin;
    }
   void Awake()
    {
        //new CréateurTerrain(largeur, terrain);
        Graph graph = new CréateurGraphCarte(largeur).CréerGraph();
        chemin = new CréateurChemin3D(largeur, graph).ListePos;
       
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
    

   
}
