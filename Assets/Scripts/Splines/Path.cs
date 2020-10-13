using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Path
{
    [SerializeField, HideInInspector]
    List<Vector2> points;
    [SerializeField, HideInInspector]
    bool isClosed;
    [SerializeField, HideInInspector]
    bool autoSetControlPoints;
    
    private int myBoostStart = 0;
    private int myBoostEnd = 0;

    public void UpdateBoost(int aBoostStart, int aBoostEnd)
    {
        myBoostStart = aBoostStart;
        myBoostEnd = aBoostEnd;
    }

    public Vector2 GetBoost()
    {
        return new Vector2(myBoostStart, myBoostEnd);
    }

    public Path(Vector2 centre)
    {
        points = new List<Vector2>
        {
            centre+Vector2.left,
            centre+(Vector2.left+Vector2.up)*.5f,
            centre + (Vector2.right+Vector2.down)*.5f,
            centre + Vector2.right
        };
    }

    public Vector2 this[int i]
    {
        get
        {
            return points[i];
        }
    }

    public bool IsClosed
    {
        get
        {
            return isClosed;
        }
        set
        {
            if (isClosed != value)
            {
                isClosed = value;

                if (isClosed)
                {
                    points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
                    points.Add(points[0] * 2 - points[1]);
                    if (autoSetControlPoints)
                    {
                        AutoSetAnchorControlPoints(0);
                        AutoSetAnchorControlPoints(points.Count - 3);
                    }
                }
                else
                {
                    points.RemoveRange(points.Count - 2, 2);
                    if (autoSetControlPoints)
                    {
                        AutoSetStartAndEndControls();
                    }
                }
            }
        }
    }

    public bool AutoSetControlPoints
    {
        get
        {
            return autoSetControlPoints;
        }
        set
        {
            if (autoSetControlPoints != value)
            {
                autoSetControlPoints = value;
                if (autoSetControlPoints)
                {
                    AutoSetAllControlPoints();
                }
            }
        }
    }

    public int NumPoints
    {
        get
        {
            return points.Count;
        }
    }

    public int NumSegments
    {
        get
        {
            return points.Count / 3;
        }
    }

    public void AddSegment(Vector2 anAnchorPos)
    {
        points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
        points.Add((points[points.Count - 1] + anAnchorPos) * .5f);
        points.Add(anAnchorPos);

        if (autoSetControlPoints)
        {
            AutoSetAllAffectedControlPoints(points.Count - 1);
        }
    }

    public void SplitSegment(Vector2 anAnchorPos, int aSegmentIndex)
    {
        points.InsertRange(aSegmentIndex * 3 + 2, new Vector2[] { Vector2.zero, anAnchorPos, Vector2.zero });
        if (autoSetControlPoints)
        {
            AutoSetAllAffectedControlPoints(aSegmentIndex * 3 + 3);
        }
        else
        {
            AutoSetAnchorControlPoints(aSegmentIndex * 3 + 3);
        }
    }

    public void DeleteSegment(int anAnchorIndex)
    {
        if (NumSegments > 2 || !isClosed && NumSegments > 1)
        {
            if (anAnchorIndex == 0)
            {
                if (isClosed)
                {
                    points[points.Count - 1] = points[2];
                }
                points.RemoveRange(0, 3);
            }
            else if (anAnchorIndex == points.Count - 1 && !isClosed)
            {
                points.RemoveRange(anAnchorIndex - 2, 3);
            }
            else
            {
                points.RemoveRange(anAnchorIndex - 1, 3);
            }
        }
    }

    public Vector2[] GetPointsInSegment(int anIndex)
    {
        return new Vector2[] { points[anIndex * 3], points[anIndex * 3 + 1], points[anIndex * 3 + 2], points[LoopIndex(anIndex * 3 + 3)] };
    }

    public void MovePoint(int anIndex, Vector2 aPos)
    {
        Vector2 deltaMove = aPos - points[anIndex];

        if (anIndex % 3 == 0 || !autoSetControlPoints)
        {
            points[anIndex] = aPos;

            if (autoSetControlPoints)
            {
                AutoSetAllAffectedControlPoints(anIndex);
            }
            else
            {

                if (anIndex % 3 == 0)
                {
                    if (anIndex + 1 < points.Count || isClosed)
                    {
                        points[LoopIndex(anIndex + 1)] += deltaMove;
                    }
                    if (anIndex - 1 >= 0 || isClosed)
                    {
                        points[LoopIndex(anIndex - 1)] += deltaMove;
                    }
                }
                else
                {
                    bool nextPointIsAnchor = (anIndex + 1) % 3 == 0;
                    int correspondingControlIndex = (nextPointIsAnchor) ? anIndex + 2 : anIndex - 2;
                    int anchorIndex = (nextPointIsAnchor) ? anIndex + 1 : anIndex - 1;

                    if (correspondingControlIndex >= 0 && correspondingControlIndex < points.Count || isClosed)
                    {
                        float dst = (points[LoopIndex(anchorIndex)] - points[LoopIndex(correspondingControlIndex)]).magnitude;
                        Vector2 dir = (points[LoopIndex(anchorIndex)] - aPos).normalized;
                        points[LoopIndex(correspondingControlIndex)] = points[LoopIndex(anchorIndex)] + dir * dst;
                    }
                }
            }
        }
    }

    public Vector2[] CalculateEvenlySpacedPoints(float aSpacing = 0.1f, float aResolution = 10)
    {
        List<Vector2> evenlySpacedPoints = new List<Vector2>();
        Vector2 previousPoint = points[0];
        evenlySpacedPoints.Add(previousPoint);        
        float dstSinceLastEvenPoint = 0;

        for (int segmentIndex = 0; segmentIndex < NumSegments; segmentIndex++)
        {
            Vector2[] p = GetPointsInSegment(segmentIndex);
            float controlNetLength = Vector2.Distance(p[0], p[1]) + Vector2.Distance(p[1], p[2]) + Vector2.Distance(p[2], p[3]);
            float estimatedCurveLength = Vector2.Distance(p[0], p[3]) + controlNetLength / 2f;
            int divisions = Mathf.CeilToInt(estimatedCurveLength * aResolution);
            float t = 0;
            while (t <= 1)
            {
                t += 1f/divisions;
                Vector2 pointOnCurve = Bezier.EvaluateCubic(p[0], p[1], p[2], p[3], t);
                dstSinceLastEvenPoint += Vector2.Distance(previousPoint, pointOnCurve);

                while (dstSinceLastEvenPoint >= aSpacing)
                {
                    float overshootDst = dstSinceLastEvenPoint - aSpacing;
                    Vector2 newEvenlySpacedPoint = pointOnCurve + (previousPoint - pointOnCurve).normalized * overshootDst;
                    evenlySpacedPoints.Add(newEvenlySpacedPoint);
                    dstSinceLastEvenPoint = overshootDst;
                    previousPoint = newEvenlySpacedPoint;
                }

                previousPoint = pointOnCurve;
            }
        }

        return evenlySpacedPoints.ToArray();
    }

    public Vector2 GetFirstPoint()
    {
        return points[0];
    }

    public Vector2 GetLastPoint()
    {
        return points[points.Count - 1];
    }

    private void AutoSetAllAffectedControlPoints(int anUpdatedAnchorIndex)
    {
        for (int i = anUpdatedAnchorIndex - 3; i <= anUpdatedAnchorIndex + 3; i += 3)
        {
            if (i >= 0 && i < points.Count || isClosed)
            {
                AutoSetAnchorControlPoints(LoopIndex(i));
            }
        }

        AutoSetStartAndEndControls();
    }

    private void AutoSetAllControlPoints()
    {
        for (int i = 0; i < points.Count; i += 3)
        {
            AutoSetAnchorControlPoints(i);
        }

        AutoSetStartAndEndControls();
    }

    private void AutoSetAnchorControlPoints(int anAnchorIndex)
    {
        Vector2 anchorPos = points[anAnchorIndex];
        Vector2 dir = Vector2.zero;
        float[] neighbourDistances = new float[2];

        if (anAnchorIndex - 3 >= 0 || isClosed)
        {
            Vector2 offset = points[LoopIndex(anAnchorIndex - 3)] - anchorPos;
            dir += offset.normalized;
            neighbourDistances[0] = offset.magnitude;
        }
        if (anAnchorIndex + 3 >= 0 || isClosed)
        {
            Vector2 offset = points[LoopIndex(anAnchorIndex + 3)] - anchorPos;
            dir -= offset.normalized;
            neighbourDistances[1] = -offset.magnitude;
        }

        dir.Normalize();

        for (int i = 0; i < 2; i++)
        {
            int controlIndex = anAnchorIndex + i * 2 - 1;
            if (controlIndex >= 0 && controlIndex < points.Count || isClosed)
            {
                points[LoopIndex(controlIndex)] = anchorPos + dir * neighbourDistances[i] * .5f;
            }
        }
    }

    private void AutoSetStartAndEndControls()
    {
        if (!isClosed)
        {
            points[1] = (points[0] + points[2]) * .5f;
            points[points.Count - 2] = (points[points.Count - 1] + points[points.Count - 3]) * .5f;
        }
    }

    private int LoopIndex(int anIndex)
    {
        return (anIndex + points.Count) % points.Count;
    }
}