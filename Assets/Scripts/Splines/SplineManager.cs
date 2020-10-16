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
    }

    private void Update()
    {
        SetSplineActivate();
    }

    public Vector2 GetClosestGround(Vector2 aPlayerPos)
    {
        Vector2 closestPoint = Vector2.negativeInfinity;
        Vector2[] points = null;
        int closestIndex = 0;

        for (int i = 0; i < pathCreators.Length; i++)
        {
            if (pathCreators[i].isActiveAndEnabled)
            {
                int closestSplineIndex = 0;
                Vector2[] iteratedSplinesPoints = pathCreators[i].path.CalculateEvenlySpacedPoints();
                Vector2 closestPointInSpline = Vector2.negativeInfinity;

                for (int j = 0; j < iteratedSplinesPoints.Length; j++)
                {
                    if (Vector2.Distance(aPlayerPos, iteratedSplinesPoints[j]) < Vector2.Distance(aPlayerPos, closestPointInSpline))
                    {
                        closestPointInSpline = iteratedSplinesPoints[j];
                        closestSplineIndex = j;
                    }
                }

                if (Vector2.Distance(aPlayerPos, closestPointInSpline) < Vector2.Distance(aPlayerPos, closestPoint))
                {
                    closestPoint = closestPointInSpline;
                    points = iteratedSplinesPoints;
                    closestIndex = closestSplineIndex;
                }
            }
        }

        Vector2[] groundPoints = new Vector2[2];

        if (closestIndex >= points.Length - 1)
        {
            groundPoints[0] = points[closestIndex - 1];
            groundPoints[1] = points[closestIndex];
        }
        else
        {
            groundPoints[0] = points[closestIndex];
            groundPoints[1] = points[closestIndex + 1];
        }
        return groundPoints[1] - groundPoints[0];
    }

    public Vector2 GetClosestPoint(Vector2 aPlayerPos, ref int aPointsIndex, ref Vector2[] aPoints, ref Vector2 aBoost)
    {
        Vector2 closestPoint = Vector2.negativeInfinity;
        int closestIndex = 0;

        for (int i = 0; i < pathCreators.Length; i++)
        {
            if (pathCreators[i].isActiveAndEnabled)
            {
                Vector2[] iteratedSplinesPoints = pathCreators[i].path.CalculateEvenlySpacedPoints();
                Vector2 closestPointInIteratedSpline = GetClosestPointInSpline(aPlayerPos, iteratedSplinesPoints, ref aPointsIndex);

                if (Vector2.Distance(aPlayerPos, closestPointInIteratedSpline) < Vector2.Distance(aPlayerPos, closestPoint))
                {
                    closestPoint = closestPointInIteratedSpline;
                    aPoints = iteratedSplinesPoints;
                    closestIndex = aPointsIndex;
                    if (pathCreators[i].HasBoost())
                    {
                        aBoost = pathCreators[i].GetBoost();
                    }
                }
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