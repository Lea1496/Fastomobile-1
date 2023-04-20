using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;

[RequireComponent(typeof(Player))]


public class GestionnaireCollision : MonoBehaviour
{
    [SerializeField] private int coucheCollisionObstacle;
    [SerializeField] private int coucheCollisionCoin;
    [SerializeField] private int coucheCollisionBonus;

    [SerializeField] GameObject coin;
    [SerializeField] GameObject bonus;
    [SerializeField] private Text textBonus1;
    [SerializeField] private Text textBonus2;
    public Vector3[] points;

    private Random gen = new Random();
    private GénérateurObjets générateur;
    private Player joueur;
    private int compteurJoueursPassés;
    private string bonusVie = "+ 10 HP!";
    private string bonusVitesse = "Boost";

    private void Start()
    {
        générateur = new GénérateurObjets();
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
        int ind;
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
            ind = gen.Next(0, 2);
            if (ind == 0)
            {
                gameObject.GetComponent<Player>().AjouterVie(10);
                textBonus1.text = bonusVie;
                StartCoroutine(AfficherBonus(textBonus1));
                StopCoroutine(AfficherBonus(textBonus1));
            }
            else
            {
                gameObject.GetComponent<GestionnaireTouches>().ApplyAcceleration(1f);
                textBonus2.text = bonusVitesse;
                StartCoroutine(AfficherBonus(textBonus2));
                StopCoroutine(AfficherBonus(textBonus2));
            }

            
        }

    }
    private IEnumerator AfficherBonus(Text textB)
    {
        yield return new WaitForSeconds(1f);
        textB.text = "";
    }
}
