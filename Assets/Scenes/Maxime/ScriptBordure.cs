using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class ScriptBordure : MonoBehaviour 
{
    private Mesh maillage;
   
    [SerializeField]
    Material matériauxBord;

    private void Start()
    {
        //maillage = new Mesh();
        //MeshCollider meshc = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
        //maillage = AjouterBordure(sommets);
        //GetComponent<MeshFilter>().mesh = maillage;
        //meshc.sharedMesh = maillage;
        //GetComponent<MeshRenderer>().material = matériauxBord;

        //int textureRepeat = Mathf.RoundToInt(tiling * pointsSpline.Count); 
        //GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(1, textureRepeat);
    }
    private Mesh AjouterBordure(Vector3[] sommets)
    {

        Vector2[] uvs = new Vector2[sommets.Length]; // texture route
        int nbTrianglesPair = (sommets.Length - 1) + 2;
        int[] trianglePair = new int[nbTrianglesPair * 3];
        int indexSom = 0;
        int indexTri = 0;

        static bool EstPair(int nb) { return nb % 2 == 0; }
        Vector3[] pointsBordurePair = new Vector3[sommets.Length];
        //Vector3[] pointsBordureImpair = new Vector3[sommets.Length];


        for (int i = 0; i < sommets.Length; i++)
        {
            if (EstPair(i))
            {
                pointsBordurePair[i] = new Vector3(sommets[i].x, 0, sommets[i].z);
                pointsBordurePair[i + 1] = new Vector3(sommets[i].x, sommets[i].y + 5f, sommets[i].z);
            }

            float completePercent = i / (float)(sommets.Length - 1);
            uvs[indexSom] = new Vector2(0, completePercent);
            uvs[indexSom + 1] = new Vector2(1, completePercent);

            if (i < sommets.Length - 1)
            {
                trianglePair[indexTri] = indexSom;
                trianglePair[indexTri + 1] = (indexSom + 2) % pointsBordurePair.Length;
                trianglePair[indexTri + 2] = indexSom + 1;

                trianglePair[indexTri + 3] = (indexSom + 1);
                trianglePair[indexTri + 4] = (indexSom + 2) % pointsBordurePair.Length;
                trianglePair[indexTri + 5] = (indexSom + 3) % pointsBordurePair.Length;
            }
            indexSom += 2;
            indexTri += 6;
        }

        Mesh mesh = new Mesh();
        mesh.vertices = pointsBordurePair;
        mesh.triangles = trianglePair;
        mesh.uv = uvs;
        return mesh;
    }
}
