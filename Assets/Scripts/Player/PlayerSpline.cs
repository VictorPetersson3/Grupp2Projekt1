using UnityEngine;

//[RequireComponent(typeof(Player))]
public class PlayerSpline : MonoBehaviour
{
    [SerializeField]
    private float myGroundedRotationSpeed = 1f;

    public bool SplineMovement(Vector2[] aCurrentPoints, float aCurrentSpeed, ref int aPointsIndex, ref float aSplineT)
    {
        if (aCurrentPoints.Length > aPointsIndex + 1)
        {
            Vector3 lookPos = new Vector3(aCurrentPoints[aPointsIndex + 1].x, aCurrentPoints[aPointsIndex + 1].y, 0) - transform.position;
            lookPos.z = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * myGroundedRotationSpeed);
            transform.GetChild(0).rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * myGroundedRotationSpeed);
        }

        float currentMove = Time.deltaTime * aCurrentSpeed;
        aSplineT += currentMove;

        if (aSplineT >= 1f)
        {
            transform.position = aCurrentPoints[aPointsIndex + 1];
            aSplineT -= 1f;
            aPointsIndex++;
        }

        if (aPointsIndex + 1 >= aCurrentPoints.Length)
        {
            return false;
        }
        else
        {
            transform.position = Vector2.Lerp(aCurrentPoints[aPointsIndex], aCurrentPoints[aPointsIndex + 1], aSplineT);
        }

        return true;
    }

    public void ReleaseSpline(Vector2[] aCurrentPoints, float aBaseSpeed, ref Vector2 aAirMovement)
    {
        aAirMovement = aCurrentPoints[aCurrentPoints.Length - 1] - aCurrentPoints[aCurrentPoints.Length - 2];
        aAirMovement = aAirMovement.normalized * aBaseSpeed / 10;
    }

    private bool IsOldSpline(Vector2[] aOldPoints, Vector2[] aCurrentPoints)
    {
        bool sameSpline = false;

        if ((aOldPoints != null && aCurrentPoints != null) && aOldPoints.Length == aCurrentPoints.Length)
        {
            sameSpline = true;

            for (int i = 0; i < aOldPoints.Length; i++)
            {
                if (aCurrentPoints[i] != aOldPoints[i])
                {
                    return false;
                }
            }
        }
        return sameSpline;
    }

    public bool AttemptToCatchSpline(SplineManager aSplineManager, float aReach, ref bool aTooCloseToOldSpline, ref int aPointsIndex, ref Vector2[] aCurrentPoints, ref Vector2[] aOldPoints)
    {
        Vector2 closestPoint = aSplineManager.GetClosestPoint(transform.position, ref aPointsIndex, ref aCurrentPoints);

        if (Vector2.Distance(transform.position, closestPoint) <= aReach)
        {
            if (aTooCloseToOldSpline && IsOldSpline(aOldPoints, aCurrentPoints))
            {
                return false;
            }

            transform.position = closestPoint;
            return true;
        }

        if (Vector2.Distance(transform.position, closestPoint) > aReach && IsOldSpline(aOldPoints, aCurrentPoints))
        {
            aTooCloseToOldSpline = false;
        }
        return false;
    }
}
