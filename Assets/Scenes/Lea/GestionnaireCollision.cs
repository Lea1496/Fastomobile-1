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
    [SerializeField] GameObject bonus;
    public Vector3[] points;
    
    //public List<PlayerData> players;
    private GénérateurObjets générateur;
    private Player joueur;
    private int compteurJoueursPassés;
    private DataCoin data;
   

    private void Start()
    {
        générateur = new GénérateurObjets();
        data = new DataCoin();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint point = collision.GetContact(0);
        
        if (point.otherCollider.gameObject.layer == coucheCollisionObstacle) //Obstacle
        {
            point.thisCollider.GetComponentInParent<Player>().EnleverVie(5); //changer cbm de vie
        }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == coucheCollisionCoin)
        {
            gameObject.GetComponent<Player>().AjouterArgent(1);
            Destroy(other.gameObject);
            générateur.GénérerCoins(1, points, coin);
        }
        if (other.gameObject.layer == coucheCollisionBonus) //Bonus
        {
            Destroy(other.gameObject);
            générateur.GénérerBonus(1, points, bonus);
        }

    }
}
