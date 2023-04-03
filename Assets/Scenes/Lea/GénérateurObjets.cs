using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
public class GénérateurObjets
{
    private int maxCoins = 15;
    private int maxBonus = 10;
    private int maxObstacles = 15;
    private Random gen = new Random();

    private GameObject coin;
    private GameObject bonus;
    private List<int> indices = new List<int>();
    
    private GameObject obstacle1;
    private GameObject obstacle2;
    private Vector3[] sommets;

   
    public void GénérerObjets(GameObject o1, GameObject o2, GameObject c, GameObject b, Vector3[] sommet) // il va prendre la liste de vecteur bézier mais pas les verticies
    {
        sommets = sommet;
        obstacle1 = o1;
        obstacle2 = o2;
        coin = c;
        bonus = b;
        indices = new List<int>();
        GénérerCoins(maxCoins, sommets, coin);
        GénérerObstacles();
        GénérerBonus(maxBonus, sommets, bonus);
    }
    public void GénérerCoins(int nbCoins, Vector3[] sommets, GameObject coin)
    {
        int indice;
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

    
    private void GénérerObstacles()
    {
        int indice;
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
                 GameObject.Instantiate(obstacle1, new Vector3(instanciatePosition.x, instanciatePosition.y , instanciatePosition.z),
                     Quaternion.LookRotation( Vector3.Lerp(pointA, pointB, décalage/100f )));
                 //GameObject.Instantiate(obstacle1, new Vector3(point.x + cotéX * décalageX, point.y + 5, point.z + cotéZ * décalageZ),obstacle1.transform.rotation);
             }
             else
             {
                 GameObject.Instantiate(obstacle2, new Vector3(instanciatePosition.x, instanciatePosition.y + 5 , instanciatePosition.z),
                     Quaternion.LookRotation( Vector3.Lerp(pointA, pointB, décalage/100f )));
                 //GameObject.Instantiate(obstacle2, new Vector3(point.x + cotéX * décalageX, point.y + 5, point.z + cotéZ * décalageZ),obstacle1.transform.rotation);
             }
        }
    }
 
    public void GénérerBonus(int nbBonus, Vector3[] sommets, GameObject bonus)
    {
        int indice;
        int décalage;
        
        Vector3 pointA;
        Vector3 pointB;
        Vector3 instanciatePosition;
        for (int i = 0; i < nbBonus; i++)
        {
            do
            {
                indice = gen.Next(3, sommets.Length -3);
            } while (indices.Contains(indice) && indices.Count < sommets.Length);

            indices.Add(indice);
          
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
            
            GameObject.Instantiate(bonus, new Vector3(instanciatePosition.x, instanciatePosition.y, instanciatePosition.z),
                bonus.transform.rotation);
        }
        
    }
}
