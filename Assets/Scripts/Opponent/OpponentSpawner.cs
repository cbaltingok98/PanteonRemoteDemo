using UnityEngine;

public class OpponentSpawner : MonoBehaviour
{
    [SerializeField] private GameObject opponent;
    [SerializeField] private int size;

    private Vector3 _spawnPos;
    private float _xPos;
    private float _increment;

    private void Awake()
    {
        _xPos = -5f;
        _increment = 10f / size;
    }

    private void Start()
    {
        _spawnPos = transform.position;
        for (var i = 0; i < size; i++)
        {
            _spawnPos.x = _xPos;
            _xPos += _increment;
            Instantiate(opponent, _spawnPos, Quaternion.identity);   
        }
    }
}
