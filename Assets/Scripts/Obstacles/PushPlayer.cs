using UnityEngine;

public class PushPlayer : MonoBehaviour
{
    public float movePlayerSpeed;
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
            other.GetComponent<Rigidbody>().AddForce(Vector3.left * movePlayerSpeed);
    }
}