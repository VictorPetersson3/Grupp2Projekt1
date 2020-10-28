using UnityEngine;
using UnityEngine.SceneManagement;

public class SandParticle : MonoBehaviour
{
    // Materials
    [SerializeField]
    private Material myLevel01Material;
    [SerializeField]
    private Material myLevel02Material;
    [SerializeField]
    private Material myLevel03Material;

    // Lifetime
    private float myMinLifeTime = 0.7f;
    private float myMaxLifeTime = 0.8f;
    private float myTotalLifeTime = 0f;
    private float myLifeTime = 0f;

    // Physics
    private float myGravity = 4f;
    private float myMaxYForce = 6f;
    private float myMinYForce = 3f;

    private Vector3 myMaxSize = new Vector3(0.6f, 0.6f, 0.6f);
    private Vector3 myScaleChange = new Vector3(0.9f, 0.9f, 0);
    private Vector3 myOriginalScale;

    private Transform myPlayerTransform = null;
    private Vector3 myRotation;

    private bool myIsDead = false;

    private void Start()
    {
        myPlayerTransform = GameObject.FindGameObjectWithTag("PlayerTag").transform;
        myOriginalScale = transform.localScale;
        SetLifeTime();
        SetMaterial();
    }

    private void OnEnable()
    {
        SetLifeTime();
    }

    private void OnDisable()
    {
        transform.localScale = myOriginalScale;
        myIsDead = false;
    }

    private void Update()
    {
        CheckIfDead();
        IncreaseSize();
        ConvertEulerToDegree();

        if (myLifeTime >= myTotalLifeTime / Random.Range(2, 4))
        {
            ApplyGravity();
            return;
        }
        ApplyForce();
    }

    private void CheckIfDead()
    {
        myLifeTime += Time.deltaTime;
        if (myLifeTime > myTotalLifeTime)
        {
            myIsDead = true;
        }
    }

    public void SetPosition(Vector3 newPos)
    {
        transform.position = newPos;
    }

    private void SetLifeTime()
    {
        myTotalLifeTime = Random.Range(myMinLifeTime, myMaxLifeTime);
        myLifeTime = 0;
    }

    private void ApplyGravity()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - (myGravity * Time.deltaTime), transform.position.z);
    }

    private void ApplyForce()
    {
        float newYForce = Random.Range(myMinYForce, myMaxYForce);
        if (myRotation.x >= 25f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (newYForce * Time.deltaTime * 3), transform.position.z);
            return;
        }
        if (myRotation.x >= 10f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + (newYForce * Time.deltaTime * 2), transform.position.z);
            return;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y + (newYForce * Time.deltaTime), transform.position.z);
    }

    private void IncreaseSize()
    {
        if (transform.localScale.x < myMaxSize.x)
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

    public bool GetIsDead()
    {
        return myIsDead;
    }

    private void SetMaterial()
    {
        for (int i = 0; i < SceneManager.sceneCount; ++i)
        {
            if (SceneManager.GetSceneAt(i).isLoaded && SceneManager.GetSceneAt(i).name != "GameManagerScene")
            {
                if (SceneManager.GetSceneAt(i).name == "Level01")
                {
                    gameObject.GetComponent<Renderer>().material = myLevel01Material;
                }
                if (SceneManager.GetSceneAt(i).name == "Level02")
                {
                    gameObject.GetComponent<Renderer>().material = myLevel02Material;
                }
                if (SceneManager.GetSceneAt(i).name == "Level3")
                {
                    gameObject.GetComponent<Renderer>().material = myLevel03Material;
                }
            }
        }
    }
}
