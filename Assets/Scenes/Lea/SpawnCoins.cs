using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
public class SpawnCoins : MonoBehaviour
{
    private int maxCoins = 15;

    private Random gen = new Random();

    private GameObject coin;

    private List<Vector3> points;
    private List<int> indices;
    public SpawnCoins(List<Vector3> piste, GameObject c) // il va prendre la liste de vecteur bézier mais pas les verticies
    {
        coin = c;
        points = piste;
        indices = new List<int>();
        
        
    }
    
    public void GénérerCoins(int nbCoins)
    {
        int indice;
        Vector3 point;
        for (int i = 0; i < maxCoins; i++)
        {
            do
            {
                indice = gen.Next(5, points.Count);
            } while (indices.Contains(indice) && indices.Count != points.Count);

            indices.Add(indice);

            point = points[indice];
            
            
            int xOuY = gen.Next(0, 2);
            int[] neg = new int[] { -1, 1 };
            int coté = neg[gen.Next(0, 2)];
            int décalage = gen.Next(1, 45);
            
            if (xOuY == 1)
            {
                Instantiate(coin, new Vector3(point.x + coté *décalage, point.y + 5, point.z),coin.transform.rotation);
            }
            else
            {
                Instantiate(coin, new Vector3(point.x, point.y + 5, point.z + coté *décalage),coin.transform.rotation);
            }
            
        }
    }
        
}
