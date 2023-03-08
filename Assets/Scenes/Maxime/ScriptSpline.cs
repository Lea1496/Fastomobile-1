// Maxime Fortier
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class ScriptSpline : MonoBehaviour
{
    private float largeurRoute = 70f;
    private List<Vector3> pointsSpline;
    public Vector3[] sommets;
    Vector3 gauche;
    Vector3 directionAvant;

    private Mesh maillage;

    [SerializeField]
    Material matériaux;

    public void FaireMesh()
    {
        maillage = new Mesh();
        MeshCollider meshc = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
        pointsSpline = GetComponent<GestionnaireJeux>().Chemin;
        maillage = CréerMeshRoute(pointsSpline);
        GetComponent<MeshFilter>().mesh = maillage;
        meshc.sharedMesh = maillage;
        GetComponent<MeshRenderer>().material = matériaux;
    }

    private Mesh CréerMeshRoute(List<Vector3> pointsSpline)
    {
        // le code CréerMeshRoute vient de https://www.youtube.com/watch?v=Q12sb-sOhdI

        sommets = new Vector3[pointsSpline.Count * 2];
        Vector2[] uvs = new Vector2[sommets.Length];
        int nbTriangles = 2 * pointsSpline.Count;
        int[] triangle = new int[nbTriangles * 3];
        int indexSom = 0;
        int indexTri = 0;

        for (int i = 0; i < pointsSpline.Count; i++)
        {
            directionAvant = Vector3.zero;
            if (i < pointsSpline.Count - 1)
            {
                directionAvant += pointsSpline[(i + 1) % pointsSpline.Count] - pointsSpline[i];
            }
            if (i > 0)
            {
                directionAvant += pointsSpline[i] - pointsSpline[(i - 1 + pointsSpline.Count) % pointsSpline.Count];
            }
            directionAvant.Normalize();
            gauche = new Vector3(-directionAvant.z, 0, directionAvant.x);

            sommets[indexSom] = pointsSpline[i] + gauche * largeurRoute;
            sommets[indexSom + 1] = pointsSpline[i] - gauche * largeurRoute;

            float completePercent = i / (float)(pointsSpline.Count - 1);
            uvs[indexSom] = new Vector2(0, completePercent);
            uvs[indexSom + 1] = new Vector2(1, completePercent);

            if (i < pointsSpline.Count - 1)
            {
                triangle[indexTri] = indexSom;
                triangle[indexTri + 1] = (indexSom + 2) % sommets.Length;
                triangle[indexTri + 2] = indexSom + 1;

                triangle[indexTri + 3] = (indexSom + 1);
                triangle[indexTri + 4] = (indexSom + 2) % sommets.Length;
                triangle[indexTri + 5] = (indexSom + 3) % sommets.Length;
            }
            indexSom += 2;
            indexTri += 6;
        }

        Debug.Log(sommets.Length);
        Mesh mesh = new Mesh();
        mesh.vertices = sommets;
        mesh.triangles = triangle;
        mesh.uv = uvs;
        return mesh;
    }
}
