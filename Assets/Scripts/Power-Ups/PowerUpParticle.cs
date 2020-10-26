using UnityEngine;

public class PowerUpParticle : MonoBehaviour
{
    private ParticleSystem myParticleEffect = null;

    void Start()
    {
        Debug.Log("COLORS");
        myParticleEffect = GetComponent<ParticleSystem>();
        myParticleEffect.Play();
        Destroy(gameObject, myParticleEffect.main.duration);
    }
}
