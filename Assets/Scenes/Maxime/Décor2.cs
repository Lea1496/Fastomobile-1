// Maxime Fortier
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Décor2 : MonoBehaviour
{
    [SerializeField] private GameObject palm;
    [SerializeField] private GameObject tree;
    [SerializeField] private GameObject cactus1;
    [SerializeField] private GameObject cactus2;
    [SerializeField] private GameObject cactus3;
    [SerializeField] private GameObject rock;
    [SerializeField] private GameObject bone;
    


    [SerializeField] private int coucheCollisionDésert = 12;

    [SerializeField] Material matériauxDécor2;

    private Random gen = new Random();

    private GameObject objets;
    private List<Vector3> points = new List<Vector3>();

    private Vector3 position;

    private int nbObjetMax = 60,
                nbObjet = 0;

    private int posX = 0,
                posY = 0,
                posZ = 0;

    void Start()
    {
        GetComponent<MeshRenderer>().material = matériauxDécor2;
        while (nbObjet < nbObjetMax)
        {
            CréerDécor();
            objets.transform.Translate(0,-1f,0);
            
            nbObjet++;
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

        GameObject objet = ChoisirObjet();
        
        objets = Instantiate(objet, position, objet.transform.rotation);

    }

    private GameObject ChoisirObjet()
    {
        int num = gen.Next(1, 8);
        string nom = num.ToString();
        string nomGame = "objet" + nom;
        if (nomGame == "objet2")
            return palm;
        if (nomGame == "objet3")
            return tree;
        if (nomGame == "objet4")
            return cactus1;
        if (nomGame == "objet5")
            return cactus2;
        if (nomGame == "objet6")
            return cactus3;
        if (nomGame == "objet7")
            return bone;
        return rock;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == coucheCollisionDésert)
        {
            Destroy(other.gameObject);
            CréerDécor();
            objets.transform.Translate(0,-1f,0);
     
        }
    }
}
