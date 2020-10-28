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

        // Instantiate 1000 particles.
        for (int i = 0; i <= 1000; i++)
        {
            GameObject particle = Instantiate(mySandParticle, Vector3.zero, Quaternion.identity, mySandParticlesContainer.transform);
            particle.SetActive(false);
            mySandParticles.Add(particle);
        }
    }

    void Update()
    {
        for (int i = 0; i < mySandParticles.Count; i++)
        {
            // Remove
            GameObject currentObject = mySandParticles[i];
            SandParticle currentParticle = currentObject.GetComponent<SandParticle>();
            if (currentParticle.GetIsDead() && currentParticle.gameObject.activeSelf)
            {
                mySandParticles[i].GetComponent<SandParticle>().gameObject.SetActive(false);
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

    public void SetAllInactive()
    {
        for (int i = 0; i < mySandParticles.Count; i++)
        {
            mySandParticles[i].GetComponent<SandParticle>().gameObject.SetActive(false);
        }
    }

    public void CreateSandParticle(int aAmount)
    {
        for (int i = 0; i < mySandParticles.Count-aAmount; i++)
        {
            if (!mySandParticles[i].GetComponent<SandParticle>().gameObject.activeSelf)
            {
                for (int j = 0; j < aAmount; j++)
                {
                    float newX = Random.Range(transform.position.x, transform.position.x - myOffsetX);
                    float newY = Random.Range(transform.position.y, transform.position.y + myOffsetY);
                    Vector3 newPosition = new Vector3(newX, newY, 0);

                    mySandParticles[i+j].GetComponent<SandParticle>().gameObject.SetActive(true);
                    mySandParticles[i+j].GetComponent<SandParticle>().SetPosition(newPosition);
                }
                return;
            }
        }
    }
}
