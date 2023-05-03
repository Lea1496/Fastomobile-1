
using UnityEngine;



[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class GénérateurCheckPoints : MonoBehaviour
{
    
    Vector3 gauche;
    Vector3 directionAvant;
    private Vector3[] points;
    private Mesh maillage;
    private int ind;
  

    public void FaireMesh(int indice, Vector3[] sommets)
    {
        points = sommets;
        ind = indice;
        maillage = new Mesh();
        MeshCollider meshc = gameObject.GetComponent(typeof(MeshCollider)) as MeshCollider;
        maillage = CréerMesh();
        GetComponent<MeshFilter>().mesh.Clear();
        GetComponent<MeshFilter>().mesh = maillage;
        meshc.sharedMesh = maillage;
        gameObject.GetComponent<MeshCollider>().convex = true;
        gameObject.GetComponent<MeshCollider>().isTrigger = true;

    }

    private Mesh CréerMesh()
    {
        Mesh mesh;
        Vector3[] sommetsTri = new Vector3[8];
        Vector2[] uvs = new Vector2[4];
        int[] triangle = new int[24];
        
        sommetsTri[0] = points[ind];
        sommetsTri[1] = points[ind + 1];

        sommetsTri[2] = new Vector3(sommetsTri[0].x, sommetsTri[0].y + 20, sommetsTri[0].z);
        sommetsTri[3] = new Vector3(sommetsTri[1].x, sommetsTri[1].y + 20, sommetsTri[1].z);
        sommetsTri[4] = new Vector3(sommetsTri[0].x + 1, sommetsTri[0].y, sommetsTri[0].z);
        sommetsTri[5] = new Vector3(sommetsTri[1].x +1, sommetsTri[1].y, sommetsTri[1].z);
        sommetsTri[6] = new Vector3(sommetsTri[0].x +1, sommetsTri[0].y + 20, sommetsTri[0].z);
        sommetsTri[7] = new Vector3(sommetsTri[1].x +1, sommetsTri[1].y + 20, sommetsTri[1].z);
        
            triangle[0] = 0;
            triangle[1] = 1;
            triangle[2] = 3;

            triangle[3] = 3;
            triangle[4] = 2;
            triangle[5] = 0;
            
            triangle[6] = 7;
            triangle[7] = 5;
            triangle[8] = 4;

            triangle[9] = 4;
            triangle[10] = 6;
            triangle[11] = 7;
            
            triangle[12] = 1;
            triangle[13] = 0;
            triangle[14] = 4;
            
            triangle[15] = 1;
            triangle[16] = 4;
            triangle[17] = 5;
            
            triangle[18] = 2;
            triangle[19] = 3;
            triangle[20] = 6;
            
            triangle[21] = 3;
            triangle[22] = 7;
            triangle[23] = 6;
            
        mesh = new Mesh();
        mesh.vertices = sommetsTri;
        mesh.triangles = triangle;
        
        return mesh;
    }
}

