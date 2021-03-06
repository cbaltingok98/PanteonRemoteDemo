using Enums;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    
    private Animator _animator;
    private Rigidbody _rb;
    private GameManager _gameManager;
    public Joystick joystick;
   
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;
    private static readonly int Run = Animator.StringToHash("run");

    private void Awake()
    {
        controller.detectCollisions = false;
        _gameManager = FindObjectOfType<GameManager>();
        _animator = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(_gameManager.GetCurrentState() == GameState.Play)
            MoveCharacter();

        if (_gameManager.GetCurrentState() == GameState.Pause && (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space))) 
            _gameManager.SetGameState(GameState.Play);
    }

    private void MoveCharacter()
    {
        //float horizontal = joystick.Horizontal;
        //float vertical = joystick.Vertical;

        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        
        var direction = new Vector3(horizontal, 0f, vertical).normalized;

        _animator.SetBool(Run, direction.magnitude >= 0.1f);
        if (!(direction.magnitude >= 0.1f)) return;

        var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        controller.Move(direction * (speed * Time.deltaTime));
        
    }

}
