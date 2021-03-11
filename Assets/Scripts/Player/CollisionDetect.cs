using System;
using UnityEngine;

public class CollisionDetect : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rb;
    private InputManager _inputManager;
    private RestartLevel _restartLevel;
    private GameManager _gameManager;

    private static readonly int Fall = Animator.StringToHash("fall");
    private static readonly int Die = Animator.StringToHash("die");


    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody>();
        _inputManager = GetComponent<InputManager>();
        _restartLevel = GetComponent<RestartLevel>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Obstacle"))
        {
            Debug.Log("Obstacle");
            _animator.SetTrigger(Die);
            _restartLevel.StartCoroutine(nameof(RestartLevel.StartLateRestart));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Fall"))
        {
            FallSequence(other);
            _restartLevel.StartCoroutine(nameof(RestartLevel.StartLateRestart));
        }
        else if (other.transform.CompareTag("Finish"))
        {
            if (_inputManager.isPlayer)
                _restartLevel.FinishSequence();
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PushPlayer>())
            _inputManager.movePlayerSpeed = other.GetComponent<PushPlayer>().movePlayerSpeed;
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PushPlayer>())
            _inputManager.movePlayerSpeed = 0f;
        
        _rb.AddForce(Vector3.zero);
    }

    private void FallSequence(Collider other)
    {
        _animator.SetTrigger(Fall);
        var fallSide = other.transform.name == "FallLeft" ? -5f : 5f;
        _rb.constraints = RigidbodyConstraints.None;
        _rb.velocity = new Vector3(fallSide, 5f, 0f);
    }
}
