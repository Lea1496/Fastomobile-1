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
        //List<Vector3> pointsSpline2 = GetComponentInParent<GestionnaireJeux>().Chemin;
        Debug.Log(sommet.Length);
        maillage = new Mesh();
        MeshCollider meshc = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
        maillage = AjouterBordure();
        GetComponent<MeshFilter>().mesh = maillage;
        meshc.sharedMesh = maillage;
        GetComponent<MeshRenderer>().material = matériauxBordImp;
    }

    private Mesh AjouterBordure()
    {
        //bordure impair
        //    Vector2[] uvsBordure = new Vector2[sommets.Length];
        //    int nbTrianglesImpair = sommets.Length;
        //    int[] triangleImpair = new int[nbTrianglesImpair * 3];
        //    static bool EstPair(int nb) { return nb % 2 == 0; }
        //    Vector3[] pointsBordureImpair = new Vector3[sommets.Length];
        //    int indexSom = 0;
        //    int indexTri = 0;

        //    // bordure impair
        //    for (int i = 0; i < sommets.Length; i++)
        //    {
        //        if (!EstPair(i))
        //        {
        //            pointsBordureImpair[i] = new Vector3(sommets[i].x, 0, sommets[i].z);
        //            pointsBordureImpair[i + 1] = new Vector3(sommets[i].x, sommets[i].y + 5f, sommets[i].z);

        //            float completePercent = i / (float)(sommets.Length - 1);
        //            uvsBordure[indexSom] = new Vector2(0, completePercent);
        //            uvsBordure[indexSom + 1] = new Vector2(1, completePercent);

        //            if (i < sommets.Length - 1)
        //            {
        //                triangleImpair[indexTri] = indexSom;
        //                triangleImpair[indexTri + 1] = (indexSom + 2) % pointsBordureImpair.Length;
        //                triangleImpair[indexTri + 2] = indexSom + 1;

        //                triangleImpair[indexTri + 3] = (indexSom + 1);
        //                triangleImpair[indexTri + 4] = (indexSom + 2) % pointsBordureImpair.Length;
        //                triangleImpair[indexTri + 5] = (indexSom + 3) % pointsBordureImpair.Length;
        //            }
        //            indexSom += 2;
        //            indexTri += 6;
        //        }
        //    }

        //    Mesh meshBordure = new Mesh();
        //    meshBordure.vertices = pointsBordureImpair;
        //    meshBordure.triangles = triangleImpair;
        //    meshBordure.uv = uvsBordure;
        //    return meshBordure;
        //sommet = new Vector3[pointsSpline2.Count * 2];
        int indexSomImpair = 0;
        Vector2[] uvsBordureImpair = new Vector2[sommet.Length];
        int nbTrianglesImpair = sommet.Length;
        int[] triangleImpair = new int[nbTrianglesImpair * 3];
        int indexTriImpair = 0;
        static bool EstPair(int nb) { return nb % 2 == 0; }
        Vector3[] pointsBordureImpair = new Vector3[sommet.Length + 1];
        // int indexSom = 0;
        //Vector3[] pointsBordureImpair = new Vector3[sommet.Length];

        /*for (int i = 0; i < pointsSpline2.Count; i++)
        {
            Vector3 directionAvant2 = new Vector3();
            directionAvant2 = Vector3.zero;
            if (i < pointsSpline2.Count - 1)
            {
                directionAvant2 += pointsSpline2[(i + 1) % pointsSpline2.Count] - pointsSpline2[i];
            }
            if (i > 0)
            {
                directionAvant2 += pointsSpline2[i] - pointsSpline2[(i - 1 + pointsSpline2.Count) % pointsSpline2.Count];
            }
            directionAvant2.Normalize();
            Vector3 gauche = new Vector3(-directionAvant2.z, 0, directionAvant2.x);

            // calculer 2 sommets
            sommet[indexSomPair] = pointsSpline2[i] + gauche * 70f;
            sommet[indexSomPair + 1] = pointsSpline2[i] - gauche * 70f;
            indexSom += 2;
        }*/
        Debug.Log(sommet.Length);

        // bordure impair
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
