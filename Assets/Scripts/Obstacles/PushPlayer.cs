using UnityEngine;

public class PushPlayer : MonoBehaviour
{
    [SerializeField] private float movePlayerSpeed;
    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<CharacterController>())
            other.GetComponent<CharacterController>().Move(Vector3.left * movePlayerSpeed * Time.deltaTime);
    }
}