using System;
using UnityEngine;

public class RotateObstacle : MonoBehaviour
{
    [SerializeField] private float zTurnSpeed;
    [SerializeField] private float yTurnSpeed;

    private void Awake()
    {
        transform.parent = null;
    }

    private void Update()
    {
        transform.eulerAngles += new Vector3(0, yTurnSpeed, zTurnSpeed) * Time.deltaTime;
    }
}
