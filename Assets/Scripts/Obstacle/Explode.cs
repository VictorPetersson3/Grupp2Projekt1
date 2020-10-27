using UnityEngine;

public class Explode : MonoBehaviour
{
    private Player myPlayer = null;
    private GameObject myParent = null;
    private GameObject myGraphicsContainer = null;
    private ParticleSystem myExplosion = null;
    private GameObject mySoundContainer = null;
    [SerializeField]
    private GameObject myExplosionSound = null;

    void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<Player>();
        mySoundContainer = GameObject.FindGameObjectWithTag("SoundContainer");
        myParent = transform.parent.gameObject;
        myGraphicsContainer = GameObject.Find("capsulCrashed");
        myExplosion = gameObject.GetComponentInParent(typeof(ParticleSystem)) as ParticleSystem;

        if (myPlayer == null)
        {
            Debug.LogError("myPlayer: missing");
        }
        if (myExplosionSound == null)
        {
            Debug.LogError("myExplosionSound: missing");
        }
    }

    private void OnCollisionEnter(Collision aCollision)
    {
        if (myPlayer == null || myExplosionSound == null)
        {
            return;
        }

        if (myPlayer.GetInvincible())
        {
            myGraphicsContainer.SetActive(false);
            myExplosion.Play();
            Instantiate(myExplosionSound, mySoundContainer.transform);
            Destroy(myParent, myExplosion.main.duration);
        }

    }
}
