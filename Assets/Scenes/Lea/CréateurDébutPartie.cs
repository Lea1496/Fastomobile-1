using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CréateurDébutPartie : MonoBehaviour
{
    private List<Behaviourauto> lesAutos;
    [SerializeField] private GameObject arc;
    private int compteurAutos = 0;
    private Vector3 posPremier = new Vector3(335, 0, -50);
    public CréateurDébutPartie(List<Behaviourauto> autos) //behavior auto pour le moment mais surement à changer
    {
        lesAutos = autos;
        Instantiate(arc, new Vector3(300, 0, -61.5f), arc.transform.rotation);
        InstancierAutos();
    }

    private void InstancierAutos()
    {
        for (int i = 0; i < lesAutos.Count / 3; i++)
        {
            for (int j = 0; j < lesAutos.Count / 4; j++)
            {
                Instantiate(lesAutos[compteurAutos],
                    new Vector3(posPremier.x - 35 * j - 4 * i, 0, posPremier.z + 32 * i + 6 * j),
                    lesAutos[compteurAutos++].transform.rotation);
            }
        }
    }
    
}
