using UnityEngine;

public class SplineManager : MonoBehaviour
{
    private PathCreator[] pathCreators;
    
    private void Start()
    {
        pathCreators = GetComponentsInChildren<PathCreator>();
    }

    public Vector2 GetClosestPoint(Vector2 aPlayerPosition, ref int aPointsIndex, ref Vector2[] aPoints)
    {
        Vector2 closestPoint = Vector2.negativeInfinity;
        int closestIndex = 0;

        for (int i = 0; i < pathCreators.Length; i++)
        {
            Vector2[] iteratedSplinesPoints = pathCreators[i].path.CalculateEvenlySpacedPoints();
            Vector2 closestPointInIteratedSpline = GetClosestPointInSpline(aPlayerPosition, iteratedSplinesPoints, ref aPointsIndex);

            if (Vector2.Distance(aPlayerPosition, closestPointInIteratedSpline) < Vector2.Distance(aPlayerPosition, closestPoint))
            {
                closestPoint = closestPointInIteratedSpline;
                aPoints = iteratedSplinesPoints;
                closestIndex = aPointsIndex;
            }
        }

        aPointsIndex = closestIndex;
        return closestPoint;
    }

    private Vector2 GetClosestPointInSpline(Vector2 aPlayerPosition, Vector2[] somePoints, ref int aPointsIndex)
    {
        Vector2 closestPoint = Vector2.negativeInfinity;

        for (int i = 0; i < somePoints.Length; i++)
        {
            if (Vector2.Distance(aPlayerPosition, somePoints[i]) < Vector2.Distance(aPlayerPosition, closestPoint))
            {
                closestPoint = somePoints[i];
                aPointsIndex = i;
            }
        }

        return closestPoint;
    }
}