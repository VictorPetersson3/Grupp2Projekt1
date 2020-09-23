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
    private float mySpeed = 1f;

    private bool myGrounded = false;
    private Vector2[] myPoints;
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
        float currentMove = Time.deltaTime * mySpeed;
        mySplineT += currentMove;

        if (mySplineT > 1f)
        {
            transform.position = myPoints[myPointsIndex + 1];

            if (myPointsIndex + 1 >= myPoints.Length)
            {
                DropSpline();
                return;
            }

            mySplineT -= 1f;
            myPointsIndex++;
        }

        transform.position = Vector2.Lerp(myPoints[myPointsIndex], myPoints[myPointsIndex + 1], mySplineT);
    }

    void Air()
    {
        float currentMove = Time.deltaTime * mySpeed;
        transform.position = new Vector3(transform.position.x + currentMove, transform.position.y - currentMove, transform.position.z);
        Vector2 closestPoint = mySplineManager.GetClosestPoint(transform.position, ref myPointsIndex, ref myPoints);
        
        if (Vector2.Distance(transform.position, closestPoint) <= myReach)
        {
            transform.position = closestPoint;
            myGrounded = true;
            mySplineT = 0;
            Debug.Log("Closest point: " + closestPoint);
            Debug.Log("Index: " + myPointsIndex);
            Debug.Log("Points: " + myPoints);
        }
    }

    void DropSpline()
    {
        myGrounded = false;
        myPointsIndex = -1;
        myPoints = null;
        mySplineT = 0;
    }
}