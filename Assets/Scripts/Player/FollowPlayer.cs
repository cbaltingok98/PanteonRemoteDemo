using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float offSet;
    [SerializeField] private float zoffSet;
    
    private void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y + offSet, target.position.z + zoffSet);
    }
}
