

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class ScriptSpline : MonoBehaviour
{
    private float largeurRoute = 70f;
    //private float tiling = 1;
    private List<Vector3> pointsSpline;
    private Mesh maillage;
    public Vector3[] sommets;
    Vector3 gauche;
    Vector3 directionAvant;

    [SerializeField]
    Material matériaux;

    private void Start()
    {
        maillage = new Mesh();
        MeshCollider meshc = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
        pointsSpline = GetComponent<GestionnaireJeux>().Chemin;
        maillage = CréerMeshRoute(pointsSpline);
        GetComponent<MeshFilter>().mesh = maillage;
        meshc.sharedMesh = maillage;
        GetComponent<MeshRenderer>().material = matériaux;

        //int textureRepeat = Mathf.RoundToInt(tiling * pointsSpline.Count); 
        //GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(1, textureRepeat);
    }

    private Mesh CréerMeshRoute(List<Vector3> pointsSpline) // prend vector3 pour faire route et s'assure de fermer la route
    {
        sommets = new Vector3[pointsSpline.Count * 2];
        Vector2[] uvs = new Vector2[sommets.Length]; // texture route
        int nbTriangles = 2 * (pointsSpline.Count - 1) + 2;
        int[] triangle = new int[nbTriangles * 3];
        int indexSom = 0;
        int indexTri = 0;

        for (int i = 0; i < pointsSpline.Count; i++) //pour chaque point on veut la direction forward et 2 sommets/point
        {
            directionAvant = Vector3.zero;
            if (i < pointsSpline.Count - 1) // dernier point 
            {
                directionAvant += pointsSpline[(i + 1) % pointsSpline.Count] - pointsSpline[i];
            }
            if (i > 0) // premier point
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

            
            //tous les pts sauf le dernier pour faire les triangles 
            if (i < pointsSpline.Count - 1)
            {
                triangle[indexTri] = indexSom;
                triangle[indexTri + 1] = (indexSom + 2) % sommets.Length;
                triangle[indexTri + 2] = indexSom + 1;

                triangle[indexTri + 3] = (indexSom + 1) ; //2
                triangle[indexTri + 4] = (indexSom + 2) % sommets.Length; //3
                triangle[indexTri + 5] = (indexSom + 3) % sommets.Length; //1
            }
            indexSom += 2;
            indexTri += 6;
        }

        /*for (int i = 0; i < sommets.Length; i++)
        {
            Instantiate(GetComponent<GestionnaireJeux>().obstalce1, sommets[i],
                GetComponent<GestionnaireJeux>().obstalce1.transform.rotation);
        }*/
        Mesh mesh = new Mesh();
        mesh.vertices = sommets;
        mesh.triangles = triangle;
        mesh.uv = uvs;
        return mesh;

       
    }
}
