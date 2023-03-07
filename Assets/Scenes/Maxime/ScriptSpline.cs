

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

        //bordure pair
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

        //Vector3[] sommetsMesh = new Vector3[sommets.Length + pointsBordurePair.Length];
        //for (int i = 0; i < sommets.Length; i++)
        //{
        //    sommetsMesh[i] = sommets[i];
        //}
        //for (int i = 0; i < pointsBordurePair.Length; i++)
        //{
        //    sommetsMesh[sommets.Length + i] = pointsBordurePair[i];
        //}


        //int[] trianglesMesh = new int[triangle.Length + trianglePair.Length];
        //for (int i = 0; i < triangle.Length; i++)
        //{
        //    trianglesMesh[i] = triangle[i];
        //}
        //for (int i = 0; i < trianglePair.Length; i++)
        //{
        //    trianglesMesh[triangle.Length + i] = trianglePair[i] + triangle.Length - 1;
        //}

        //Vector2[] uvsMesh = new Vector2[uvs.Length + uvsBordurePair.Length];
        //for (int i = 0; i < uvs.Length; i++)
        //{
        //    uvsMesh[i] = uvs[i];
        //}
        //for (int i = 0; i < uvsBordurePair.Length; i++)
        //{
        //    uvsMesh[uvs.Length + i] = uvsBordurePair[i];
        //}

        Debug.Log(sommets.Length);
        Mesh mesh = new Mesh();
        mesh.vertices = sommets;
        mesh.triangles = triangle;
        mesh.uv = uvs;
        return mesh;
    }

    //private static int[] ConcatenerTableauxInt(int[] t1, int[] t2)
    //{
    //    int[] tableauConcatenés = new int[t1.Length + t2.Length];

    //    // On copie le premier tableau
    //    for (int i = 0; i < t1.Length; i++)
    //    {
    //        tableauConcatenés[i] = t1[i];
    //    }

    //    // On copie le deuxième tableau à la suite
    //    for (int i = 0; i < t2.Length; i++)
    //    {
    //        // observez bien l'indice ici!
    //        tableauConcatenés[t1.Length + i] = t2[i];
    //    }
    //    return tableauConcatenés;
    //}

    //private Mesh CréerBordurePair(Vector3[] sommets)
    //{
    //    //bordure pair
    //    Vector2[] uvsBordure = new Vector2[sommets.Length];
    //    int nbTrianglesPair = sommets.Length;
    //    int[] trianglePair = new int[nbTrianglesPair * 3];
    //    static bool EstPair(int nb) { return nb % 2 == 0; }
    //    Vector3[] pointsBordurePair = new Vector3[sommets.Length];
    //    int indexSom = 0;
    //    int indexTri = 0;

    //    // bordure pair
    //    for (int i = 0; i < sommets.Length; i++)
    //    {
    //        if (EstPair(i))
    //        {
    //            pointsBordurePair[i] = new Vector3(sommets[i].x, 0, sommets[i].z);
    //            pointsBordurePair[i + 1] = new Vector3(sommets[i].x, sommets[i].y + 5f, sommets[i].z);

    //            float completePercent = i / (float)(sommets.Length - 1);
    //            uvsBordure[indexSom] = new Vector2(0, completePercent);
    //            uvsBordure[indexSom + 1] = new Vector2(1, completePercent);

    //            if (i < sommets.Length - 1)
    //            {
    //                trianglePair[indexTri] = indexSom;
    //                trianglePair[indexTri + 1] = (indexSom + 2) % pointsBordurePair.Length;
    //                trianglePair[indexTri + 2] = indexSom + 1;

    //                trianglePair[indexTri + 3] = (indexSom + 1);
    //                trianglePair[indexTri + 4] = (indexSom + 2) % pointsBordurePair.Length;
    //                trianglePair[indexTri + 5] = (indexSom + 3) % pointsBordurePair.Length;
    //            }
    //            indexSom += 2;
    //            indexTri += 6;
    //        }
    //    }

    //    //SubMeshDescriptor meshBordure = new SubMeshDescriptor();
    //    Mesh meshBordure = new Mesh();
    //    meshBordure.vertices = pointsBordurePair;
    //    meshBordure.triangles = trianglePair;
    //    meshBordure.uv = uvsBordure;
    //    return meshBordure;
    //}

    //private Mesh CréerBordureImpair(Vector3[] sommets)
    //{
    //    //bordure impair
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
    //}
}
