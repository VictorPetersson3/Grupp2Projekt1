using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameManager myGameManager = null;
    [SerializeField]
    private SplineManager mySplineManager = null;
    [SerializeField]
    private CameraFollow myCameraFollow = null;
    [SerializeField]
    private float myReach = 0.25f;
    [SerializeField]
    private float myGravity = 1f;
    [SerializeField]
    private float myStartSpeed = 40f;
    [Header("Camera Shake")]
    [SerializeField]
    private float myShakeDurationRocks = 15f;
    [SerializeField]
    private float myShakeMagnitudeRocks = 10f;
    [SerializeField]
    private float myShakeDurationSplines = 10f;
    [SerializeField]
    private float myShakeMagnitudeSplines = 5f;

    private SandParticleManager mySandParticleManager;
    private PlayerSpline myPlayerSpline;
    private PlayerJump myPlayerJump;
    private PlayerAir myPlayerAir;
    private PlayerInput myPlayerInput;
    private PlayerCollision myPlayerCollision;
    private PlayerBobbing myPlayerBobbing;
    private PlayerBackflip myPlayerBackflip;
    private CameraShake myCameraShake = null;

    private bool myGrounded = false;
    private bool myTooCloseToOldSpline = false;
    private bool myIsJumping;
    private int myScore = 0;
    private CollisionData myCollisionData;
    private Vector2[] myCurrentPoints;
    private Vector2[] myOldPoints;
    private Vector2 myAirMovement = Vector2.right;
    private Vector2 myBoostVector = Vector2.zero;
    private int myPointsIndex = -1;
    private float mySplineT = -1;
    private float myUnmodifiedSpeed = 0f;
    private float myTrickBoost = 0f;
    private float myTotalSpeed = 0f;

    // Particles
    private const int myGroundParticleAmount = 8;

    // Power-Ups
    private bool myMagnet = false;

    private void Start()
    {
        myPlayerSpline = GetComponent<PlayerSpline>();
        myPlayerJump = GetComponent<PlayerJump>();
        myPlayerAir = GetComponent<PlayerAir>();
        myPlayerInput = GetComponent<PlayerInput>();
        myPlayerCollision = GetComponentInChildren<PlayerCollision>();
        myPlayerBobbing = GetComponent<PlayerBobbing>();
        myPlayerBackflip = GetComponentInChildren<PlayerBackflip>();
        mySandParticleManager = GetComponentInChildren<SandParticleManager>();
        myCameraShake = myCameraFollow.gameObject.GetComponentInChildren<CameraShake>();

        myUnmodifiedSpeed = myStartSpeed;

        if (mySplineManager == null)
        {
            Debug.LogError(this + " has no splineManager!");
            return;
        }
        if (myCameraFollow == null)
        {
            Debug.LogError(this + " has no camera follow!");
        }

        myCameraFollow.SetPlayerSpeeds(myPlayerSpline.GetMinMaxSpeeds().x, myPlayerSpline.GetMinMaxSpeeds().y);
    }

    private void Update()
    {
        if (myPlayerInput.IsQuitting())
        {
            Application.Quit();
        }

        if (myPlayerInput.IsResetting())
        {
            Crash();
            return;
        }

        Collision();

        myIsJumping = myPlayerInput.IsJumping();
        if (myGrounded)
        {
            Grounded();
            return;
        }

        Air();
    }

    private void ResetSpline()
    {
        myTooCloseToOldSpline = true;
        myGrounded = false;
        myPointsIndex = -1;
        myOldPoints = myCurrentPoints;
        myCurrentPoints = null;
        mySplineT = 0;
        myPlayerSpline.ResetAngleVariables();
        myBoostVector = Vector2.zero;
    }

    private void Grounded()
    {
        myCameraFollow.UpdateYOffset(0);
        myPlayerBobbing.Bob();
        mySandParticleManager.CreateSandParticle(myGroundParticleAmount);

        if (myIsJumping)
        {
            myPlayerJump.Jump(myCurrentPoints, myPointsIndex, myTotalSpeed, ref myAirMovement);
            ResetSpline();
            return;
        }

        if (!myPlayerSpline.SplineMovement(myCurrentPoints, ref myUnmodifiedSpeed, ref myPointsIndex, ref mySplineT, myGravity, myBoostVector, ref myTrickBoost, ref myTotalSpeed))
        {
            myPlayerSpline.ReleaseSpline(myCurrentPoints, myTotalSpeed, ref myAirMovement, myPointsIndex);
            ResetSpline();
        }

        myCameraFollow.CameraZoom(myTotalSpeed);
    }

    private void Collision()
    {
        myCollisionData = myPlayerCollision.ReturnCollisionData();
        myCollisionData.Print();
        if (myCollisionData.GetHasCollided())
        {
            myCollisionData.SetHasCollided(false);

            if (!Bounce() && myCollisionData.GetTag() == "Rock")
            {
                myCamera.TriggerShake(myShakeDurationRocks, myShakeMagnitudeRocks);
                Crash();
                return;
            }
        }
    }

    private bool Bounce()
    {
        if (!myGrounded && myAirMovement.y < 0 && myCollisionData.GetTag() == "Rock")
        {
            myPlayerJump.Bounce(ref myAirMovement);
            return true;
        }
        return false;
    }

    private void Crash()
    {
        mySandParticleManager.DestroyAllSandParticles();
        myGameManager.GameOver(SceneManager.GetSceneAt(1));
        myGameManager.GameOver(myGameManager.GetActiveScene());
        myAirMovement = Vector2.right;
        myUnmodifiedSpeed = myStartSpeed;
        myTrickBoost = 0f;
        mySplineManager.ResetAllSplines();
        ResetSpline();
        myPlayerBackflip.ResetScore();
    }

    private void Air()
    {
        myPlayerAir.AirMovement(myGravity, ref myAirMovement);
        myCameraFollow.UpdateYOffset(myAirMovement.y);

        if (myIsJumping)
        {
            myPlayerBackflip.Backflip();
        }
        else
        {
            myPlayerAir.AirRotation();
        }

        if (myAirMovement.y > 0)
        {
            return;
        }

        if (!myPlayerSpline.AttemptToCatchSpline(mySplineManager, myReach, ref myTooCloseToOldSpline, ref myPointsIndex, ref myCurrentPoints, ref myOldPoints, ref myBoostVector))
        {
            return;
        }

        if (myPointsIndex + 1 >= myCurrentPoints.Length)
        {
            if (myPlayerBackflip.WillCrash(myCurrentPoints[myPointsIndex] - myCurrentPoints[myPointsIndex - 1]))
            {
                Crash();
                return;
            }
        }

        else if (myPlayerBackflip.WillCrash(myCurrentPoints[myPointsIndex + 1] - myCurrentPoints[myPointsIndex]))
        {
            Crash();
            return;
        }

        CatchSpline();
    }

    public void IncreaseScore()
    {
        myScore++;
    }

    public int GetScore()
    {
        return myScore;
    }
    public bool GetMagnet()
    {
        return myMagnet;
    }

    public void SetMagnet(bool aValue)
    {
        myMagnet = aValue;
    }

    private void CatchSpline()
    {
        myTrickBoost += myPlayerBackflip.GetBackflipScore();
        myCameraShake.TriggerShake(myShakeDurationSplines, myShakeMagnitudeSplines);
        myGrounded = true;
        mySplineT = 0;
    }
}