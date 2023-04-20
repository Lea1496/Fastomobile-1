using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class CréateurLigneArrivée : MonoBehaviour
{
    Vector3 gauche;
    Vector3 directionAvant;
    private Vector3 p1;
    private Vector3 p2;
    private Mesh maillage;
    private int ind;


    public void FaireMesh( Vector3 point1, Vector3 point2)
    {
        p1 = point1;
        p2 = point2;
        maillage = new Mesh();
        maillage = CréerMesh();
        GetComponent<MeshFilter>().mesh.Clear();
        GetComponent<MeshFilter>().mesh = maillage;

    }

    private Mesh CréerMesh()
    {
        Mesh mesh;
        Vector3[] sommets = new Vector3[4];
        Vector2[] uvs = new Vector2[4];
        int[] triangle = new int[6];

        sommets[0] = new Vector3(p1.x , p1.y + 0.1f, p1.z);
        sommets[1] = new Vector3(p2.x, p2.y+0.1f, p2.z);

        sommets[2] = new Vector3(p1.x +10, p1.y + 0.1f, p1.z);
        sommets[3] = new Vector3(p2.x + 10 , p2.y +0.1f, p2.z);
        
        
        triangle[0] = 2;
        triangle[1] = 1;
        triangle[2] = 0;

        triangle[3] = 1;
        triangle[4] = 2;
        triangle[5] = 3;

        uvs[0] = new Vector2(0, 0);
        uvs[1] = new Vector2(1, 0);
        uvs[2] = new Vector2(0, 1);
        uvs[3] = new Vector2(1, 1);
    

        mesh = new Mesh();
        mesh.vertices = sommets;
        mesh.triangles = triangle;
        mesh.uv = uvs;
        return mesh;
    }
}
