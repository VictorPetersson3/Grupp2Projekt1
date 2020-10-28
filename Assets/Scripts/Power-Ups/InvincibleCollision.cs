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
    private bool myShouldDestroy = false;
    
    void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<Player>();
        myGraphicsContainer = GameObject.FindGameObjectWithTag("invincibleGraphics");
        mySoundContainer = GameObject.FindGameObjectWithTag("SoundContainer");

        if (myPlayer == null)
        {
            Debug.LogError("myPlayer: missing");
        }
        if (mySound == null)
        {
            Debug.LogError("mySound: missing");
        }  
    }

    private void LateUpdate()
    {
        if (myShouldDestroy)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision aCollision)
    {
        if (aCollision.gameObject.layer == 8)
        {
            if (myPlayer == null || mySound == null)
            {
                return;
            }
            Debug.Log("Invincible working");

            if (myGraphicsContainer != null)
            {
                myGraphicsContainer.SetActive(false);
            }
            myPlayer.SetInvincible(true);
            Instantiate(myParticleObject);
            Instantiate(mySound, mySoundContainer.transform);
            myShouldDestroy = true;
        }
    }
}
