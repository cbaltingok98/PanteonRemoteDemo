using System;
using System.Runtime.CompilerServices;
using Enums;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [HideInInspector] public GameState playerState;
    
    private Animator _animator;
    private GameManager _gameManager;
    private Rigidbody _rb;
    public Joystick joystick;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;
    private static readonly int Run = Animator.StringToHash("run");

    [HideInInspector] public Vector3 addForce;
    private float _horizontal;
    private float _vertical;

    public bool isPlayer;
    public float movePlayerSpeed;

    private void Awake()
    {
        playerState = GameState.Pause;
        _rb = GetComponent<Rigidbody>();
        _gameManager = FindObjectOfType<GameManager>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        addForce = Vector3.zero;
        _rb.isKinematic = true;
    }

    private void Update()
    {
        if(playerState == GameState.Play && _gameManager.GetCurrentState() == GameState.Play)
            MoveCharacter();

        if (playerState == GameState.Pause && (Input.touchCount > 0 || Input.GetKeyDown(KeyCode.Space)))
        {
            _gameManager.SetGameState(GameState.Play);
            playerState = GameState.Play;
            _rb.isKinematic = false;
        }
        
    }

    private void MoveCharacter()
    {
        if(isPlayer)
           GetInput();

        //HandleJoystick();
        addForce.x = _horizontal;
        addForce.z = _vertical;

        _animator.SetBool(Run, addForce.magnitude >= 0.1f);

        if (!(addForce.magnitude >= 0.1f))
            addForce = RemoveForce();

        var targetAngle = Mathf.Atan2(addForce.x, addForce.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        
        _rb.velocity = addForce * speed + new Vector3(0, _rb.velocity.y, 0);
        _rb.AddForce(Vector3.left * movePlayerSpeed);
    }

    private void GetInput()
    {
        if (_gameManager.IsKeyboard())
        {
            HandleKeyInput();
        }
        else
        {
            HandleJoystick();
        }
    }

    private void HandleKeyInput()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
    }

    private void HandleJoystick()
    {
        _horizontal = joystick.Horizontal;

        if (joystick.Vertical >= .2f)
        {
            _vertical = 1f;
        }
        else if (joystick.Vertical <= -.2f)
        {
            _vertical = -1f;
        }
        else
        {
            _vertical = 0;
        }
    }

    private Vector3 RemoveForce()
    {
        return new Vector3(0f, 0f, 0f);
    }
}
