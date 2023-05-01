// Maxime Fortier
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class BordureImpair : MonoBehaviour
{
    private Mesh maillage;
    private Vector3[] sommet;

    [SerializeField]
    Material materiauxBordImp;

    private void Start()
    {
        sommet = GetComponentInParent<ScriptSpline>().sommets;
        maillage = new Mesh();
        MeshCollider meshc = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
        maillage = AjouterBordure();
        GetComponent<MeshFilter>().mesh = maillage;
        meshc.sharedMesh = maillage;
        GetComponent<MeshRenderer>().material = materiauxBordImp;
    }

    private Mesh AjouterBordure()
    {
        static bool EstPair(int nb) { return nb % 2 == 0; }
        Vector2[] uvsBordureImpair = new Vector2[sommet.Length];
        Vector3[] pointsBordureImpair = new Vector3[sommet.Length];
        int nbTrianglesImpair = sommet.Length;
        int[] triangleImpair = new int[nbTrianglesImpair * 6];
        int indexSomImpair = 0;
        int indexTriImpair = 0;
        int indice = 0;
       
        for (int i = 1; i < sommet.Length ; i++)
        {
            if (!EstPair(i))
            {
                pointsBordureImpair[indexSomImpair] = new Vector3(sommet[i].x, 0, sommet[i].z);
                pointsBordureImpair[indexSomImpair + 1] = new Vector3(sommet[i].x, sommet[i].y + 5f, sommet[i].z);

                float completePercent = indexSomImpair / (float)(sommet.Length - 1);
                uvsBordureImpair[indexSomImpair] = new Vector2(0, completePercent);
                uvsBordureImpair[indexSomImpair + 1] = new Vector2(1, completePercent);

                if (i < sommet.Length - 1)
                {
                    triangleImpair[indexTriImpair] = indexSomImpair + 1;
                    triangleImpair[indexTriImpair + 1] = (indexSomImpair + 2) % pointsBordureImpair.Length;
                    triangleImpair[indexTriImpair + 2] = indexSomImpair;

                    triangleImpair[indexTriImpair + 5] = (indexSomImpair + 1);
                    triangleImpair[indexTriImpair + 4] = (indexSomImpair + 2) % pointsBordureImpair.Length;
                    triangleImpair[indexTriImpair + 3] = (indexSomImpair + 3) % pointsBordureImpair.Length;
                    
                    triangleImpair[indexTriImpair + 8] = indexSomImpair + 1;
                    triangleImpair[indexTriImpair + 7] = (indexSomImpair + 2) % pointsBordureImpair.Length;
                    triangleImpair[indexTriImpair + 6] = indexSomImpair;

                    triangleImpair[indexTriImpair + 9] = (indexSomImpair + 1);
                    triangleImpair[indexTriImpair + 10] = (indexSomImpair + 2) % pointsBordureImpair.Length;
                    triangleImpair[indexTriImpair + 11] = (indexSomImpair + 3) % pointsBordureImpair.Length;
                }

                
                indexSomImpair += 2;
                indexTriImpair += 12;
                
            }
        }
        
        Mesh mesh = new Mesh();
        mesh.vertices = pointsBordureImpair;
        mesh.triangles = triangleImpair;
        mesh.uv = uvsBordureImpair;
        return mesh;
    }
}
