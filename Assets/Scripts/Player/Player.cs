﻿using UnityEngine;

//[RequireComponent(typeof(PlayerSpline))]
//[RequireComponent(typeof(PlayerJump))]
//[RequireComponent(typeof(PlayerAir))]
//[RequireComponent(typeof(SplineManager))]
//[RequireComponent(typeof(PlayerInput))]
//[RequireComponent(typeof(PlayerCollision))]
//[RequireComponent(typeof(PlayerDeath))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private SplineManager mySplineManager = null;
    [SerializeField]
    private Camera myCamera = null;
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
    [SerializeField]
    private float myFlipRotationSpeed = 100f;
    [SerializeField]
    private float myShakeDurationRocks = 15f;
    [SerializeField]
    private float myShakeMagnitudeRocks = 10f;
    [SerializeField]
    private float myShakeDurationSplines = 10f;
    [SerializeField]
    private float myShakeMagnitudeSplines = 5f;

    private PlayerSpline myPlayerSpline;
    private PlayerJump myPlayerJump;
    private PlayerAir myPlayerAir;
    private PlayerInput myPlayerInput;
    private PlayerCollision myPlayerCollision;
    private PlayerDeath myPlayerDeath;
    private CameraShake myCameraShake;

    private bool myGrounded = false;
    private bool myTooCloseToOldSpline = false;
    private bool myIsJumping;
    private bool myHasCollided = false;
    private Vector2[] myCurrentPoints;
    private Vector2[] myOldPoints;
    private Vector2 myAirMovement = new Vector2(1, 0);
    private int myPointsIndex = -1;
    private float mySplineT = -1;
    private float myCurrentSpeed;
    Quaternion myOriginalRotation;

    private void Start()
    {
        myPlayerSpline = GetComponent<PlayerSpline>();
        myPlayerJump = GetComponent<PlayerJump>();
        myPlayerAir = GetComponentInChildren<PlayerAir>();
        myPlayerInput = GetComponent<PlayerInput>();
        myPlayerDeath = GetComponent<PlayerDeath>();
        myPlayerCollision = GetComponentInChildren<PlayerCollision>();
        myCameraShake = myCamera.GetComponent<CameraShake>();
        myCurrentSpeed = myBaseSpeed;
        myOriginalRotation = transform.rotation;

        if (mySplineManager == null)
        {
            Debug.LogError(this + " has no splineManager!");
            return;
        }
        if (myCamera == null)
        {
            Debug.LogError(this + " has no camera!");
        }
    }

    private void Update()
    {
        myIsJumping = myPlayerInput.IsJumping();
        myHasCollided = myPlayerCollision.HasCollided();

        if (myHasCollided)
        {
            myCameraShake.TriggerShake(myShakeDurationRocks, myShakeMagnitudeRocks);
            myPlayerDeath.Die();
            ResetSpline();
        }

        if (myGrounded)
        {

            if (myIsJumping)
            {
                myPlayerJump.Jump(myCurrentPoints, myPointsIndex, myJumpForce, myCurrentSpeed, ref myAirMovement);
                ResetSpline();
                return;
            }

            if (!myPlayerSpline.SplineMovement(myCurrentPoints, myCurrentSpeed, ref myPointsIndex, ref mySplineT))
            {
                myPlayerSpline.ReleaseSpline(myCurrentPoints, myCurrentSpeed, ref myAirMovement);
                ResetSpline();
            }
            return;
        }
        else
        {
            AirMovement(myGravity, ref myAirMovement);
        }

        if (myIsJumping)
        {
            myPlayerAir.Backflip(myFlipRotationSpeed);
        }

        AirRotation(myRotationResetSpeed, myOriginalRotation);

        if (myAirMovement.y < 0)
        {
            if (myPlayerSpline.AttemptToCatchSpline(mySplineManager, myReach, ref myTooCloseToOldSpline, ref myPointsIndex, ref myCurrentPoints, ref myOldPoints))
            {
                myCameraShake.TriggerShake(myShakeDurationSplines, myShakeMagnitudeSplines);
                myGrounded = true;
                mySplineT = 0;
            }
        }
    }

    private void ResetSpline()
    {
        myTooCloseToOldSpline = true;
        myGrounded = false;
        myPointsIndex = -1;
        myOldPoints = myCurrentPoints;
        myCurrentPoints = null;
        mySplineT = 0;
    }

    public void AirMovement(float aGravity, ref Vector2 aAirMovement)
    {
        Vector2 currentMove = Time.deltaTime * aAirMovement;
        transform.position = new Vector3(transform.position.x + currentMove.x, transform.position.y + currentMove.y, transform.position.z);
        aAirMovement = new Vector2(aAirMovement.x, aAirMovement.y - (aGravity * Time.deltaTime));
    }

    public void AirRotation(float aRotationResetSpeed, Quaternion anOriginalRotation)
    {
        //float newXRot = 45f;
        //float newYRot = 90f;

        //Quaternion newRot = Quaternion.Euler(newXRot, newYRot, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, anOriginalRotation, Time.deltaTime * aRotationResetSpeed);
    }
}