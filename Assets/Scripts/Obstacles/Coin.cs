using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameManager _gameManager;
    private AudioManager _audioManager;

    private ParticleSystem explodeParticle;
    
    private int value = 1;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _audioManager = AudioManager.instance;
        explodeParticle = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            _gameManager.UpdateCoin(value);
            _audioManager.Play("Coin");
        }

        explodeParticle.transform.parent = null;
        explodeParticle.Play();
        Destroy(gameObject);
    }
}
