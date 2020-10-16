using UnityEngine;

public class CogsCollision : MonoBehaviour
{
    private Player myPlayer = null;
    private GameObject mySoundContainer;
    [SerializeField]
    private GameObject myCogSound = null;

    private void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        mySoundContainer = GameObject.FindGameObjectWithTag("SoundContainer");
        if (myPlayer == null)
        {
            Debug.LogError("myPlayer: " + myPlayer);
        }
        if (myCogSound == null)
        {
            Debug.LogError("myCogSound: " + myCogSound);
        }
    }
    private void OnCollisionEnter(Collision aCollision)
    {
        myPlayer.IncreaseScore();
        Instantiate(myCogSound, mySoundContainer.transform);
        Destroy(gameObject);
    }
}
