using UnityEngine;

public class InvincibleCollision : MonoBehaviour
{
    private Player myPlayer = null;
    private GameObject mySoundContainer;
    [SerializeField]
    private GameObject mySound = null;
    
    void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
        myPlayer.SetInvincible(true);
        Instantiate(mySound, mySoundContainer.transform);
        Destroy(gameObject);
    }
}
