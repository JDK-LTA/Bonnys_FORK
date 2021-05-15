using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve
{
    // Calculate value of Bezier curve
    /// <summary>
    /// T is the number of points the curve is made of. Control points are the pivots.
    /// </summary>
    /// <param name="t"></param>
    /// <param name="controlPoints"></param>
    /// <returns></returns>
    public static List<Vector3> DeCasteljau(/*int degree, */float t, List<Vector3> controlPoints)
    {
        List<Vector3> f = null;
        int degree = controlPoints.Count - 1;
        //// Check if order matches the degree of the curve
        //if (controlPoints.Count != (degree + 1))
        //{
        //    Debug.Log("The number of control points has to be equal with the degree of the curve plus 1!!!");
        //    return f;
        //}
        //// Check degree
        //if (degree < 1)
        //{
        //    Debug.Log("Bezier curve has to be at least of degree 1!!!");
        //    return f;
        //}

        if (controlPoints.Count < 1)
        {
            Debug.Log("Bezier curve needs to have at least 1 point!!!");
            return f;
        }

        List<Vector3> bezierPoints = new List<Vector3>();
        for (int level = degree; level >= 0; level--)
        {
            // Top level of the DeCasteljau pyramid
            if (level == degree)
            {
                for (int i = 0; i <= degree; i++)
                {
                    bezierPoints.Add(controlPoints[i]);
                }
                continue;
            }

            // All the other levels are constructed using their
            // immediate above level
            int lastIdx = bezierPoints.Count;
            int levelIdx = level + 2;
            int idx = lastIdx - levelIdx;
            for (int i = 0; i <= level; i++)
            {
                Vector3 pi = (1 - t) * bezierPoints[idx] + t * bezierPoints[idx + 1];
                bezierPoints.Add(pi);
                ++idx;
            }
        }

        // Return the curve
        return bezierPoints;
    }
}
