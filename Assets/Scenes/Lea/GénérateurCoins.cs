using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
[RequireComponent(typeof(GestionnaireJeux))]
public class GénérateurCoins 
{
    private int maxCoins = 15;

    private Random gen = new Random();

    private GameObject coin;

    private List<Vector3> points;
    private List<int> indices = new List<int>();
   
    
    public void GénérerCoins(int nbCoins, List<Vector3> points, GameObject coin)
    {
        int indice;
        int[] neg = new int[] { -1, 1 };
        int cotéX;
        int décalageX;
        int décalageZ;
        int cotéZ;
        Vector3 point;
        for (int i = 0; i < nbCoins; i++)
        {
            do
            {
                indice = gen.Next(5, points.Count - 8);
            } while (indices.Contains(indice) && indices.Count != points.Count);

            indices.Add(indice);
            point = points[indice];
            
            cotéX = neg[gen.Next(0, 2)];
            décalageX = gen.Next(1, 45);
            décalageZ = gen.Next(1, 45);
            cotéZ = neg[gen.Next(0, 2)];
            
           
            GameObject.Instantiate(coin, new Vector3(point.x + cotéX * décalageX, point.y + 5, point.z + cotéZ * décalageZ),coin.transform.rotation);
            
            
        }
    }
        
}
