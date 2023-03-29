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
    private List<int> indices = new List<int>();
   
    
    public void GénérerCoins(int nbCoins, Vector3[] sommets, GameObject coin)
    {
        int indice;
        int[] neg = new int[] { -1, 1 };
        int cotéX;
        int décalageX;
        int décalageZ;
        int cotéZ;
        Vector3 point;
        Debug.Log(sommets.Length);
        int décalage;
        
        Vector3 pointA;
        Vector3 pointB;
        Vector3 instanciatePosition;
        for (int i = 0; i < nbCoins; i++)
        {
            do
            {
                indice = gen.Next(3, sommets.Length - 3);
            } while (indices.Contains(indice));

            indices.Add(indice);
            /* point = points[indice];
             
             cotéX = neg[gen.Next(0, 2)];
             décalageX = gen.Next(1, 45);
             décalageZ = gen.Next(1, 45);
             
             cotéZ = neg[gen.Next(0, 2)];*/
            décalage = gen.Next(20, 80);
            if (indice % 2 == 0)
            {
                pointA = sommets[indice];
                pointB = sommets[indice + 1];
            }
            else
            {
                pointA = sommets[indice + 1];
                pointB = sommets[indice + 2];
            }
            
            instanciatePosition = Vector3.Lerp(pointA, pointB, décalage / 100f);
            
            GameObject.Instantiate(coin,
                new Vector3(instanciatePosition.x, instanciatePosition.y + 5, instanciatePosition.z),
                coin.transform.rotation);
        }
    }
        
}
