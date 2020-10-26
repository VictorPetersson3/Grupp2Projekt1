using UnityEngine;

public class Explode : MonoBehaviour
{
    private Player myPlayer = null;
    private GameObject myParent = null;
    private GameObject myGraphicsContainer = null;
    private ParticleSystem myExplosion = null;

    void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        myParent = transform.parent.gameObject;
        myGraphicsContainer = GameObject.Find("capsulCrashed");
        myExplosion = gameObject.GetComponentInParent(typeof(ParticleSystem)) as ParticleSystem;
        if (myPlayer == null)
        {
            Debug.LogError("myPlayer: " + myPlayer);
        }
    }

    private void OnCollisionEnter(Collision aCollision)
    {
        if (myPlayer.GetInvincible())
        {
            myExplosion.Play();
            myGraphicsContainer.SetActive(false);
            Destroy(myParent, myExplosion.main.duration);
        }

    }
}
