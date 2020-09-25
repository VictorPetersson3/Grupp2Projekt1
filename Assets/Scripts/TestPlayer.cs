using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    [SerializeField]
    private SplineManager mySplineManager = null;
    [SerializeField]
    private float myReach = 0.25f;
    [SerializeField]
    private float myAirSpeed = 1f;
    [SerializeField]
    private float myGroundSpeed = 1f;

    private bool myNextIsNull = false;
    private bool myGrounded = false;
    private Vector2[] myPoints;
    private Vector2[] myOldPoints;
    private int myPointsIndex = -1;
    private float mySplineT = -1;

    void Update()
    {
        if (mySplineManager == null)
        {
            Debug.LogError(this + " has no splineManager!");
            return;
        }

        if (myGrounded)
        {
            Grounded();
            return;
        }

        Air();
    }

    void Grounded()
    {
        float currentMove = Time.deltaTime * myGroundSpeed;
        mySplineT += currentMove;

        if (mySplineT > 1f)
        {
            if (myNextIsNull)
            {
                DropSpline();
                return;
            }

            transform.position = myPoints[myPointsIndex + 1];
            mySplineT -= 1f;
            myPointsIndex++;
        }

        if (myPointsIndex + 1 >= myPoints.Length)
        {
            myNextIsNull = true;
        }
        else
        {
            transform.position = Vector2.Lerp(myPoints[myPointsIndex], myPoints[myPointsIndex + 1], mySplineT);
        }
    }

    void Air()
    {
        float currentMove = Time.deltaTime * myAirSpeed;
        transform.position = new Vector3(transform.position.x + currentMove, transform.position.y - currentMove, transform.position.z);
        Vector2 closestPoint = mySplineManager.GetClosestPoint(transform.position, ref myPointsIndex, ref myPoints);
        
        if (Vector2.Distance(transform.position, closestPoint) <= myReach)
        {
            if ((myOldPoints != null && myPoints != null) && myOldPoints.Length == myPoints.Length)
            {
                Debug.Log("Attempting to grab spline, but Same amount of spheres as old spline.");
                bool sameSpline = true;

                for (int i = 0; i < myOldPoints.Length; i++)
                {
                    if (myPoints[i] != myOldPoints[i])
                    {
                        sameSpline = false;
                    }
                }

                if (sameSpline)
                {
                    Debug.Log("Tried grabbing same spline. ABORT!");
                    return;
                }

                Debug.Log("Wasn't the same spline. LETS GO!");
            }

            transform.position = closestPoint;
            myGrounded = true;
            mySplineT = 0;
            //Debug.Log("Closest point: " + closestPoint);
            Debug.Log("Index: " + myPointsIndex);
            Debug.Log("Index vec2: " + myPoints[myPointsIndex]);
            //Debug.Log("Grabbing Points: " + myPoints);
        }
    }

    void DropSpline()
    {
        //Debug.Log("Dropping spline: " + myPoints);
        myNextIsNull = false;
        myGrounded = false;
        myPointsIndex = -1;
        myOldPoints = myPoints;
        myPoints = null;
        mySplineT = 0;
    }
}