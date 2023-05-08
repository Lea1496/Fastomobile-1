
using System;
using System.Collections;

using UnityEngine;

using UnityEngine.UI;
using Random = System.Random;

[RequireComponent(typeof(Player))]


public class GestionnaireCollision : MonoBehaviour
{
    private const int CoucheCollisionObstacle = 9;
    private const int CoucheCollisionCoin = 7;
    private const int CoucheCollisionBonus = 11;
    private const int CoucheCollisionMur = 14;
    
    

    [SerializeField] GameObject coin;
    [SerializeField] GameObject bonus;
    [SerializeField] private Text textBonusVie;
    [SerializeField] private Text textBonusVitesse;
    [SerializeField] private Text textBonusVie2;
    [SerializeField] private Text textBonusVitesse2;
    public Vector3[] points;
    private Vector3 reculer = new Vector3(0, 0, -10);
    private Random gen = new Random();
    private GénérateurObjets générateur;
    private Player joueur;
    private int compteurJoueursPassés;
    private string bonusVie = "Bonus HP!";
    private string bonusVitesse = "Boost";
    private float temps;

    private void Update()
    {
        temps += Time.deltaTime;
    }

    private void Start()
    {
        générateur = new GénérateurObjets();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint point = collision.GetContact(0);
        
        if (point.otherCollider.gameObject.layer == CoucheCollisionObstacle) //Obstacle
        {
            joueur = point.thisCollider.GetComponentInParent<Player>();
            joueur.EnleverVie(10);
            if (joueur.Vie <= 0)
            {
                GameData.ListeJoueursMorts.Add(joueur.Nom);
            }
            //Empêche que le joueur reste pris dans un mur/obstacle
           // gameObject.transform.Translate(reculer, Space.Self);
        }
        else if (point.otherCollider.gameObject.layer == 14)
        {
           // gameObject.transform.Translate(reculer, Space.Self);
        }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        int ind;
        joueur = gameObject.GetComponent<Player>();
        if (temps > 0.5f)
        {
            if (other.gameObject.layer == CoucheCollisionCoin)
            {
                joueur.AjouterArgent(1);
                Destroy(other.gameObject);
                générateur.GénérerCoins(1, points, coin);
                temps = 0;
            }
            else if (other.gameObject.layer == CoucheCollisionBonus) //Bonus
            {
                temps = 0;
                Destroy(other.gameObject);
                générateur.GénérerBonus(1, points, bonus);
                ind = gen.Next(0, 2);
                if (joueur.IsMainPlayer2)
                {
                    if (ind == 0)
                    {
                        joueur.AjouterVie(5);
                        textBonusVie2.text = bonusVie;
                        StartCoroutine(AfficherBonus(textBonusVie2));
                        StopCoroutine(AfficherBonus(textBonusVie2));
                    }
                    else
                    {
                        gameObject.GetComponent<GestionnaireTouches>().ApplyAccelerationCustom(8f);
                        textBonusVitesse2.text = bonusVitesse;
                        StartCoroutine(AfficherBonus(textBonusVitesse2));
                        StopCoroutine(AfficherBonus(textBonusVitesse2));
                    }
                }
                else if(joueur.IsMainPlayer)
                {
                    if (ind == 0)
                    {
                        joueur.AjouterVie(15);
                        textBonusVie.text = bonusVie;
                        StartCoroutine(AfficherBonus(textBonusVie));
                        StopCoroutine(AfficherBonus(textBonusVie));
                    }
                    else
                    {
                        gameObject.GetComponent<GestionnaireTouches>().ApplyAccelerationCustom(8f);
                        textBonusVitesse.text = bonusVitesse;
                        StartCoroutine(AfficherBonus(textBonusVitesse));
                        StopCoroutine(AfficherBonus(textBonusVitesse));
                    }
                }
                
    
                
            }

            
        }
        

    }
    private IEnumerator AfficherBonus(Text textB)
    {
        yield return new WaitForSeconds(1f);
        textB.text = "";
    }
}
