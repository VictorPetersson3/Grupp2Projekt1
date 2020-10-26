using UnityEngine;

public class PowerUpParticle : MonoBehaviour
{
    private ParticleSystem myParticleEffect = null;

    void Start()
    {
        myParticleEffect = GetComponent<ParticleSystem>();
        myParticleEffect.Play();
        Destroy(gameObject, myParticleEffect.main.duration);
    }
}
