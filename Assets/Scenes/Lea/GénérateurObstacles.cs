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
    private List<Vector3> points;
    private List<int> indices;
    private Vector3[] sommets;
    public GénérateurObstacles(List<Vector3> piste, GameObject o1, GameObject o2, Vector3[] sommet) // il va prendre la liste de vecteur bézier mais pas les verticies
    {
        sommets = sommet;
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
                indice = gen.Next(5, points.Count -5);
            } while (indices.Contains(indice) && indices.Count != points.Count);

            indices.Add(indice);
            
            point = points[indice];
            Quaternion quat = CalculerQuaternion(indice);
            cotéX = neg[gen.Next(0, 2)];
            décalageX = gen.Next(1, 45);
            décalageZ = gen.Next(1, 45);
            cotéZ = neg[gen.Next(0, 2)];
            if (indice % 2 == 0)
            {
                GameObject.Instantiate(obstacle1, new Vector3(point.x + cotéX * décalageX, point.y + 5, point.z + cotéZ * décalageZ),obstacle1.transform.rotation);
            }
            else
            {
                GameObject.Instantiate(obstacle2, new Vector3(point.x + cotéX * décalageX, point.y + 5, point.z + cotéZ * décalageZ),obstacle1.transform.rotation);
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

    private Quaternion CalculerQuaternion(int indice)
    {
        Vector3 point1 = sommets[indice];
        Vector3 point2 = sommets[indice + 1];
        
        Vector3 vecteur = new Vector3(point2.x - point1.x, point2.y - point1.y, point2.z - point1.z);
        
        Vector3 pointZ = new Vector3(vecteur.x, vecteur.y, vecteur.z + 75);
        
        //float angle = Mathf.Acos(vecteur.x * pointZ.x + vecteur.y * pointZ.y + vecteur.z * pointZ.z);
        
        return EulerToQuaternion(new Vector3(0, 0,0 /*angle*/));


    }
    
    // Fonction de : https://forum.unity.com/threads/how-to-create-a-new-rotation-using-quaternion.708512/
    private Quaternion EulerToQuaternion(Vector3 p)
    {
        p.x *= Mathf.Deg2Rad;
        p.y *= Mathf.Deg2Rad;
        p.z *= Mathf.Deg2Rad;
        Quaternion q = new Quaternion();
        float cy = Mathf.Cos(p.z * 0.5f);
        float sy = Mathf.Sin(p.z * 0.5f);
        float cr = Mathf.Cos(p.y * 0.5f);
        float sr = Mathf.Sin(p.y * 0.5f);
        float cp = Mathf.Cos(p.x * 0.5f);
        float sp = Mathf.Sin(p.x * 0.5f);
        q.w = cy * cr * cp + sy * sr * sp;
        q.x = cy * cr * sp + sy * sr * cp;  
        q.y = cy * sr * cp - sy * cr * sp;
        q.z = sy * cr * cp - cy * sr * sp;
        return q;
    }
    
}
