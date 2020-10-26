using UnityEngine;

public class InvincibleCollision : MonoBehaviour
{
    private Player myPlayer = null;
    private ParticleSystem myParticleEffect = null;
    private GameObject myGraphicsContainer = null;
    private GameObject mySoundContainer = null;
    [SerializeField]
    private GameObject mySound = null;
    
    void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        myGraphicsContainer = GameObject.FindGameObjectWithTag("invincibleGraphics");
        mySoundContainer = GameObject.FindGameObjectWithTag("SoundContainer");
        myParticleEffect = GetComponent<ParticleSystem>();
        if (myPlayer == null)
        {
            Debug.LogError("myPlayer: " + myPlayer);
        }
        if (mySound == null)
        {
            Debug.LogError("mySound: " + mySound);
        }  
    }

    private void OnCollisionEnter(Collision aCollision)
    {
        myPlayer.SetInvincible(true);
        myParticleEffect.Play();
        myGraphicsContainer.SetActive(false);
        Instantiate(mySound, mySoundContainer.transform);
        Destroy(gameObject, myParticleEffect.main.duration);
    }
}
