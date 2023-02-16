using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

public class GénérateurObstacles : MonoBehaviour
{
    private int maxObstacles = 15;

    private Random gen = new Random();

    [SerializeField] private GameObject obstacle1;
    [SerializeField] private GameObject obstacle2;
    private List<Vector3> points;
    private List<int> indices;
    public GénérateurObstacles(List<Vector3> piste)
    {
        points = piste;
        indices = new List<int>(piste.Count);
        for (int i = 0; i < indices.Count; i++)
        {
            indices[i] = i;
        }
        
    }

    private void GénérerObstacles()
    {
        int obtstacle = 0;

        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
