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

    public Vector2 GetClosestPoint(Vector2 aPosition, ref int aPointsIndex, ref Vector2[] aPoints)
    {
        Vector2 closestPoint = Vector2.zero;

        for (int i = 0; i < pathCreators.Length; i++)
        {
            Vector2 closestPointInIteration = pathCreators[i].GetClosestPoint(aPosition);

            if (closestPoint == Vector2.zero)
            {
                closestPoint = closestPointInIteration;
            }
            else if (Vector2.Distance(aPosition, closestPointInIteration) < Vector2.Distance(aPosition, closestPoint))
            {
                Debug.Log(this + " found new closest point.");
                closestPoint = closestPointInIteration;
                aPointsIndex = i;
                aPoints = pathCreators[i].path.CalculateEvenlySpacedPoints();
            }
        }

        return closestPoint;
    }
}