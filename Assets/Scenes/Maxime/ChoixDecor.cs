// Maxime Fortier

using UnityEngine;
using Random = System.Random;

public class ChoixDecor : MonoBehaviour
{
    [SerializeField] private GameObject terrain1;
    [SerializeField] private GameObject terrain2;
    private Random gen = new Random();

    private int decor;
    private void Awake()
    {
        decor = gen.Next(1, 4);
        if (decor == 1)
        {
            GetComponent<GestionnaireJeux>().terrain = terrain1;
        }
        else
        {
            GetComponent<GestionnaireJeux>().terrain = terrain2;
        }
    }

    void Start()
    {
        if (decor == 1)
        {
            GetComponent<Décor>().enabled = true;
        }
        else
        {
            GetComponent<Décor2>().enabled = true;
        }
    }
}
