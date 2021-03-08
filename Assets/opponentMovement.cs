using Enums;
using UnityEngine;

public class opponentMovement : MonoBehaviour
{
    private Rigidbody _rb;
    private GameManager _gameManager;
    private Animator _animator;
    private static readonly int Run = Animator.StringToHash("run");

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _gameManager = FindObjectOfType<GameManager>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        //if (_gameManager.GetCurrentState() == GameState.Play)
            HandleMovement();
    }

    private void HandleMovement()
    {
        
        var _horizontal = Input.GetAxisRaw("Horizontal");
        var _vertical = Input.GetAxisRaw("Vertical");
        

        var direction = new Vector3(_horizontal, transform.position.y, _vertical).normalized;
        
        _animator.SetBool(Run, direction.magnitude >= 0.1f);
        if (!(direction.magnitude >= 0.1f)) return;

        var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        //controller.Move(direction * (speed * Time.deltaTime));
        _rb.AddForce(direction * (speed * Time.deltaTime));
    }
}
