using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameManager _gameManager;
    
    private int value = 1;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
            _gameManager.UpdateCoin(value);
        
        Destroy(gameObject);
    }
}
