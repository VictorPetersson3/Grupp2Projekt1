using Boo.Lang;
using UnityEngine;

public class Explode : MonoBehaviour
{
    [SerializeField]
    private GameObject myRockParticle = null;
    // Currently using same container as for sandparticles
    private GameObject mySandParticlesContainer = null;
    private List<GameObject> myRockParticles = new List<GameObject>();
    private Player myPlayer = null;

    void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (myPlayer == null)
        {
            Debug.LogError("myPlayer: " + myPlayer);
        }
        mySandParticlesContainer = GameObject.FindGameObjectWithTag("SandParticles");
        if (mySandParticlesContainer == null)
        {
            Debug.LogError("mySandParticleContainer: " + mySandParticlesContainer);
        }
    }

    private void Update()
    {
        for (int i = myRockParticles.Count - 1; i >= 0; i--)
        {
            // Remove
            GameObject currentObject = myRockParticles[i];
            SandParticle currentParticle = currentObject.GetComponent<SandParticle>();
            if (currentParticle.myIsDead)
            {
                myRockParticles.RemoveAt(i);
                Destroy(currentObject);
            }
        }
    }

    private void OnCollisionEnter(Collision aCollision)
    {
        if (myPlayer.GetInvincible())
        {
            // TODO: Fix Explosion
            Destroy(gameObject);
            //CreateExplosion(25);
        }

    }

    private void CreateExplosion(int aTimes)
    {
        for (int i = 0; i < aTimes; i++)
        {
            float newX = Random.Range(transform.position.x - 5, transform.position.x + 5);
            float newY = Random.Range(transform.position.y - 5, transform.position.y + 5);
            Vector3 newPosition = new Vector3(newX, newY, 0);

            GameObject particle = Instantiate(myRockParticle, newPosition, Quaternion.identity, mySandParticlesContainer.transform);
            myRockParticles.Add(particle);
        }
    }
}
