// Maxime Fortier
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class BordurePairGrande : MonoBehaviour
{
    private Mesh maillage;
    private Vector3[] sommet;

    [SerializeField]
    Material matériauxBord;

    private void Start()
    {
        sommet = GetComponentInParent<ScriptSpline>().sommets;
        maillage = new Mesh();
        MeshCollider meshc = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
        maillage = AjouterBordure();
        GetComponent<MeshFilter>().mesh = maillage;
        meshc.sharedMesh = maillage;
//        GetComponent<MeshRenderer>().material = matériauxBord;
    }

    private Mesh AjouterBordure()
    {
        static bool EstPair(int nb) { return nb % 2 == 0; }
        Vector2[] uvsBordurePair = new Vector2[sommet.Length];
        Vector3[] pointsBordurePair = new Vector3[sommet.Length];
        int nbTrianglesPair = sommet.Length;
        int[] trianglePair = new int[nbTrianglesPair * 6];
        int indexSomPair = 0;
        int indexTriPair = 0;
        
        for (int i = 0; i < sommet.Length; i++)
        {
            if (EstPair(i))
            {
                pointsBordurePair[i] = new Vector3(sommet[i].x, 0, sommet[i].z);
                pointsBordurePair[i + 1] = new Vector3(sommet[i].x, sommet[i].y + 30f, sommet[i].z);

                float completePercent = i / (float)(sommet.Length - 1);
                uvsBordurePair[indexSomPair] = new Vector2(0, completePercent);
                uvsBordurePair[indexSomPair + 1] = new Vector2(1, completePercent);
                

                if (i < sommet.Length - 1)
                {
                    trianglePair[indexTriPair] = indexSomPair;
                    trianglePair[indexTriPair + 1] = (indexSomPair + 2) % pointsBordurePair.Length;
                    trianglePair[indexTriPair + 2] = indexSomPair + 1;

                    trianglePair[indexTriPair + 3] = (indexSomPair + 1);
                    trianglePair[indexTriPair + 4] = (indexSomPair + 2) % pointsBordurePair.Length;
                    trianglePair[indexTriPair + 5] = (indexSomPair + 3) % pointsBordurePair.Length;
                    
                    trianglePair[indexTriPair + 8] = indexSomPair;
                    trianglePair[indexTriPair + 7] = (indexSomPair + 2) % pointsBordurePair.Length;
                    trianglePair[indexTriPair + 6] = indexSomPair + 1;

                    trianglePair[indexTriPair + 11] = (indexSomPair + 1);
                    trianglePair[indexTriPair + 10] = (indexSomPair + 2) % pointsBordurePair.Length;
                    trianglePair[indexTriPair + 9] = (indexSomPair + 3) % pointsBordurePair.Length;
                }
               
                indexSomPair += 2;
                indexTriPair += 12;
            }
        }
        Mesh mesh = new Mesh();
        mesh.vertices = pointsBordurePair;
        mesh.triangles = trianglePair;
        mesh.uv = uvsBordurePair;
        return mesh;
    }
}
