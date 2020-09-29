using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerSpline : MonoBehaviour
{
    [SerializeField]
    private float myGroundedRotationSpeed = 10f;

    private float myOldY = 0f;
    private float myCurrentY = 0f;
    private bool myFirstCheck = true;

    public bool SplineMovement(Vector2[] aCurrentPoints, float aCurrentSpeed, ref int aPointsIndex, ref float aSplineT, float aGravity)
    {
        if (aCurrentPoints.Length > aPointsIndex + 1)
        {
            Vector3 lookPos = new Vector3(aCurrentPoints[aPointsIndex + 1].x, aCurrentPoints[aPointsIndex + 1].y, 0) - transform.position;
            lookPos.z = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * myGroundedRotationSpeed);
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
            ResetDeltaVariables();
            return false;
        }

        transform.position = Vector2.Lerp(aCurrentPoints[aPointsIndex], aCurrentPoints[aPointsIndex + 1], aSplineT);

        if (DeltaYGreaterThanYGravity(aGravity))
        {
            ResetDeltaVariables();
            return false;
        }

        return true;
    }

    private void ResetDeltaVariables()
    {
        myOldY = 0f;
        myCurrentY = 0f;
        myFirstCheck = true;
    }

    private bool DeltaYGreaterThanYGravity(float aGravity)
    {
        myOldY = myCurrentY;
        myCurrentY = transform.position.y;

        if (myFirstCheck)
        {
            Debug.Log("FIRST CHECK THING");
            myFirstCheck = false;
            return false;
        }
        
        float deltaY = myCurrentY - myOldY;
        float deltaGrav = -aGravity * Time.deltaTime;

        if (myOldY + deltaY < (myOldY + deltaGrav))
        {
            Debug.Log("Old Y: " + myOldY);
            Debug.Log("Current Y: " + myCurrentY);
            Debug.Log("Delta Y:" + deltaY);
            Debug.Log("Delta Gravity:" + deltaGrav);

            ResetDeltaVariables();            
            return true;
        }

        return false;
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
