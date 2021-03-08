using UnityEngine;

public class CollisionDetect : MonoBehaviour
{
    private Animator _animator;
    private GameManager _gameManager;
    private Rigidbody _rb;
    private InputManager _inputManager;
    private RestartLevel _restartLevel;
    
    private static readonly int Fall = Animator.StringToHash("fall");
    private static readonly int Run = Animator.StringToHash("run");
    private static readonly int Die = Animator.StringToHash("die");
    private static readonly int Idle = Animator.StringToHash("idle");
    private static readonly int Restart = Animator.StringToHash("restart");


    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _gameManager = FindObjectOfType<GameManager>();
        _rb = GetComponent<Rigidbody>();
        _inputManager = GetComponent<InputManager>();
        _restartLevel = GetComponent<RestartLevel>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Obstacle"))
        {
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
            _restartLevel.FinishSequence();
        }
    }

    private void FallSequence(Collider other)
    {
        _animator.SetTrigger(Fall);
        var fallSide = other.transform.name == "FallLeft" ? -5f : 5f;
        _rb.useGravity = true;
        _rb.constraints = RigidbodyConstraints.None;
        _rb.velocity = new Vector3(fallSide, 5f, 0f);
    }
}
