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
    Material matériauxBordImp;

    private void Start()
    {
        sommet = GetComponentInParent<ScriptSpline>().sommets;
        maillage = new Mesh();
        MeshCollider meshc = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
        maillage = AjouterBordure();
        GetComponent<MeshFilter>().mesh = maillage;
        meshc.sharedMesh = maillage;
        GetComponent<MeshRenderer>().material = matériauxBordImp;
    }

    private Mesh AjouterBordure()
    {
        static bool EstPair(int nb) { return nb % 2 == 0; }
        Vector2[] uvsBordureImpair = new Vector2[sommet.Length + 1];
        Vector3[] pointsBordureImpair = new Vector3[sommet.Length + 1];
        int nbTrianglesImpair = sommet.Length;
        int[] triangleImpair = new int[nbTrianglesImpair * 3];
        int indexSomImpair = 0;
        int indexTriImpair = 0;

        for (int i = 0; i < sommet.Length; i++)
        {
            if (!EstPair(i))
            {
                pointsBordureImpair[i] = new Vector3(sommet[i].x, 0, sommet[i].z);
                pointsBordureImpair[i + 1] = new Vector3(sommet[i].x, sommet[i].y + 5f, sommet[i].z);

                float completePercent = i / (float)(sommet.Length - 1);
                uvsBordureImpair[indexSomImpair] = new Vector2(0, completePercent);
                uvsBordureImpair[indexSomImpair + 1] = new Vector2(1, completePercent);

                
                if (i < sommet.Length - 1)
                {
                    triangleImpair[indexTriImpair] = indexSomImpair;
                    triangleImpair[indexTriImpair + 1] = (indexSomImpair + 2) % pointsBordureImpair.Length;
                    triangleImpair[indexTriImpair + 2] = indexSomImpair + 1;

                    triangleImpair[indexTriImpair + 3] = (indexSomImpair + 1);
                    triangleImpair[indexTriImpair + 4] = (indexSomImpair + 2) % pointsBordureImpair.Length;
                    triangleImpair[indexTriImpair + 5] = (indexSomImpair + 3) % pointsBordureImpair.Length;
                }

                indexSomImpair += 2;
                indexTriImpair += 6;
            }
        }
        Mesh mesh = new Mesh();
        mesh.vertices = pointsBordureImpair;
        mesh.triangles = triangleImpair;
        mesh.uv = uvsBordureImpair;
        return mesh;
    }
}
