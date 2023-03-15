using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
public class GénérateurCheckPoints : MonoBehaviour
{
    private float largeurRoute = 70f;
    private List<Vector3> pointsSpline;
    public Vector3[] sommets;
    Vector3 gauche;
    Vector3 directionAvant;
    private Vector3[] points;
    private Mesh maillage;
    private int ind;
    [SerializeField] Material matériaux;

    public void FaireMesh(int indice)
    {
        //points = sommets;
        ind = indice;
        maillage = new Mesh();
        MeshCollider meshc = gameObject.AddComponent(typeof(MeshCollider)) as MeshCollider;
        pointsSpline = GetComponent<GestionnaireJeux>().Chemin;

        CréerMesh(pointsSpline);
        GetComponent<MeshFilter>().mesh = maillage;
        meshc.sharedMesh = maillage;
        GetComponent<MeshRenderer>().material = matériaux;


    }

    private void CréerMesh(List<Vector3> pointsSpline)
    {
        // le code CréerMeshRoute vient de https://www.youtube.com/watch?v=Q12sb-sOhdI
        Mesh mesh;
        sommets = new Vector3[4];
        Vector2[] uvs = new Vector2[sommets.Length];
        int nbTriangles = 2 * pointsSpline.Count;
        int[] triangle = new int[6];
        int indexSom = 0;
        int indexTri = 0;

        //for (int i = 0; i < pointsSpline.Count; i++)
        {
            directionAvant = Vector3.zero;
            if (ind < pointsSpline.Count - 1)
            {
                directionAvant += pointsSpline[(ind + 1) % pointsSpline.Count] - pointsSpline[ind];
            }
            if (ind > 0)
            {
                directionAvant += pointsSpline[ind] - pointsSpline[(ind - 1 + pointsSpline.Count) % pointsSpline.Count];
            }
            directionAvant.Normalize();
            gauche = new Vector3(-directionAvant.z, 0, directionAvant.x);

            sommets[0] = pointsSpline[ind] + gauche * largeurRoute;
            sommets[1] = pointsSpline[ind] - gauche * largeurRoute;



            //sommets[0] = points[ind];
            //sommets[1] = points[ind + 1];
            sommets[2] = new Vector3(sommets[0].x, sommets[0].y + 20, sommets[0].z);
            sommets[3] = new Vector3(sommets[1].x, sommets[1].y + 20, sommets[1].z);

            float completePercent = ind / (float)(pointsSpline.Count - 1);
            uvs[0] = new Vector2(0, completePercent);
            uvs[1] = new Vector2(1, completePercent);

            if (ind < pointsSpline.Count - 1)
            {
                triangle[0] = 0;
                triangle[1] = 1;
                triangle[2] = 3;

                triangle[3] = 3;
                triangle[4] = 2;
                triangle[5] = 0;
            }


            // mesh = new Mesh();
            maillage.vertices = sommets;
            maillage.triangles = triangle;
            maillage.uv = uvs;



        }
    }
}


/*public class GénérateurCheckPoints
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public GénérateurCheckPoints(Vector3[] sommets, GameObject ligne, Transform transform)
    {
        Vector3 dernièrePos = new Vector3();
        Vector3 point1;
        Vector3 point2;
        
        for (int i = 0; i + +2< sommets.Length; i ++)
        {
            point1 = sommets[i + 1];
            point2 = sommets[i];
            Vector3 vecteur = new Vector3(point2.x - point1.x, point2.y - point1.y, point2.z - point1.z);
        
            Vector3 pointZ = new Vector3(vecteur.x, vecteur.y, vecteur.z + 150);
        
            float angle = Mathf.Acos(vecteur.x * pointZ.x + vecteur.y * pointZ.y + vecteur.z * pointZ.z);
            if (i != 0)
            {
                GameObject.Instantiate(ligne,  Vector3.Lerp(sommets[i++], sommets[i], 0.5f), Quaternion.LookRotation(Vector3.Lerp(sommets[i - 1], sommets[i], 0.5f)));
            }
            else
            {
               // GameObject.Instantiate(ligne,  Vector3.Lerp(sommets[i++], sommets[i], 0.5f), transform.rotation);
            }
            dernièrePos = Vector3.Lerp(sommets[i++], sommets[i], 0.5f);
            
        }
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}*/
