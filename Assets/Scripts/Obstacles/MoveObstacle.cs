using UnityEngine;

[DisallowMultipleComponent]
public class MoveObstacle : MonoBehaviour
{
    [SerializeField] bool move = true;
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 3f;

    [SerializeField] float movementFactor; // 0 for not moved, 1 for fully moved

    Vector3 _startingPos;

    private void Start()
    {
        _startingPos = transform.position;
    }

    private void FixedUpdate()
    {
        if (!move) return;
        
        if (period <= Mathf.Epsilon) { return; }
        var cycles = Time.time / period; // grows continually from 0

        const float tau = Mathf.PI * 2; // about 6.28
        var rawSinWave = Mathf.Sin(cycles * tau);

        movementFactor = rawSinWave / 2f + 0.5f;
        Vector3 offset = movementFactor * movementVector;
        transform.position = (_startingPos + offset);
    }
}

