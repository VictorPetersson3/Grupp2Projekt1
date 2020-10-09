using UnityEngine;

public class PlayerSpline : MonoBehaviour
{
    [SerializeField]
    private float myGroundedRotationSpeed = 10f;
    [SerializeField]
    private float myAngleBuffer = -0.1f;
    [SerializeField]
    private float myGroundBoostStrength = 50f;
    [SerializeField]
    private float mySlopeAcceleration = 1f;
    [SerializeField]
    private float mySlopeDeceleration = 1f;
    [SerializeField]
    private float myMaxUnmodifiedSpeed = 100f;
    [SerializeField]
    private float myMinUnmodifiedSpeed = 10f;
    [Header("Trick Boost")]
    [SerializeField]
    private float myTrickBoostStrength = 100f;
    [SerializeField]
    private float myTrickBoostMax = 100f;

    private float myOldAngle = 90;
    private float myCurrentAngle = 90;
    private bool myFirstCheck = true;

    const int myFlatAngle = 90;

    public bool SplineMovement(Vector2[] someCurrentPoints, ref float anUnmodifiedSpeed, ref int aPointsIndex, ref float aSplineT, float aGravity, Vector2 aBoostVector, ref float aTrickBoost, ref float aTotalSpeed)
    {
        float accMultiplier = mySlopeAcceleration;

        if (myCurrentAngle > 90)
        {
            accMultiplier = mySlopeDeceleration;
        }
        
        anUnmodifiedSpeed -= (myCurrentAngle - myFlatAngle) * accMultiplier;
        anUnmodifiedSpeed = Mathf.Clamp(anUnmodifiedSpeed, myMinUnmodifiedSpeed, myMaxUnmodifiedSpeed);

        aTotalSpeed = anUnmodifiedSpeed;

        if (IndexWithinBoost(aPointsIndex, aBoostVector))
        {
            aTotalSpeed += myGroundBoostStrength;
        }

        if (aTrickBoost > 0)
        {
            float currentTrickBoost = aTrickBoost * myTrickBoostStrength;
            if (currentTrickBoost > myTrickBoostMax)
            {
                currentTrickBoost = myTrickBoostMax;
            }
            aTotalSpeed += currentTrickBoost;
            aTrickBoost -= Time.deltaTime;
        }
        else if (aTrickBoost < 0)
        {
            aTrickBoost = 0;
        }
        aSplineT += aTotalSpeed * Time.deltaTime;

        while (aSplineT >= 1f)
        {
            transform.position = someCurrentPoints[aPointsIndex + 1];

            aSplineT -= 1f;
            aPointsIndex++;
            if (aPointsIndex + 1 < someCurrentPoints.Length)
            {
                if (!UpdateAngles(someCurrentPoints, aPointsIndex, aTotalSpeed, aGravity))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        
        LookAtNextPoint(someCurrentPoints, aPointsIndex);

        if (myFirstCheck && (aPointsIndex + 1 < someCurrentPoints.Length))
        {
            myOldAngle = myCurrentAngle;
            myCurrentAngle = GetAngle(someCurrentPoints[aPointsIndex], someCurrentPoints[aPointsIndex + 1]);
        }

        transform.position = Vector2.Lerp(someCurrentPoints[aPointsIndex], someCurrentPoints[aPointsIndex + 1], aSplineT);

        return true;
    }

    private void LookAtNextPoint(Vector2[] someCurrentPoints, int aPointsIndex)
    {
        if (someCurrentPoints.Length > aPointsIndex + 1)
        {
            Vector3 lookPos = new Vector3(someCurrentPoints[aPointsIndex + 1].x, someCurrentPoints[aPointsIndex + 1].y, 0) - transform.position;
            lookPos.z = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * myGroundedRotationSpeed);
            //transform.GetChild(0).rotation = Quaternion.Slerp(transform.GetChild(0).rotation, rotation, Time.deltaTime * myGroundedRotationSpeed);
        }
    }

    private bool UpdateAngles(Vector2[] someCurrentPoints, int aPointsIndex, float aTotalSpeed, float aGravity)
    {
        myOldAngle = myCurrentAngle;
        myCurrentAngle = GetAngle(someCurrentPoints[aPointsIndex], someCurrentPoints[aPointsIndex + 1]);

        if (myFirstCheck)
        {
            myFirstCheck = false;
        }

        if (SplineTooSteep(someCurrentPoints, aPointsIndex, aTotalSpeed, aGravity))
        {
            return false;
        }

        return true;
    }

    private bool SplineTooSteep(Vector2[] someCurrentPoints, int aPointsIndex, float aTotalSpeed, float aGravity)
    {
        if (myOldAngle < myCurrentAngle)
        {
            return false;
        }

        Vector2 splineMovement = (someCurrentPoints[aPointsIndex] - someCurrentPoints[aPointsIndex + 1]) * aTotalSpeed;
        aGravity *= -1;
        float delta = splineMovement.y - aGravity;

        if ((splineMovement.y < aGravity) && (delta < myAngleBuffer))
        {
            return true;
        }

        return false;
    }

    public void ResetAngleVariables()
    {
        myOldAngle = 0f;
        myCurrentAngle = 0f;
        myFirstCheck = true;
    }

    //Down=0, Right=90, Up=180, Left=270
    public float GetAngle(Vector2 aPosition, Vector2 anotherPosition)
    {
        Vector2 delta = anotherPosition - aPosition;
        float angleRadians = Mathf.Atan2(delta.y, delta.x);
        float angleDegrees = angleRadians * Mathf.Rad2Deg;
        angleDegrees += 90;

        if (angleDegrees < 0)
        {
            angleDegrees += 360;
        }
        else if (angleDegrees > 360)
        {
            angleDegrees -= 360;
        }
        
        return angleDegrees;
    }

    public void ReleaseSpline(Vector2[] somePoints, float aTotalSpeed, ref Vector2 aAirMovement, int aPointsIndex)
    {
        aAirMovement = somePoints[aPointsIndex] - somePoints[aPointsIndex - 1];
        aAirMovement = aAirMovement.normalized * aTotalSpeed / 10;
    }

    private bool IsOldSpline(Vector2[] someOldPoints, Vector2[] someCurrentPoints)
    {
        bool sameSpline = false;

        if ((someOldPoints != null && someCurrentPoints != null) && someOldPoints.Length == someCurrentPoints.Length)
        {
            sameSpline = true;

            for (int i = 0; i < someOldPoints.Length; i++)
            {
                if (someCurrentPoints[i] != someOldPoints[i])
                {
                    return false;
                }
            }
        }
        return sameSpline;
    }

    public bool AttemptToCatchSpline(SplineManager aSplineManager, float aReach, ref bool aTooCloseToOldSpline, ref int aPointsIndex, ref Vector2[] someCurrentPoints, ref Vector2[] someOldPoints, ref Vector2 aBoost)
    {
        Vector2 closestPoint = aSplineManager.GetClosestPoint(transform.position, ref aPointsIndex, ref someCurrentPoints, ref aBoost);

        if (Vector2.Distance(transform.position, closestPoint) <= aReach)
        {
            if (aTooCloseToOldSpline && IsOldSpline(someOldPoints, someCurrentPoints))
            {
                return false;
            }

            transform.position = closestPoint;
            return true;
        }

        if (Vector2.Distance(transform.position, closestPoint) > aReach && IsOldSpline(someOldPoints, someCurrentPoints))
        {
            aTooCloseToOldSpline = false;
        }
        return false;
    }

    private bool IndexWithinBoost(int anIndex, Vector2 aBoost)
    {
        if (aBoost == Vector2.zero)
        {
            return false;
        }

        if (anIndex >= aBoost.x && anIndex <= aBoost.y)
        {
            return true;
        }

        return false;
    }
}
