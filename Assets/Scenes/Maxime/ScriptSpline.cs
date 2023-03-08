

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
    //private float tiling = 1;
    private List<Vector3> pointsSpline;
    private Mesh maillage;

    //private Mesh maillageBord;
    //private Mesh maillageBordImp;

    public Vector3[] sommets;
    Vector3 gauche;
    Vector3 directionAvant;

    [SerializeField]
    Material matériaux;

    //[SerializeField]
    //Material matériauxBord;

    //[SerializeField]
    //Material matériauxBordImp;
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

    private void Start()
    {
        /*maillage = new Mesh();
        MeshCollider meshc = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
        pointsSpline = GetComponent<GestionnaireJeux>().Chemin;
        maillage = CréerMeshRoute(pointsSpline);
        GetComponent<MeshFilter>().mesh = maillage;
        meshc.sharedMesh = maillage;
        GetComponent<MeshRenderer>().material = matériaux;*/



        //mesh bordure pair
        // maillage.SetSubMesh(0, maillageBord, [MeshUpdateFlags flags = MeshUpdateFlags.Default]);

        //MeshCollider meshcBord = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
        //maillageBord = CréerBordurePair(sommets);
        //GetComponent<MeshFilter>().mesh = maillageBord;
        //meshcBord.sharedMesh = maillageBord;
        //GetComponent<MeshRenderer>().material = matériauxBord;

        //mesh bordure impair
        //maillageBordImp = new Mesh();
        //MeshCollider meshcBordImp = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
        //maillageBordImp = CréerBordureImpair(sommets);
        //GetComponent<MeshFilter>().mesh = maillageBordImp;
        //meshcBordImp.sharedMesh = maillageBordImp;
        //GetComponent<MeshRenderer>().material = matériauxBordImp;

        //int textureRepeat = Mathf.RoundToInt(tiling * pointsSpline.Count); 
        //GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(1, textureRepeat);
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

            // calculer 2 sommets
            sommets[indexSom] = pointsSpline[i] + gauche * largeurRoute;
            sommets[indexSom + 1] = pointsSpline[i] - gauche * largeurRoute;

            //texture
            float completePercent = i / (float)(pointsSpline.Count - 1);
            //float v = 1 - Mathf.Abs(2 * completePercent - 1);
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

        ////bordure pair
        //Vector2[] uvsBordurePair = new Vector2[sommets.Length];
        //int nbTrianglesPair = sommets.Length;
        //int[] trianglePair = new int[nbTrianglesPair * 3];
        //static bool EstPair(int nb) { return nb % 2 == 0; }
        //Vector3[] pointsBordurePair = new Vector3[sommets.Length];
        //int indexSomPair = 0;
        //int indexTriPair = 0;

        //// bordure pair
        //for (int i = 0; i < sommets.Length; i++)
        //{
        //    if (EstPair(i))
        //    {
        //        pointsBordurePair[i] = new Vector3(sommets[i].x, 0, sommets[i].z);
        //        pointsBordurePair[i + 1] = new Vector3(sommets[i].x, sommets[i].y + 5f, sommets[i].z);

        //        float completePercent = i / (float)(sommets.Length - 1);
        //        uvsBordurePair[indexSomPair] = new Vector2(0, completePercent);
        //        uvsBordurePair[indexSomPair + 1] = new Vector2(1, completePercent);

        //        if (i < sommets.Length - 1)
        //        {
        //            trianglePair[indexTriPair] = indexSomPair;
        //            trianglePair[indexTriPair + 1] = (indexSomPair + 2) % pointsBordurePair.Length;
        //            trianglePair[indexTriPair + 2] = indexSomPair + 1;

        //            trianglePair[indexTriPair + 3] = (indexSomPair + 1);
        //            trianglePair[indexTriPair + 4] = (indexSomPair + 2) % pointsBordurePair.Length;
        //            trianglePair[indexTriPair + 5] = (indexSomPair + 3) % pointsBordurePair.Length;
        //        }
        //        indexSomPair += 2;
        //        indexTriPair += 6;

        //    }

        //}

        /*for (int i = 0; i < sommets.Length; i++)
        {
            Instantiate(GetComponent<GestionnaireJeux>().obstalce1, sommets[i],
                GetComponent<GestionnaireJeux>().obstalce1.transform.rotation);
        }*/



        Debug.Log(sommets.Length);
        Mesh mesh = new Mesh();
        mesh.vertices = sommets;
        mesh.triangles = triangle;
        mesh.uv = uvs;
        return mesh;
    }
}
