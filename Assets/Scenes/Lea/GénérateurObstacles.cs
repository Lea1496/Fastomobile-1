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
        for (int i = 0; i < maxObstacles; i++)
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
            if (indice == 0)
            {
                if (xOuY == 1)
                {
                    Instantiate(obstacle1, new Vector3(point.x + coté *décalage, point.y, point.z),obstacle1.transform.rotation);
                }
                else
                {
                    Instantiate(obstacle1, new Vector3(point.x, point.y, point.z + coté *décalage),obstacle1.transform.rotation);
                }
            
            }
            else
            {
                if (xOuY == 1)
                {
                    Instantiate(obstacle2, new Vector3(point.x + coté *décalage, point.y, point.z),obstacle2.transform.rotation);
                }
                else
                {
                    Instantiate(obstacle2, new Vector3(point.x, point.y, point.z + coté *décalage),obstacle2.transform.rotation);
                }
            }
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
