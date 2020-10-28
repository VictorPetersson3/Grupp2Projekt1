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
    private bool myIsHoldingJump;
    private bool myPressJump;
    private bool myLevelComplete = false;
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
    private bool mySpeedInvincible = false;
    private bool myIsDead = false;

    // Animation
    [SerializeField]
    private Animator myAnimator;

    // Cutscene
    [SerializeField]
    private bool myHasSeenCutscene = false;

    // Power ups
    private bool myMagnet = false;
    private bool myInvincible = false;

    // Sounds
    private AudioSource myJumpSound = null;

    // Trails
    private GameObject mySpeedTrail = null;

    private void Start()
    {
        myAnimator.SetTrigger("StartedPlaying");
        myPlayerSpline = GetComponent<PlayerSpline>();
        myPlayerJump = GetComponent<PlayerJump>();
        myPlayerAir = GetComponent<PlayerAir>();
        myPlayerInput = GetComponent<PlayerInput>();
        myPlayerCollision = GetComponentInChildren<PlayerCollision>();
        myPlayerBobbing = GetComponent<PlayerBobbing>();
        myPlayerBackflip = GetComponentInChildren<PlayerBackflip>();
        mySandParticleManager = GetComponentInChildren<SandParticleManager>();
        myCameraShake = myCameraFollow.gameObject.GetComponentInChildren<CameraShake>();
        mySpeedTrail = GameObject.FindGameObjectWithTag("SpeedTrail");
        myJumpSound = GetComponentInChildren<AudioSource>();

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
        if (myLevelComplete)
        {
            return;
        }
        if(!myHasSeenCutscene)
        {
            myAnimator.SetTrigger("StartedPlaying");
            if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Clip_Idle") && myAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                myHasSeenCutscene = true;
                myCameraShake.TriggerShake(60, 100);
            }
        }
        else
        {
            if (Time.timeScale == 0f)
            {
                return;
            }

            myAnimator.SetFloat("MovementSpeed", myTotalSpeed);
            Collision();
            ActivateTrail();

            myIsHoldingJump = myPlayerInput.IsJumping();
            myPressJump = myPlayerInput.PressJump();
            if (myGrounded)
            {
                myAnimator.SetBool("Idle", true);
                myAnimator.SetBool("InAir", false);
                Grounded();
                return;
            }

            Air();
        }
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
        if (!myPlayerSpline.GetIsRailing())
        {
            mySandParticleManager.CreateSandParticle(4);
        }
        if (myPressJump)
        {
            myAnimator.SetTrigger("Jumping");
            myAnimator.SetBool("Idle", false);
            myJumpSound.Play();
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
            if (myInvincible || mySpeedInvincible)
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
            myJumpSound.Play();
            myPlayerJump.Bounce(ref myAirMovement);
            return true;
        }
        return false;
    }

    private void Crash()
    {
        if (!myIsDead)
        {
            mySandParticleManager.DestroyAllSandParticles();
            myGameManager.GameOver(myGameManager.GetActiveScene());
            myIsDead = true;
        }
    }

    private void Air()
    {
        myOldPosition = myPlayerAir.AirMovement(myGravity, ref myAirMovement);
        myCameraFollow.UpdateYOffset(myAirMovement.y);

        if (myIsHoldingJump)
        {
            myAnimator.SetBool("Idle", false);
            myAnimator.SetBool("Bow Down", true);
            myPlayerBackflip.Backflip();
        }
        else
        {
            myAnimator.SetBool("Bow Down", false);
            myPlayerAir.AirRotation(mySplineManager.GetGroundDirection(transform.position));
        }

        bool falling = false;
        if (myAirMovement.y < 0)
        {
            falling = true;
        }

        bool isRail = false;
        if (!mySplineManager.PlayerSplineCollision(transform.position, myOldPosition, ref myPointsIndex, ref myCurrentPoints, ref myBoostVector, ref isRail, falling, myIsHoldingJump))
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

    private void ActivateTrail()
    {
        float trailLimit = 185f;
        if (myTotalSpeed >= trailLimit)
        {
            mySpeedTrail.SetActive(true);
            mySpeedInvincible = true;
        }
        else
        {
            mySpeedTrail.SetActive(false);
            mySpeedInvincible = false;
        }
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

    public bool GetSpeedInvincible()
    {
        return mySpeedInvincible;
    }

    public void SetInvincible(bool aValue)
    {
        myInvincible = aValue;
    }

    private void CatchSpline()
    {
        myPlayerBackflip.GetBackflipScore();
        myAnimator.SetTrigger("Landing");
        myCameraShake.TriggerShake(myShakeDurationSplines, myShakeMagnitudeSplines);
        myGrounded = true;
        mySplineT = 0;
    }

    public void LevelComplete()
    {
        myLevelComplete = true;
    }

    public bool GetHasSeenCutscene() { return myHasSeenCutscene; }
}