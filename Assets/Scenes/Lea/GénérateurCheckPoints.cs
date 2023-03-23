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

    public void FaireMesh(int indice, List<Vector3> chemin, Vector3[] sommets)
    {
        points = sommets;
        ind = indice;
        maillage = new Mesh();
        MeshCollider meshc = gameObject.GetComponent(typeof(MeshCollider)) as MeshCollider;
//        pointsSpline = GetComponent<GestionnaireJeux>().Chemin;
        pointsSpline = chemin;
        maillage = CréerMesh();
        GetComponent<MeshFilter>().mesh.Clear();
        GetComponent<MeshFilter>().mesh = maillage;
        meshc.sharedMesh = maillage;
        GetComponent<MeshRenderer>().material = matériaux;


    }

    private Mesh CréerMesh()
    {
        // le code CréerMeshRoute vient de https://www.youtube.com/watch?v=Q12sb-sOhdI
        Mesh mesh;
        Vector3[] sommetsTri = new Vector3[4];
        Vector2[] uvs = new Vector2[4];
        int nbTriangles = 2 * pointsSpline.Count;
        int[] triangle = new int[6];
        int indexSom = 0;
        int indexTri = 0;

        
        sommetsTri[0] = points[ind];
        sommetsTri[1] = points[ind + 1];



        //sommets[0] = points[ind];
        //sommets[1] = points[ind + 1];
        sommetsTri[2] = new Vector3(sommetsTri[0].x, sommetsTri[0].y + 20, sommetsTri[0].z);
        sommetsTri[3] = new Vector3(sommetsTri[1].x, sommetsTri[1].y + 20, sommetsTri[1].z);

       // float completePercent = ind / (float)(pointsSpline.Count* 2 - 1);
        
        if (ind < pointsSpline.Count - 1)
        {
            triangle[0] = 0;
            triangle[1] = 1;
            triangle[2] = 3;

            triangle[3] = 3;
            triangle[4] = 2;
            triangle[5] = 0;
        }


        mesh = new Mesh();
        mesh.vertices = sommetsTri;
        mesh.triangles = triangle;




        return mesh;
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
