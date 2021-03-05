using Enums;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Animator animator;
    
    private float _turnSmoothVelocity;
    private GameManager _gameManager;
    
    public Joystick joystick;
    public CharacterController controller;
   
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;

    private Rigidbody _rb;
    
    private void Awake()
    {
        controller.detectCollisions = false;
        _gameManager = FindObjectOfType<GameManager>();
        animator = GetComponentInChildren<Animator>();
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
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        //float horizontal = Input.GetAxisRaw("Horizontal");
        //float vertical = Input.GetAxisRaw("Vertical");
        
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        animator.SetBool("run", direction.magnitude >= 0.1f);
        if (!(direction.magnitude >= 0.1f)) return;

        var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        controller.Move(direction * (speed * Time.deltaTime));
    }

}
