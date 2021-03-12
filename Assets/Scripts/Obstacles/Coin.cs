using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameManager _gameManager;

    private ParticleSystem explodeParticle;
    
    [SerializeField] private int value = 1;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        explodeParticle = GetComponentInChildren<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            _gameManager.UpdateCoin(value);
        }

        explodeParticle.transform.parent = null;
        explodeParticle.Play();
        Destroy(gameObject);
    }
}
