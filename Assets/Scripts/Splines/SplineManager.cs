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

    public Vector2 GetClosestPoint(Vector2 aPlayerPosition, ref int aPointsIndex, ref Vector2[] aPoints, ref Vector2 aBoost)
    {
        Vector2 closestPoint = Vector2.negativeInfinity;
        int closestIndex = 0;

        for (int i = 0; i < pathCreators.Length; i++)
        {
            if (pathCreators[i].isActiveAndEnabled)
            {
                Vector2[] iteratedSplinesPoints = pathCreators[i].path.GetMyEvenlySpacedPoints();
                Vector2 closestPointInIteratedSpline = GetClosestPointInSpline(aPlayerPosition, iteratedSplinesPoints, ref aPointsIndex);

                if (Vector2.Distance(aPlayerPosition, closestPointInIteratedSpline) < Vector2.Distance(aPlayerPosition, closestPoint))
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