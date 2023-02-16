using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class ScriptSpline : MonoBehaviour
{
    private float largeurRoute = 75f;
    private List<Vector3> pointsSpline;
    private Mesh maillage;
    private void Start()
    {
        maillage = new Mesh();
        pointsSpline = GetComponent<GestionnaireJeux>().Chemin;
        Debug.Log(pointsSpline.Count + " nb points"); // pour v�rifier que c'est pas 0
        maillage = Cr�erMeshRoute(pointsSpline, true);
        GetComponent<MeshFilter>().mesh = maillage;
    }
    private Mesh Cr�erMeshRoute(List<Vector3> pointsSpline, bool estFerm�) // prend vector3 pour faire route et s'assure de fermer la route
    {
        Vector3[] sommets = new Vector3[pointsSpline.Count * 2];
        int nbTriangles = 2 * (pointsSpline.Count - 1) + ((estFerm�) ? 2 : 0);
        int[] triangle = new int[nbTriangles * 3];
        int indexSom = 0;
        int indexTri = 0;

        for (int i = 0; i < pointsSpline.Count; i++) //pour chaque point on veut la direction forward et 2 sommets/point
        {
            Vector3 directionAvant = Vector3.zero;
            if (i < pointsSpline.Count - 1 || estFerm�) // dernier point
            {
                directionAvant += pointsSpline[(i + 1) % pointsSpline.Count] - pointsSpline[i];
            }
            if (i > 0 || estFerm�) // premier point
            {
                directionAvant += pointsSpline[i] - pointsSpline[(i - 1 + pointsSpline.Count) % pointsSpline.Count];
            }
            directionAvant.Normalize();
            Vector3 gauche = new Vector3(-directionAvant.y, directionAvant.x); //pour prendre sommet � coter du pt

            // calculer 2 sommets
            sommets[indexSom] = pointsSpline[i] + gauche * largeurRoute * .5f;
            sommets[indexSom + 1] = pointsSpline[i] - gauche * largeurRoute * .5f;

            //tous les pts sauf le dernier pour faire les triangles 
            if (i < pointsSpline.Count - 1 || estFerm�)
            {
                triangle[indexTri] = indexSom;
                triangle[indexTri + 1] = (indexSom + 2) % sommets.Length;
                triangle[indexTri + 2] = indexSom + 1;

                triangle[indexTri + 3] = indexSom + 1;
                triangle[indexTri + 4] = (indexSom + 2) % sommets.Length;
                triangle[indexTri + 5] = (indexSom + 3) % sommets.Length;
            }
            indexSom += 2;
            indexTri += 6;
        }

        Mesh mesh = new Mesh();
        mesh.vertices = sommets;
        mesh.triangles = triangle;

        return mesh;
    }
}
