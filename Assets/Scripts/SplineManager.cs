using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineManager : MonoBehaviour
{
    private PathCreator[] pathCreators;
    
    void Start()
    {
        pathCreators = GetComponentsInChildren<PathCreator>();
    }

    public Vector2 GetClosestPoint(Vector2 aPlayerPosition, ref int aPointsIndex, ref Vector2[] aPoints)
    {
        Vector2 closestPoint = new Vector2(-100100, 100000);
        int closestIndex = 0;

        for (int i = 0; i < pathCreators.Length; i++)
        {
            Vector2[] iteratedSplinesPoints = pathCreators[i].path.CalculateEvenlySpacedPoints();
            Vector2 closestPointInIteratedSpline = GetClosestPointInSpline(aPlayerPosition, iteratedSplinesPoints, ref aPointsIndex);

            if (Vector2.Distance(aPlayerPosition, closestPointInIteratedSpline) < Vector2.Distance(aPlayerPosition, closestPoint))
            {
                //Debug.Log(this + " found new closest point.");
                closestPoint = closestPointInIteratedSpline;
                aPoints = iteratedSplinesPoints;
                closestIndex = aPointsIndex;
            }
        }

        aPointsIndex = closestIndex;
        //Debug.Log("CLOSEST INDEX IN ALL SPLINES: " + aPointsIndex);

        return closestPoint;
    }

    private Vector2 GetClosestPointInSpline(Vector2 aPlayerPosition, Vector2[] somePoints, ref int aPointsIndex)
    {
        Vector2 closestPoint = new Vector2(-100000, 100000);

        for (int i = 0; i < somePoints.Length; i++)
        {
            if (Vector2.Distance(aPlayerPosition, somePoints[i]) < Vector2.Distance(aPlayerPosition, closestPoint))
            {
                closestPoint = somePoints[i];
                aPointsIndex = i;
            }
        }

        //Debug.Log("CLOSEST INDEX IN THIS SPLINE: " + aPointsIndex);

        return closestPoint;
    }
}