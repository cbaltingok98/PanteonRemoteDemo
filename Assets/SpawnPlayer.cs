using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private void Start()
    {
        Instantiate(player, transform.position, Quaternion.identity);
    }
}
