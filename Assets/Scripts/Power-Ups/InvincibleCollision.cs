using UnityEngine;

public class InvincibleCollision : MonoBehaviour
{
    private Player myPlayer = null;
    private GameObject myGraphicsContainer = null;
    private GameObject mySoundContainer = null;
    [SerializeField]
    private GameObject myParticleObject = null;
    [SerializeField]
    private GameObject mySound = null;
    
    void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        myGraphicsContainer = GameObject.FindGameObjectWithTag("invincibleGraphics");
        mySoundContainer = GameObject.FindGameObjectWithTag("SoundContainer");

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
        myGraphicsContainer.SetActive(false);
        myPlayer.SetInvincible(true);
        Instantiate(myParticleObject);
        Instantiate(mySound, mySoundContainer.transform);
        Destroy(gameObject);
    }
}
