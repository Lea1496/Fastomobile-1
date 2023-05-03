// Maxime Fortier

using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Décor : MonoBehaviour
{
    [SerializeField] private GameObject arbre1;
    [SerializeField] private GameObject arbre2;
    [SerializeField] private GameObject arbre3;
    [SerializeField] private GameObject arbre4;
    [SerializeField] private GameObject arbre5;

    [SerializeField] private int coucheCollisionArbre = 10;

    [SerializeField] Material matériauxDécor;

    private GameObject arbres;
    private Random gen = new Random();

    private List<Vector3> points = new List<Vector3>();

    private Vector3 position;

    private int nbArbresMax = 60,
                nbArbres = 0;

    private int posX = 0,
                posY = 0,
                posZ = 0;


    private void Start()
    {
        GetComponent<MeshRenderer>().material = matériauxDécor;
        while (nbArbres < nbArbresMax)
        {
            CréerDécor();
            arbres.transform.Translate(0,-1f,0);
            
            nbArbres++;
        }
    }

    public void CréerDécor()
    {
        do
        {
            posX = gen.Next(0, 10);
            posZ = gen.Next(0, 10);

            position = new Vector3(posX * 150, posY, posZ * 150);

        } while (points.Contains(position));

        points.Add(position);

        GameObject arbre = ChoisirArbre();
        arbres = Instantiate(arbre, position, arbre.transform.rotation);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == coucheCollisionArbre)
        {
            Destroy(other.gameObject);
            
            CréerDécor();
            arbres.transform.Translate(0,-1f,0);
            
        }
    }

    private GameObject ChoisirArbre()
    {
        int num = gen.Next(1, 6);
        string nom = num.ToString();
        string nomGame = "arbre" + nom;
        if (nomGame == "arbre2")
            return arbre2;
        if (nomGame == "arbre3")
            return arbre3;
        if (nomGame == "arbre4")
            return arbre4;
        if (nomGame == "arbre5")
            return arbre5;
        return arbre1;
    }
}
