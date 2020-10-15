using UnityEngine;

public class CogsCollision : MonoBehaviour
{
    private Player myPlayer = null;
    private GameObject mySoundContainer;
    [SerializeField]
    private GameObject myCogSound;

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
        Instantiate(myCogSound, mySoundContainer.transform);
        Destroy(gameObject);
    }
}
