using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCreator : MonoBehaviour
{
    [SerializeField]
    private LineRenderer myLineRenderer = null;

    [HideInInspector]
    public Path path;

    public Color anchorCol = Color.red;
    public Color controlCol = Color.white;
    public Color segmentCol = Color.green;
    public Color selectedSegmentCol = Color.yellow;
    public float anchorDiameter = .1f;
    public float controlDiameter = .075f;
    public bool displayControlPoints = true;

    private void Start()
    {
        UpdateLineRenderer();
    }

    public void UpdateLineRenderer()
    {
        Vector3[] v3 = ConvertV2V3(path.CalculateEvenlySpacedPoints());
        myLineRenderer.positionCount = v3.Length;
        myLineRenderer.SetPositions(v3);
    }

    private Vector3[] ConvertV2V3(Vector2[] aVector2s)
    {
        Vector3[] v3 = new Vector3[aVector2s.Length];
        for (int i = 0; i < v3.Length; i++)
        {
            Vector2 tempV2 = aVector2s[i];
            v3[i] = new Vector3(tempV2.x, tempV2.y, 0);
        }

        return v3;
    }

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
    }
}