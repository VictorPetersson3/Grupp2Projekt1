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
    private bool myShouldDestroy = false;

    private void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("PlayerTag").GetComponent<Player>();
        myGraphicsContainer = GameObject.FindGameObjectWithTag("magnetGraphics");
        mySoundContainer = GameObject.FindGameObjectWithTag("SoundContainer");

        if (myPlayer == null)
        {
            Debug.LogError("myPlayer: missing");
        }
        if (myMagnetSound == null)
        {
            Debug.LogError("myMagnetSound: missing");
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
            if (myPlayer == null || myMagnetSound == null)
            {
                return;
            }
            if (myGraphicsContainer != null)
            {
                myGraphicsContainer.SetActive(false);
            }
            myPlayer.SetMagnet(true);
            Instantiate(myParticleObject);
            Instantiate(myMagnetSound, mySoundContainer.transform);
            myShouldDestroy = true;
        }
    }
}
