using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float xOffSet;
    [SerializeField] private float yOffSet;
    [SerializeField] private float zOffSet;
    
    private void Update()
    {
        var position = target.position;
        transform.position = new Vector3(position.x + xOffSet, position.y + yOffSet, position.z - zOffSet);
    }

    [ContextMenu("Painting Camera")]
    public void PaintingCamera()
    {
        xOffSet = -1f;
        yOffSet = 3.7f;
        zOffSet = 5.2f;
    }

}
