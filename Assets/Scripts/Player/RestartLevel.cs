using System;
using System.Collections;
using Enums;
using UnityEngine;

public class RestartLevel : MonoBehaviour
{
    private Vector3 _respawnPlayer;

    private InputManager _inputManager;
    private Rigidbody _rb;
    private Animator _animator;
    private GameManager _gameManager;
    
    private static readonly int Run = Animator.StringToHash("run");
    private static readonly int Restart = Animator.StringToHash("restart");


    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        _respawnPlayer = transform.position;
    }

    public IEnumerator StartLateRestart()
    {
        _inputManager.playerState = GameState.Restart;
        yield return new WaitForSeconds(2f);
        RestartSequence();
    }

    private void RestartSequence()
    {
        SetRigidBody();
        SetAnimation();
        SetPosition();
        _inputManager.playerState = GameState.Pause;
    }
    
    public void FinishSequence()
    {
        _animator.SetBool(Run, false);
        _inputManager.playerState = GameState.Finish;
        _gameManager.SetGameState(GameState.Paint);
        GetComponent<StartPainting>().StartPaintSequence();
    }

    private void SetRigidBody()
    {
        _rb.velocity = new Vector3(0f, 0f, 0f);
        _rb.useGravity = false;
        _rb.constraints = RigidbodyConstraints.FreezePositionY;
        _rb.freezeRotation = true;
    }
    
    private void SetAnimation()
    {
        _animator.SetBool(Run, false);
        _animator.SetTrigger(Restart);
    }
    
    private void SetPosition()
    {
        transform.position = _respawnPlayer;
        transform.rotation = Quaternion.identity;
    }
}
