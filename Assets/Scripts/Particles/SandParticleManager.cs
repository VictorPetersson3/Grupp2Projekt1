using System.Collections.Generic;
using UnityEngine;

public class SandParticleManager : MonoBehaviour
{
    [SerializeField]
    private GameObject mySandParticle = null;
    [SerializeField]
    private List<GameObject> mySandParticles = new List<GameObject>();
    [SerializeField]
    private float myOffsetX = 1f;
    [SerializeField]
    private float myOffsetY = 1f;

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

    public void CreateSandParticle()
    {
        float newX = Random.Range(transform.position.x - myOffsetX, transform.position.x - myOffsetX*2);
        float newY = Random.Range(transform.position.y, transform.position.y + myOffsetY);
        Vector3 newPosition = new Vector3(newX, newY, 0);

        GameObject particle = Instantiate(mySandParticle, newPosition, Quaternion.identity);
        mySandParticles.Add(particle);
    }
}
