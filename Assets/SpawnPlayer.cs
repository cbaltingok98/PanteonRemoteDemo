using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Camera mainCamera;
    private void Start()
    {
        Instantiate(player, transform.position, Quaternion.identity);
    }
}
