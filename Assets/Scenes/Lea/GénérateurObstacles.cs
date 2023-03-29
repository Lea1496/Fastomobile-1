using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

public class GénérateurObstacles
{
    private int maxObstacles = 15;

    private Random gen = new Random();

    private GameObject obstacle1;
    private GameObject obstacle2;
    private List<int> indices;
    private Vector3[] sommets;
    public GénérateurObstacles(GameObject o1, GameObject o2, Vector3[] sommet) // il va prendre la liste de vecteur bézier mais pas les verticies
    {
        sommets = sommet;
        obstacle1 = o1;
        obstacle2 = o2;
        indices = new List<int>();
        
        GénérerObstacles();
    }
    
    private void GénérerObstacles()
    {
        
        int indice;
        /*Vector3 point;
        int[] neg = new int[] { -1, 1 };
        int cotéX;
        int décalageX;
        int décalageZ;
        int cotéZ;*/
        int décalage;
        
        Vector3 pointA;
        Vector3 pointB;
        Vector3 instanciatePosition;
        for (int i = 0; i < maxObstacles; i++)
        {
            do
            {
                indice = gen.Next(3, sommets.Length -2);
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
            instanciatePosition = Vector3.Lerp(pointA, pointB, décalage/100f );
            
             if (indice % 2 == 0)
             {
                 GameObject.Instantiate(obstacle1, new Vector3(instanciatePosition.x, instanciatePosition.y + 5 , instanciatePosition.z),
                     obstacle1.transform.rotation);
                 //GameObject.Instantiate(obstacle1, new Vector3(point.x + cotéX * décalageX, point.y + 5, point.z + cotéZ * décalageZ),obstacle1.transform.rotation);
             }
             else
             {
                 GameObject.Instantiate(obstacle2, new Vector3(instanciatePosition.x, instanciatePosition.y + 5 , instanciatePosition.z),
                     obstacle1.transform.rotation);
                 //GameObject.Instantiate(obstacle2, new Vector3(point.x + cotéX * décalageX, point.y + 5, point.z + cotéZ * décalageZ),obstacle1.transform.rotation);
             }
        }
        
    }

    //public GameObject ropePrefab;
    //Vector3 pointA;
    //Vector3 pointB;
    //Vector3 instantiatePosition;
    //float lerpValue;
    //float distance;
    //int segmentsToCreate;

   /*
    https://answers.unity.com/questions/1379156/instantiate-objects-in-a-line-between-two-points.html
    private void InstantiateSegments(Vector3 pointA, Vector3 pointB, Vector3 instantiatePosition)
    {
        //Here we calculate how many segments will fit between the two points
        //segmentsToCreate = Mathf.RoundToInt(Vector3.Distance(pointA, pointB) / 0.5f);
        //As we'll be using vector3.lerp we want a value between 0 and 1, and the distance value is the value we have to add
        distance = 1 / segmentsToCreate;
        //for (int i = 0; i < segmentsToCreate; i++)
        {
            //We increase our lerpValue
            lerpValue += distance;
            //Get the position
            instantiatePosition = Vector3.Lerp(pointA, pointB, lerpValue);
            //Instantiate the object
            Instantiate(ropePrefab, instantiatePosition, transform.rotation);
        }

    }*/

    
    
    
}
