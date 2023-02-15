using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CréateurGraphCarte : MonoBehaviour
{
    private int largeur;
   
    public CréateurGraphCarte(int Largeur)
    {
        largeur = Largeur;
    }
  
    public Graph CréerGraph()
    {
        int A = 0;
        int B = 0;
        Graph graph = new Graph(2 * largeur * largeur );
        
        for (int i = 0; i < largeur; i++)
        {
            if (i != largeur - 1)
            {
                for (int j = 0; j < largeur; j++)
                {
                    A = j + largeur * i;
                    B = A + largeur * (i + 1); 
                    graph.AddDirectEdge(0, 8);
                }
            }
            

            for (int j = 0; j < largeur - 1; j++)
            {
                
                A = j + largeur * i;
                B = A + 1;
                graph.AddDirectEdge(A, B);
            } 
            
            
            
            
        }

        Debug.Log(graph.CreateAdjacencyListsDisplay());

        return graph;
    }
}
