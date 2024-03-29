
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class ScriptBézier
{
    private List<Vector3> pointsSpline = new List<Vector3>();
    public List<Vector3> PointsSpline
    {
        get => pointsSpline;
    }
    public ScriptBézier(List<Vector3> liste)
    {
        pointsSpline = liste;
        pointsSpline = CréerPointBézier(pointsSpline);
    }
    private List<Vector3> CréerPointBézier(List<Vector3> pointsSpline)
    {
        float t;
        Vector3 position;
        List<Vector3> pointsBézier = new List<Vector3>();
        Vector3 p1;
        Vector3 p2;
        Vector3 p3;
        Vector3 p4;
        pointsBézier.Add(Vector3.zero);
        position = new Vector3(0, 0, 0);
        int dernièrePos = 0;
        for (int j = 0; j < pointsSpline.Count - 3 ; j += 3)
        {
            if (j < 2)
            {
                p1 = position;
                p2 = pointsSpline[j + 1];
                p3 = pointsSpline[j + 2];
                p4 = pointsSpline[j + 3];
                
            }
            else
            {
                p1 = pointsSpline[j - 1];
                p2 = pointsSpline[j];
                p3 = pointsSpline[j + 1];
                p4 = pointsSpline[j + 2];

            }

            dernièrePos = j + 3;
            for (int i = 1; i < 8; i++)
            {
                t = i / 8f;
                
                pointsBézier.Add(CalculateBezierPoint(t, p1, p2, p3, p4));
                position = pointsBézier.Last();
            }
        }

        
        p1 = position;
        p2 = pointsSpline[dernièrePos];
        p3 = pointsBézier[1];
        p4 = pointsBézier[8];
       
        for (int i = 0; i < 8; i++)
        {
            t = i / 8f;
            pointsBézier[i] = CalculateBezierPoint(t, p1, p2, p3, p4);
        }
        pointsBézier[8] = Vector3.Lerp(pointsBézier[7], pointsBézier[9], 0.5f);
        pointsBézier.Remove(pointsBézier[0]);
        
        return pointsBézier;
    }
    
    //Code de : https://forum.unity.com/threads/generating-endless-bezier-curve.1009732/
    private Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
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
