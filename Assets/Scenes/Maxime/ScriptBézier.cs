using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Bézier : MonoBehaviour
{
    public List<Vector3> CréerPointBézier(List<Vector3> pointsSpline)
    {
        float t;
        int position = 0;
        List<Vector3> pointsBézier = new List<Vector3>();
        Vector3 p1;
        Vector3 p2;
        Vector3 p3;
        Vector3 p4;

        for (int j = 0; j < pointsSpline.Count; j += 4)
        {
            if (j < 2)
            {
                p1 = pointsSpline[j];
                p2 = pointsSpline[j + 1];
                p3 = pointsSpline[j + 2];
                p4 = pointsSpline[j + 3];
            }
            else
            {
                p1 = pointsBézier[position - 1];
                p2 = pointsBézier[position];
                p3 = pointsSpline[j];
                p4 = pointsSpline[j + 1];
                j -= 2;
            }

            for (int i = 0; i < 8; i++)
            {
                t = i / 8f;
                pointsBézier[i] = CalculateBezierPoint(t, p1, p2, p3, p4);
                position = i;
            }
        }
        return pointsBézier;
    }
    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 3 * uu * t * p1;
        p += 3 * u * tt * p2;
        p += ttt * p3;

        return p;
    }
}
