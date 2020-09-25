using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    [SerializeField]
    private PathPlacer pathPlacer = null;

    [HideInInspector]
    public Path path;

    public Color anchorCol = Color.red;
    public Color controlCol = Color.white;
    public Color segmentCol = Color.green;
    public Color selectedSegmentCol = Color.yellow;
    public float anchorDiameter = .1f;
    public float controlDiameter = .075f;
    public bool displayControlPoints = true;

    public void CreatePath()
    {
        path = new Path(transform.position);
    }

    public void StartOver()
    {
        Reset();
    }

    void Reset()
    {
        CreatePath();

        if (pathPlacer == null)
        {
            Debug.Log("PathCreator " + this + " is missing a PathPlacer!");
            return;
        }

        pathPlacer.CreateSpheres();
    }

    public Vector2 GetClosestPoint(Vector2 aPosition, ref int aPointsIndex)
    {
        Vector2 closestPoint = new Vector2(-100, 100);
        Vector2[] evenPoints = path.CalculateEvenlySpacedPoints();

        for (int i = 0; i < evenPoints.Length; i++)
        {
            if (Vector2.Distance(aPosition, evenPoints[i]) < Vector2.Distance(aPosition, closestPoint))
            {
                closestPoint = evenPoints[i];
                aPointsIndex = i;
            }
        }

        return closestPoint;
    }
}