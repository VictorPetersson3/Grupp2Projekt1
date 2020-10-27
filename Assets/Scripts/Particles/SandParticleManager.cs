using System.Collections.Generic;
using UnityEngine;

public class SandParticleManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mySandParticle = null;

    private List<GameObject> mySandParticles = new List<GameObject>();
    private GameObject mySandParticlesContainer = null;
 
    private float myOffsetX = 0.25f;
    private float myOffsetY = 0.5f;

    private void Start()
    {
        mySandParticlesContainer = GameObject.FindGameObjectWithTag("SandParticles");
        if (mySandParticlesContainer == null)
        {
            Debug.LogError("Error, you need a SandParticleContainer.");
        }
    }

    void Update()
    {
        for (int i = mySandParticles.Count - 1; i >= 0; i--)
        {
            // Remove
            GameObject currentObject = mySandParticles[i];
            SandParticle currentParticle = currentObject.GetComponent<SandParticle>();
            if (currentParticle.myIsDead)
            {
                mySandParticles.RemoveAt(i);
                Destroy(currentObject);
            }
        }
    }

    public void DestroyAllSandParticles()
    {
        for (int i = mySandParticles.Count - 1; i >= 0; i--)
        {
            GameObject currentObject = mySandParticles[i];
            mySandParticles.RemoveAt(i);
            Destroy(currentObject);
        }
    }

    public void CreateSandParticle(int aTimes)
    {
        float amount = aTimes * Time.deltaTime;
        for (int i = 0; i < amount; i++)
        {
            float newX = Random.Range(transform.position.x, transform.position.x - myOffsetX);
            float newY = Random.Range(transform.position.y, transform.position.y + myOffsetY);
            Vector3 newPosition = new Vector3(newX, newY, 0);

            GameObject particle = Instantiate(mySandParticle, newPosition, Quaternion.identity, mySandParticlesContainer.transform);
            mySandParticles.Add(particle);
        }
    }
}
