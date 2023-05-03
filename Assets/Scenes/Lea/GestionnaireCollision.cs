
using System.Collections;

using UnityEngine;

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
    [SerializeField] private Text textBonusVie;
    [SerializeField] private Text textBonusVitesse;
    [SerializeField] private Text textBonusVie2;
    [SerializeField] private Text textBonusVitesse2;
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
            point.thisCollider.GetComponentInParent<Player>().EnleverVie(10); //changer cbm de vie
        }
        
        
    }

    private void OnTriggerEnter(Collider other)
    {
        int ind;
        joueur = gameObject.GetComponent<Player>();
        if (other.gameObject.layer == coucheCollisionCoin)
        {
            joueur.AjouterArgent(1);
            Destroy(other.gameObject);
            générateur.GénérerCoins(1, points, coin);
        }
        if (other.gameObject.layer == coucheCollisionBonus) //Bonus
        {
            Destroy(other.gameObject);
            générateur.GénérerBonus(1, points, bonus);
            ind = gen.Next(0, 2);
            if (joueur.IsMainPlayer2)
            {
                if (ind == 0)
                {
                    joueur.AjouterVie(10);
                    textBonusVie2.text = bonusVie;
                    StartCoroutine(AfficherBonus(textBonusVie2));
                    StopCoroutine(AfficherBonus(textBonusVie2));
                }
                else
                {
                    gameObject.GetComponent<GestionnaireTouches>().ApplyAcceleration(3f);
                    textBonusVitesse2.text = bonusVitesse;
                    StartCoroutine(AfficherBonus(textBonusVitesse2));
                    StopCoroutine(AfficherBonus(textBonusVitesse2));
                }
            }
            else
            {
                if (ind == 0)
                {
                    gameObject.GetComponent<Player>().AjouterVie(10);
                    textBonusVie.text = bonusVie;
                    StartCoroutine(AfficherBonus(textBonusVie));
                    StopCoroutine(AfficherBonus(textBonusVie));
                }
                else
                {
                    gameObject.GetComponent<GestionnaireTouches>().ApplyAcceleration(3f);
                    textBonusVitesse.text = bonusVitesse;
                    StartCoroutine(AfficherBonus(textBonusVitesse));
                    StopCoroutine(AfficherBonus(textBonusVitesse));
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
