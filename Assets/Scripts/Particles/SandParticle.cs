using UnityEngine;

public class SandParticle : MonoBehaviour
{
    // Lifetime
    private float myMinLifeTime = 0.3f;
    private float myMaxLifeTime = 0.7f;
    private float myTotalLifeTime = 0f;
    private float myLifeTime = 0f;

    // Physics
    private float myGravity = 6f;
    private float myMaxYForce = 2f;
    private float myMinYForce = 1.5f;

    // Color and size
    private float myDecreseOpacity = 0.02f;
    private Vector3 myScaleChange = new Vector3(0.4f, 0.4f, 0);
    private Material myMaterial;
    private Color myOriginalColor;
    private Color myNewColor;

    // Getting rotation
    private Vector3 myRotation;
    private Transform myPlayerTransform;
    private SandParticleManager mySandParticleManager;

    [HideInInspector]
    public bool myIsDead = false;

    private void Start()
    {
        // Set life
        SetLifeTime();

        // Rotation
        myPlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        mySandParticleManager = GameObject.FindGameObjectWithTag("SandParticleManager").GetComponent<SandParticleManager>();

        // Render
        myMaterial = GetComponent<MeshRenderer>().material;
        myOriginalColor = myMaterial.color;
        myNewColor = myOriginalColor;

        ConvertEulerToDegree();
    }

    private void Update()
    {
        CheckIfDead();
        ApplyForce();
        ApplyGravity();
        IncreaseSize();
        IncreaseTransparency();
    }

    private void CheckIfDead()
    {
        myLifeTime += Time.deltaTime;
        if (myLifeTime > myTotalLifeTime)
        {
            myIsDead = true;
        }
    }

    private void SetLifeTime()
    {
        myTotalLifeTime = Random.Range(myMinLifeTime, myMaxLifeTime);
    }

    private void IncreaseTransparency()
    {
        if (myNewColor.a > 0.1f)
        {
            myNewColor.a -= myDecreseOpacity;
            myMaterial.SetColor("_Color", myNewColor);
        }
    }

    private void ApplyGravity()
    {
        if (myLifeTime >= myTotalLifeTime / 2)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (myGravity * Time.deltaTime), transform.position.z);
        }
    }

    private void ApplyForce()
    {
        float newYForce = Random.Range(myMinYForce, myMaxYForce);

        if (myRotation.x < -5)
        {
            mySandParticleManager.ChangeSpawnOffsetY();
            mySandParticleManager.ChangeSpawnOffsetX();
            transform.position = new Vector3(transform.position.x, transform.position.y + (newYForce * Time.deltaTime), transform.position.z);
            return;
        }
        mySandParticleManager.ChangeSpawnOffsetY();
        mySandParticleManager.ChangeSpawnOffsetX();
        transform.position = new Vector3(transform.position.x, transform.position.y + (newYForce * Time.deltaTime * 2), transform.position.z);
    }

    private void IncreaseSize()
    {
        transform.localScale += myScaleChange * Time.deltaTime;
    }

    private void ConvertEulerToDegree()
    {
        if (myPlayerTransform.eulerAngles.x > 90)
        {
            myRotation = new Vector3(-(360 - myPlayerTransform.eulerAngles.x), 0, 0);
        }
        else
        {
            myRotation = new Vector3(myPlayerTransform.eulerAngles.x, 0, 0);
        }
    }
}
