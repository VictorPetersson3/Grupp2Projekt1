using UnityEngine;

public class MagnetCollision : MonoBehaviour
{
    private Player myPlayer = null;
    private GameObject mySoundContainer;
    [SerializeField]
    private GameObject myMagnetSound = null;

    private void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        mySoundContainer = GameObject.FindGameObjectWithTag("SoundContainer");
        if (myPlayer == null)
        {
            Debug.LogError("myPlayer: " + myPlayer);
        }
        if (myMagnetSound == null)
        {
            Debug.LogError("myMagnetSound: " + myMagnetSound);
        }
    }

    private void OnCollisionEnter(Collision aCollision)
    {
        myPlayer.SetMagnet(true);
        Instantiate(myMagnetSound, mySoundContainer.transform);
        Destroy(gameObject);
    }
}
