using UnityEngine;

[RequireComponent(typeof(PlayerSpline))]
[RequireComponent(typeof(PlayerJump))]
[RequireComponent(typeof(PlayerAir))]
[RequireComponent(typeof(SplineManager))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerCollision))]
[RequireComponent(typeof(PlayerDeath))]
public class Player : MonoBehaviour
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
    [SerializeField]
    private float myFlipRotationSpeed = 100f;

    private PlayerSpline myPlayerSpline;
    private PlayerJump myPlayerJump;
    private PlayerAir myPlayerAir;
    private PlayerInput myPlayerInput;
    private PlayerCollision myPlayerCollision;
    private PlayerDeath myPlayerDeath;

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
    private Quaternion myOriginalRotation;

    private void Start()
    {
        myPlayerSpline = GetComponent<PlayerSpline>();
        myPlayerJump = GetComponent<PlayerJump>();
        myPlayerAir = GetComponent<PlayerAir>();
        myPlayerInput = GetComponent<PlayerInput>();
        myPlayerDeath = GetComponent<PlayerDeath>();
        myPlayerCollision = GetComponentInChildren<PlayerCollision>();

        myCurrentSpeed = myBaseSpeed;
        myOriginalRotation = transform.rotation;

        if (mySplineManager == null)
        {
            Debug.LogError(this + " has no splineManager!");
            return;
        }
    }

    private void Update()
    {
        myIsJumping = myPlayerInput.IsJumping();
        myHasCollided = myPlayerCollision.HasCollided();

        if (myHasCollided)
        {
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
        
        myPlayerAir.AirMovement(myGravity, ref myAirMovement);

        if (myIsJumping)
        {
            myPlayerAir.Backflip(myFlipRotationSpeed);
        }
        else
        {
            myPlayerAir.ResetRotation(myOriginalRotation, myRotationResetSpeed);
        }

        if (myAirMovement.y < 0)
        {
            if (myPlayerSpline.AttemptToCatchSpline(mySplineManager, myReach, ref myTooCloseToOldSpline, ref myPointsIndex, ref myCurrentPoints, ref myOldPoints))
            {
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
}