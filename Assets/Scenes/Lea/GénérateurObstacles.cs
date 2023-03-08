using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

public class GénérateurObstacles : MonoBehaviour
{
    private int maxObstacles = 15;

    private Random gen = new Random();

    private GameObject obstacle1;
    private GameObject obstacle2;
    private List<Vector3> points;
    private List<int> indices;
    public GénérateurObstacles(List<Vector3> piste, GameObject o1, GameObject o2) // il va prendre la liste de vecteur bézier mais pas les verticies
    {
        obstacle1 = o1;
        obstacle2 = o2;
        points = piste;
        indices = new List<int>();
        
        GénérerObstacles(); 
        
        
    }
    
    private void GénérerObstacles()
    {
        int indice;
        Vector3 point;
        int[] neg = new int[] { -1, 1 };
        int cotéX;
        int décalageX;
        int décalageZ;
        int cotéZ;
        for (int i = 0; i < maxObstacles; i++)
        {
            do
            {
                indice = gen.Next(0, points.Count);
            } while (indices.Contains(indice) && indices.Count != points.Count);

            indices.Add(indice);
            
            point = points[indice];
            cotéX = neg[gen.Next(0, 2)];
            décalageX = gen.Next(1, 45);
            décalageZ = gen.Next(1, 45);
            cotéZ = neg[gen.Next(0, 2)];
            if (indice % 2 == 0)
            {
                Instantiate(obstacle1, new Vector3(point.x + cotéX * décalageX, point.y + 5, point.z + cotéZ * décalageZ),obstacle1.transform.rotation);
            }
            else
            {
                Instantiate(obstacle2, new Vector3(point.x + cotéX * décalageX, point.y + 5, point.z + cotéZ * décalageZ),obstacle2.transform.rotation);
            }
        }
        
    }
    
}
