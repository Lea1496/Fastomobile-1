using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class CréateurCheminComplet : MonoBehaviour
{
    private Graph graph;

    private int largeur;

  
    private int compteur = 0;
    private int nbIndices;
    private Random gen = new Random();

    private int[] positions;
    private int[] tableau1;
    private int[] tableau2;
    private int[] tableau3;
    private int[] tableau4;

    private List<Vector3> cheminComplet;
    private CréateurChemin2D chemin;

    public List<Vector3> CheminComplet
    {
        get => cheminComplet;
        private set => CheminComplet = cheminComplet;
    }


    public CréateurCheminComplet(int Largeur, Graph Graph)
    {
        largeur = Largeur;
        graph = Graph;
        nbIndices = largeur / 2 * (largeur / 2);
        tableau1 = new int[largeur / 2 * (largeur / 2 -1)];
        tableau2 = new int[nbIndices];
        tableau3 = new int[nbIndices];
        tableau4 = new int[nbIndices];
        CréerTableaux();
        positions = TransformerIndicesEnPostion();
        chemin = new CréateurChemin2D(largeur, graph, positions[0], positions[1], positions[2], positions[3]);
        cheminComplet = new List<Vector3>();
        cheminComplet.AddRange(chemin.ListePos);
        
        

    }

    
    private void CréerTableaux()
    {
        compteur = 0;
        for (int i = 0; i < largeur / 2; i++)
        {
            for (int j = 1; j < largeur / 2; j++)
            {
                tableau1[compteur++] = i * largeur  + j;
            }
        }

        compteur = 0;
        for (int i = 0; i < largeur / 2; i++)
        {
            for (int j = 0; j < largeur / 2 ; j++)
            {
                tableau2[compteur++] = largeur / 2 + i * largeur + j ;
            }
        }
        compteur = 0;
        for (int i = 0; i < largeur / 2; i++)
        {
            for (int j = 0; j < largeur / 2 ; j++)
            {
                tableau3[compteur++] = (largeur / 2 + i) * largeur + largeur / 2 + j;
            }
        } 
        compteur = 0;
        for (int i = 0; i < largeur / 2; i++)
        {
            for (int j = 0; j < largeur / 2; j++)
            {
                tableau4[compteur++] = (largeur / 2 + i) * largeur + j;
            }
        }
          
    }

    private List<Vector3> FormerChemin2D()
    {
        List<Vector3> lesChemins = new List<Vector3>();
        chemin = new CréateurChemin2D(largeur, graph, positions[0], positions[1], positions[2], positions[3]);
        lesChemins.AddRange(chemin.ListePos);
        return lesChemins;
    }

    private int[] GénérerIndices()
    {
        int[] ind = new int[4];

        for (int j = 0; j < ind.Length; j++)
        {
            if (j == 0)
            {
                ind[j]  = gen.Next(1, largeur / 2 * (largeur / 2 -1));
            }
            else
            {
                ind[j]  = gen.Next(0, nbIndices);
            }
           
        }

        return ind;
    }
    private int[] TransformerIndicesEnPostion()
    {
        int[] indices = GénérerIndices();
        int[] pos = new int[4];

        pos[0] = tableau1[indices[0]];
        pos[1] = tableau2[indices[1]];
        pos[2] = tableau3[indices[2]];
        pos[3] = tableau4[indices[3]];

        return pos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
