using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] private ParticleSystem collision;
    public void PlayDeathParticle()
    {
        deathParticle.Play();
    }

    public void PlayCollisionParticle()
    {
        collision.Play();
    }
}
