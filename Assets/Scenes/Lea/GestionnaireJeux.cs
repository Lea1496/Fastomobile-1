using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionnaireJeux : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int largeur;
    [SerializeField] private GameObject terrain;
   void Awake()
    {
        new CréateurTerrain(largeur, terrain);
        Graph graph = new CréateurGraphCarte(largeur).CréerGraph();
        List<Vector3> chemin = new CréateurCheminComplet(largeur, graph).CheminComplet;
        Debug.Log("YAYY");
        Debug.Log(chemin.Count);
        for (int i = 0; i < chemin.Count; i++)
        {
            Debug.Log(chemin[i]);
        }
    }

   
}
