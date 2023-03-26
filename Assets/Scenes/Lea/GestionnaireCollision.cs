using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Player))]


public class GestionnaireCollision : MonoBehaviour
{
    [SerializeField] private int coucheCollisionObstacle;
    [SerializeField] private int coucheCollisionCoin;
    [SerializeField] private int coucheCollisionBonus;

    [SerializeField] GameObject coin;

    public List<Vector3> points;

    public bool isMainPlayer1;

    public bool isMainPlayer2 = false;
    //public List<PlayerData> players;
    private GénérateurCoins générateurC;
    private Player joueur;
    private int compteurJoueursPassés;
    private DataCoin data;
   

    private void Start()
    {
        générateurC = new GénérateurCoins();
        data = new DataCoin();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint point = collision.GetContact(0);
        
        if (point.otherCollider.gameObject.layer == coucheCollisionObstacle) //Obstacle
        {
            Debug.Log(point.thisCollider.gameObject.layer);
            point.thisCollider.GetComponentInParent<Player>().EnleverVie(5); //changer cbm de vie
        }
        if (point.otherCollider.gameObject.layer == coucheCollisionCoin) //Argent
        {
            gameObject.GetComponent<Player>().AjouterArgent(1);
            if (isMainPlayer1)
            {
                data.AjouterCoin("InfoPlayer1.txt", 1); //EST-CE QUE JE DEVRAIS À PLACE JUSTE L'AJOUTER QUAND JEU FINIT
               
            }
            else
            {
                if (isMainPlayer2)
                {
                    data.AjouterCoin("InfoPlayer2.txt", 1);
                }
            }
            
            Destroy(point.otherCollider.gameObject);
            générateurC.GénérerCoins(1, points, coin);
            
        }
        if (point.otherCollider.gameObject.layer == coucheCollisionBonus) //Bonus
        {
            
        }
        
    }
    
}
