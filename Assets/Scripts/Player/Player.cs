using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameManager myGameManager = null;
    [SerializeField]
    private SplineManager mySplineManager = null;
    [SerializeField]
    private CameraFollow myCameraFollow = null;
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
    private bool myIsJumping;
    private int myScore = 0;
    private CollisionData myCollisionData;
    private Vector3 myOldPosition;
    private Vector2[] myCurrentPoints;
    private Vector2 myAirMovement = Vector2.right;
    private Vector2 myBoostVector = Vector2.zero;
    private int myPointsIndex = -1;
    private float mySplineT = -1;
    private float myUnmodifiedSpeed = 0f;
    private float myTotalSpeed = 0f;
    private const int myGroundParticleAmount = 8;

    [SerializeField]
    Animator myAnimator;

    // Power ups
    private bool myMagnet = false;
    private bool myInvincible = false;

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
        myOldPosition = transform.position;

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
            //Application.Quit();
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
            myAnimator.SetBool("Idle", true);
            myAnimator.SetBool("InAir", false);
            Grounded();
            return;
        }

        Air();
    }

    private void OnGUI()
    {
        float fps = 1 / Time.deltaTime;
        GUI.Label(new Rect(0, 100, 250, 20), "FPS: " + fps);
#if UNITY_EDITOR
        GUI.Label(new Rect(0, 120, 250, 20), "Total Speed: " + myTotalSpeed);
        GUI.Label(new Rect(0, 140, 250, 20), "Air Movement: " + myAirMovement);
#endif
    }

    private void ResetSpline()
    {
        myGrounded = false;
        myPointsIndex = -1;
        myCurrentPoints = null;
        mySplineT = 0;
        myPlayerSpline.ResetAngleVariables();
        myBoostVector = Vector2.zero;
        myPlayerSpline.SetRailing(false);
    }

    private void Grounded()
    {
        myAirMovement.y = 0;
        myCameraFollow.UpdateYOffset(0);
        myPlayerBobbing.Bob();
        mySandParticleManager.CreateSandParticle(myGroundParticleAmount);
        if (myIsJumping)
        {
            myAnimator.SetTrigger("Jumping");
            myAnimator.SetBool("Idle", false);
            myPlayerJump.Jump(myCurrentPoints, myPointsIndex, myTotalSpeed, ref myAirMovement);
            ResetSpline();
            return;
        }

        if (!myPlayerSpline.SplineMovement(myCurrentPoints, ref myUnmodifiedSpeed, ref myPointsIndex, ref mySplineT, myGravity, myBoostVector, ref myTotalSpeed))
        {
            myPlayerSpline.ReleaseSpline(myCurrentPoints, myTotalSpeed, ref myAirMovement, myPointsIndex);
            ResetSpline();
        }

        myCameraFollow.CameraZoom(myTotalSpeed);
    }

    private void Collision()
    {
        myCollisionData = myPlayerCollision.ReturnCollisionData();
        if (myCollisionData.GetHasCollided())
        {
            myCollisionData.SetHasCollided(false);
            if (myCollisionData.GetTag() == "Top")
            {
                Bounce();
                return;
            }
            if (myInvincible)
            {
                return;
            }
            if (myCollisionData.GetTag() == "Left")
            {
                myCameraShake.TriggerShake(myShakeDurationRocks, myShakeMagnitudeRocks);
                Crash();
                return;
            }
        }
    }

    private bool Bounce()
    {
        if (!myGrounded && myAirMovement.y < 0)
        {
            myPlayerJump.Bounce(ref myAirMovement);
            return true;
        }
        return false;
    }

    private void Crash()
    {
        mySandParticleManager.DestroyAllSandParticles();
        myGameManager.GameOver(myGameManager.GetActiveScene());
    }

    private void Air()
    {
        myOldPosition = myPlayerAir.AirMovement(myGravity, ref myAirMovement);
        myCameraFollow.UpdateYOffset(myAirMovement.y);

        if (myIsJumping)
        {
            myAnimator.SetBool("Idle", false);
            //myAnimator.SetBool("InAir", false);
            myAnimator.SetBool("Bow Down", true);
            myPlayerBackflip.Backflip();
        }
        else
        {
            myAnimator.SetBool("Bow Down", false);
            //myAnimator.SetBool("InAir", true);
            myPlayerAir.AirRotation(mySplineManager.GetGroundDirection(transform.position));
        }

        if (myAirMovement.y > 0)
        {
            return;
        }

        bool isRail = false;
        if (!mySplineManager.PlayerSplineCollision(transform.position, myOldPosition, ref myPointsIndex, ref myCurrentPoints, ref myBoostVector, ref isRail))
        {
            return;
        }

        myPlayerSpline.SetRailing(isRail);

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

    public bool GetInvincible()
    {
        return myInvincible;
    }

    public void SetInvincible(bool aValue)
    {
        myInvincible = aValue;
    }

    private void CatchSpline()
    {
        myPlayerBackflip.GetBackflipScore();
        //myAnimator.SetTrigger("Landing");
        myCameraShake.TriggerShake(myShakeDurationSplines, myShakeMagnitudeSplines);
        myGrounded = true;
        mySplineT = 0;
    }
}