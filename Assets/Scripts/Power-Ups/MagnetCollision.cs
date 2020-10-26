using UnityEngine;

public class MagnetCollision : MonoBehaviour
{
    private Player myPlayer = null;
    private ParticleSystem myParticleEffect = null;
    private GameObject myGraphicsContainer = null;
    private GameObject mySoundContainer = null;
    [SerializeField]
    private GameObject myMagnetSound = null;

    private void Start()
    {
        myPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        myGraphicsContainer = GameObject.FindGameObjectWithTag("magnetGraphics");
        mySoundContainer = GameObject.FindGameObjectWithTag("SoundContainer");
        myParticleEffect = GetComponent<ParticleSystem>();
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
        myParticleEffect.Play();
        myGraphicsContainer.SetActive(false);
        Instantiate(myMagnetSound, mySoundContainer.transform);
        Destroy(gameObject, myParticleEffect.main.duration);
    }
}
