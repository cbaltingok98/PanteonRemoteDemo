using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float offSet;
    [SerializeField] private float zoffSet;
    
    private void Update()
    {
        var position = target.position;
        transform.position = new Vector3(position.x, position.y + offSet, position.z + zoffSet);
    }
}
