﻿using Boo.Lang.Environments;
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
    [SerializeField]
    private float myRotationResetSpeed = 5f;

    private bool myGrounded = false;
    private bool myTooCloseToOldSpline = false;
    private Vector2[] myCurrentPoints;
    private Vector2[] myOldPoints;
    private Vector2 myAirMovement = new Vector2(1, 0);
    private int myPointsIndex = -1;
    private float mySplineT = -1;
    private float myCurrentSpeed; 
    private Quaternion myOriginalRotation;

    private void Start()
    {
        myCurrentSpeed = myBaseSpeed;

        myOriginalRotation = transform.rotation;

        if (mySplineManager == null)
        {
            Debug.LogError(this + " has no splineManager!");
            return;
        }
    }

    void Update()
    {
        if (myGrounded)
        {
            if (Input.GetAxisRaw("Jump") != 0)
            {
                Jump();
                return;
            }

            SplineMovement();
            return;
        }
        
        ResetRotation();
        Air();
    }

    void Jump()
    {
        if (myPointsIndex == 0)
        {
            myAirMovement = myCurrentPoints[0] - myCurrentPoints[1];
        }
        else
        {
            myAirMovement = myCurrentPoints[myPointsIndex] - myCurrentPoints[myPointsIndex - 1];
        }
        
        myAirMovement = myAirMovement.normalized * myBaseSpeed * transform.localScale.x;
        myAirMovement = new Vector2(myAirMovement.x, myAirMovement.y + myJumpForce);
        DropSpline();
    }

    void SplineMovement()
    {
        transform.LookAt(myCurrentPoints[myPointsIndex + 1]);

        float currentMove = Time.deltaTime * myCurrentSpeed;
        mySplineT += currentMove;

        if (mySplineT >= 1f)
        {
            transform.position = myCurrentPoints[myPointsIndex + 1];
            mySplineT -= 1f;
            myPointsIndex++;
        }

        if (myPointsIndex + 1 >= myCurrentPoints.Length)
        {
            myAirMovement = myCurrentPoints[myCurrentPoints.Length - 1] - myCurrentPoints[myCurrentPoints.Length - 2];
            myAirMovement = myAirMovement.normalized * myBaseSpeed / 10;
            DropSpline();            
        }
        else
        {
            transform.position = Vector2.Lerp(myCurrentPoints[myPointsIndex], myCurrentPoints[myPointsIndex + 1], mySplineT);
        }
    }

    void Air()
    {
        Vector2 currentMove = Time.deltaTime * myAirMovement;
        transform.position = new Vector3(transform.position.x + currentMove.x, transform.position.y + currentMove.y, transform.position.z);
        
        if (myAirMovement.y < 0)
        {
            AttemptToCatchSpline();
        }       

        myAirMovement = new Vector2(myAirMovement.x, myAirMovement.y - (myGravity * Time.deltaTime));
    }

    void AttemptToCatchSpline()
    {
        Vector2 closestPoint = mySplineManager.GetClosestPoint(transform.position, ref myPointsIndex, ref myCurrentPoints);

        if (Vector2.Distance(transform.position, closestPoint) <= myReach)
        {
            if (myTooCloseToOldSpline && IsOldSpline())
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
        myOldPoints = myCurrentPoints;
        myCurrentPoints = null;
        mySplineT = 0;
    }

    private bool IsOldSpline()
    {
        bool sameSpline = false;

        if ((myOldPoints != null && myCurrentPoints != null) && myOldPoints.Length == myCurrentPoints.Length)
        {
            sameSpline = true;

            for (int i = 0; i < myOldPoints.Length; i++)
            {
                if (myCurrentPoints[i] != myOldPoints[i])
                {
                    return false;
                }
            }
        }

        return sameSpline;
    }

    private void ResetRotation()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, myOriginalRotation, Time.deltaTime * myRotationResetSpeed);
    }
}