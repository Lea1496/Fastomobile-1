using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class BordurePair : MonoBehaviour
{
    private Mesh maillage;
    private Vector3[] sommet;

    [SerializeField]
    Material matériauxBord;

    private void Start()
    {
        //System.Threading.Thread.Sleep(1000);
        sommet = GetComponentInParent<ScriptSpline>().sommets;
       // List<Vector3> pointsSpline2 = GetComponentInParent<GestionnaireJeux>().Chemin;
        //Debug.Log(pointsSpline2.Count);
        maillage = new Mesh();
        MeshCollider meshc = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
        maillage = AjouterBordure();
        GetComponent<MeshFilter>().mesh = maillage;
        meshc.sharedMesh = maillage;
        GetComponent<MeshRenderer>().material = matériauxBord;

        //int textureRepeat = Mathf.RoundToInt(tiling * pointsSpline.Count); 
        //GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(1, textureRepeat);
    }

    


    private Mesh AjouterBordure()
    {
        //sommet = new Vector3[pointsSpline2.Count * 2];
        int indexSomPair = 0;
        Vector2[] uvsBordurePair = new Vector2[sommet.Length]; 
        int nbTrianglesPair = sommet.Length;
        int[] trianglePair = new int[nbTrianglesPair * 3];
        int indexTriPair = 0;
        static bool EstPair(int nb) { return nb % 2 == 0; }
        Vector3[] pointsBordurePair = new Vector3[sommet.Length];
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
        //Debug.Log(sommet.Length);

        // bordure pair
        for (int i = 0; i < sommet.Length; i++)
        {
            if (EstPair(i))
            {
                pointsBordurePair[i] = new Vector3(sommet[i].x, 0, sommet[i].z);
                pointsBordurePair[i + 1] = new Vector3(sommet[i].x, sommet[i].y + 5f, sommet[i].z);

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
                }
                indexSomPair += 2;
                indexTriPair += 6;
            }
        }
        Mesh mesh = new Mesh();
        mesh.vertices = pointsBordurePair;
        mesh.triangles = trianglePair;
        mesh.uv = uvsBordurePair;
        return mesh;
    }
}
