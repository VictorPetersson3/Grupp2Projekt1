using UnityEngine;

public class SplineManager : MonoBehaviour
{
    private PathCreator[] pathCreators;
    private Player myPlayer;
    private GameObject[] mySplines;

    [SerializeField]
    private float mySplineOffset = 25f;
    
    private void Start()
    {
        pathCreators = GetComponentsInChildren<PathCreator>();
        myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (mySplines == null)
        {
            mySplines = GameObject.FindGameObjectsWithTag("Spline");
        }

        for (int i = 0; i < pathCreators.Length; i++)
        {
            pathCreators[i].path.CalculateEvenlySpacedPoints();
        }
    }

    private void Update()
    {
        //SetSplineActivate();
    }

    public bool PlayerSplineCollision(Vector2 aPlayerPos, Vector2 anOldPos, ref int aPointsIndex, ref Vector2[] someCurrentPoints, ref Vector2 aBoost, ref bool aIsRail, bool aIsFalling, bool aIsBackflipping)
    {
        for (int i = 0; i < pathCreators.Length; i++)
        {
            //Spline too far away too matter
            if (!pathCreators[i].isActiveAndEnabled ||
                pathCreators[i].path.GetFirstPoint().x > aPlayerPos.x ||
                pathCreators[i].path.GetLastPoint().x < aPlayerPos.x)
            {
                continue;
            }
            //Ignore rails when backflipping and when travelling upwards
            if (pathCreators[i].GetIsRail() && (!aIsFalling || aIsBackflipping))
            {
                continue;
            }
            Vector2[] points = pathCreators[i].path.GetMyEvenlySpacedPoints();
            const int offset = 1;
            for (int j = 0; j < points.Length; j += offset)
            {
                bool collide;
                if (j + offset >= points.Length)
                {
                    collide = LineLineIntersection(anOldPos, aPlayerPos, points[points.Length - (offset + 1)], points[points.Length - 1]);
                }
                else
                {
                    collide = LineLineIntersection(anOldPos, aPlayerPos, points[j], points[j + offset]);
                }
                if (collide)
                {
                    bool catchSpline = false;
                    const int indexDelta = 10;
                    if (someCurrentPoints != points)
                    {
                        catchSpline = true;
                    }
                    else if (j - aPointsIndex > indexDelta)
                    {
                        catchSpline = true;
                    }
                    else if (aPointsIndex == -1)
                    {
                        catchSpline = true;
                    }

                    if (catchSpline)
                    {
                        aPointsIndex = j;
                        someCurrentPoints = points;
                        aBoost = pathCreators[i].GetBoost();
                        aIsRail = pathCreators[i].GetIsRail();
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public static bool LineLineIntersection(Vector3 aLine1point1, Vector3 aLine1point2, Vector3 aLine2point1, Vector3 aLine2point2)
    {
        Vector2 a = aLine1point2 - aLine1point1;
        Vector2 b = aLine2point1 - aLine2point2;
        Vector2 c = aLine1point1 - aLine2point1;

        float alphaNumerator = b.y * c.x - b.x * c.y;
        float betaNumerator = a.x * c.y - a.y * c.x;
        float denominator = a.y * b.x - a.x * b.y;

        if (denominator == 0)
        {
            return false;
        }
        else if (denominator > 0)
        {
            if (alphaNumerator < 0 || alphaNumerator > denominator || betaNumerator < 0 || betaNumerator > denominator)
            {
                return false;
            }
        }
        else if (alphaNumerator > 0 || alphaNumerator < denominator || betaNumerator > 0 || betaNumerator < denominator)
        {
            return false;
        }
        return true;
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

    public Vector2 GetGroundDirection(Vector2 aPlayerPosition)
    {
        int closestIndex = 0;
        Vector2 closestPoint = Vector2.negativeInfinity;
        Vector2[] closestPoints = null;

        for (int i = 0; i < pathCreators.Length; i++)
        {
            if (pathCreators[i].isActiveAndEnabled)
            {
                int anIndex = 0;
                Vector2[] iteratedSplinesPoints = pathCreators[i].path.GetMyEvenlySpacedPoints();
                Vector2 closestPointInIteratedSpline = GetClosestPointInSpline(aPlayerPosition, iteratedSplinesPoints, ref anIndex);

                if (Vector2.Distance(aPlayerPosition, closestPointInIteratedSpline) < Vector2.Distance(aPlayerPosition, closestPoint))
                {
                    closestPoint = closestPointInIteratedSpline;
                    closestPoints = iteratedSplinesPoints;
                    closestIndex = anIndex;
                }
            }
        }

        Vector2[] groundPoints = new Vector2[2];

        if (closestIndex >= closestPoints.Length - 1)
        {
            groundPoints[0] = closestPoints[closestIndex - 1];
            groundPoints[1] = closestPoints[closestIndex];
        }
        else
        {
            groundPoints[0] = closestPoints[closestIndex];
            groundPoints[1] = closestPoints[closestIndex + 1];
        }

        return groundPoints[1] - groundPoints[0];
    }

    private void SetSplineActivate()
    {
        for (int i = 0; i < pathCreators.Length; i++)
        {
            Vector2 first = pathCreators[i].path.GetFirstPoint();
            Vector2 last = pathCreators[i].path.GetLastPoint();

            if (myPlayer.transform.position.x > last.x)
            {
                if ((myPlayer.transform.position.x - last.x) > mySplineOffset)
                {
                    mySplines[i].SetActive(false);
                }
                else
                {
                    mySplines[i].SetActive(true);
                }
            }

            if (myPlayer.transform.position.x < first.x)
            {
                if ((first.x - myPlayer.transform.position.x) > mySplineOffset)
                {
                    mySplines[i].SetActive(false);
                }
                else
                {
                    mySplines[i].SetActive(true);
                }
            }
        }
    }
}