using UnityEngine;

public class MagnetCollision : MonoBehaviour
{
    private Player myPlayer = null;
    private GameObject mySoundContainer;
    [SerializeField]
    private GameObject myMagnetSound;

    private void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        mySoundContainer = GameObject.FindGameObjectWithTag("SoundContainer");
        if (myPlayer == null)
        {
            Debug.LogError("Error: myPlayer " + myPlayer);
        }
    }

    private void OnCollisionEnter(Collision aCollision)
    {
        myPlayer.SetMagnet(true);
        Instantiate(myMagnetSound, mySoundContainer.transform);
        Destroy(gameObject);
    }
}
