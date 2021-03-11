using UnityEngine;

public class OpponentSpawner : MonoBehaviour
{
    [SerializeField] private GameObject opponent;
    [SerializeField] private int size;

    private void Start()
    {
        for (var i = 0; i < size; i++)
        {
            Instantiate(opponent, transform.position, Quaternion.identity);
        }
    }
}
