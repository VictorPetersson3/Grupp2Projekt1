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
    private float myGravity = 1f;
    [SerializeField]
    private float myBaseSpeed = 1f;
    [SerializeField]
    private float myJumpForce = 10f;

    private bool myGrounded = false;
    private bool myTooCloseToOldSpline = false;
    private Vector2[] myPoints;
    private Vector2[] myOldPoints;
    private Vector2 myAirMovement = new Vector2(1, 0);
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
            if (Input.GetAxisRaw("Jump") != 0)
            {
                myAirMovement = myPoints[myPointsIndex] - myPoints[myPointsIndex - 1];
                myAirMovement = myAirMovement.normalized;
                myAirMovement = new Vector2(myAirMovement.x, myAirMovement.y + myJumpForce);
                DropSpline();
                return;
            }

            Grounded();
            return;
        }

        Air();
    }

    void Grounded()
    {
        float currentMove = Time.deltaTime * myBaseSpeed;
        mySplineT += currentMove;

        if (mySplineT >= 1f)
        {
            transform.position = myPoints[myPointsIndex + 1];
            mySplineT -= 1f;
            myPointsIndex++;
        }

        if (myPointsIndex + 1 >= myPoints.Length)
        {
            myAirMovement = myPoints[myPoints.Length - 1] - myPoints[myPoints.Length - 2];
            myAirMovement = myAirMovement.normalized;
            DropSpline();            
        }
        else
        {
            transform.position = Vector2.Lerp(myPoints[myPointsIndex], myPoints[myPointsIndex + 1], mySplineT);
        }
    }

    void Air()
    {
        Vector2 currentMove = Time.deltaTime * myAirMovement;
        transform.position = new Vector3(transform.position.x + currentMove.x, transform.position.y + currentMove.y, transform.position.z);
        myAirMovement = new Vector2(myAirMovement.x, myAirMovement.y - (myGravity * Time.deltaTime));
        Vector2 closestPoint = mySplineManager.GetClosestPoint(transform.position, ref myPointsIndex, ref myPoints);
        
        if (Vector2.Distance(transform.position, closestPoint) <= myReach)
        {
            if (IsOldSpline() && myTooCloseToOldSpline)
            {
                return;
            }

            transform.position = closestPoint;
            myGrounded = true;
            mySplineT = 0;
            return;
        }

        if (Vector2.Distance(transform.position, closestPoint) > myReach && IsOldSpline())
        {
            myTooCloseToOldSpline = false;
        }
    }

    void DropSpline()
    {
        myTooCloseToOldSpline = true;
        myGrounded = false;
        myPointsIndex = -1;
        myOldPoints = myPoints;
        myPoints = null;
        mySplineT = 0;
    }

    private bool IsOldSpline()
    {
        bool sameSpline = false;

        if ((myOldPoints != null && myPoints != null) && myOldPoints.Length == myPoints.Length)
        {
            sameSpline = true;

            for (int i = 0; i < myOldPoints.Length; i++)
            {
                if (myPoints[i] != myOldPoints[i])
                {
                    return false;
                }
            }
        }

        return sameSpline;
    }
}