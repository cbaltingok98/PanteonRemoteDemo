using System;
using System.Collections;
using Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionDetect : MonoBehaviour
{
    [SerializeField] private Transform spawnPlayer;
    private Animator _animator;
    private GameManager _gameManager;
    private Rigidbody _rb;
    private static readonly int Fall = Animator.StringToHash("fall");

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _gameManager = FindObjectOfType<GameManager>();
        _rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Obstacle"))
            StartCoroutine(nameof(StartLateRestart));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Fall"))
        {
            _gameManager.SetGameState(GameState.Finish);
            _animator.SetBool(Fall, true);
            _rb.useGravity = true;
            var fallSide = other.transform.name == "FallLeft" ? -5f : 5f;
            _rb.velocity = new Vector3(fallSide, 5f, 0f);
        }
        else if (other.transform.CompareTag("RestartLevel"))
        {
            SceneManager.LoadScene(0);
        }
        else if (other.transform.CompareTag("Finish"))
        {
            _gameManager.SetGameState(GameState.Finish);
            _animator.SetBool("run", false);
        }
    }

    IEnumerator StartLateRestart()
    {
        _gameManager.SetGameState(GameState.Finish);
        _animator.SetTrigger("die");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(0);
    }

    private void SpawnPlayer()
    {
        _gameManager.SetGameState(GameState.Pause);
        _animator.SetBool("idle", true);
        transform.position = spawnPlayer.position;
        transform.rotation = Quaternion.identity;
    }
}
