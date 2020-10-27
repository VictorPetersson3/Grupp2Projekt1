using UnityEngine;

public class MagnetCollision : MonoBehaviour
{
    private Player myPlayer = null;
    private GameObject myGraphicsContainer = null;
    private GameObject mySoundContainer = null;
    [SerializeField]
    private GameObject myParticleObject = null;
    [SerializeField]
    private GameObject myMagnetSound = null;

    private void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        myGraphicsContainer = GameObject.FindGameObjectWithTag("magnetGraphics");
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
        myGraphicsContainer.SetActive(false);
        myPlayer.SetMagnet(true);
        Instantiate(myParticleObject);
        Instantiate(myMagnetSound, mySoundContainer.transform);
        Destroy(gameObject);
    }
}
