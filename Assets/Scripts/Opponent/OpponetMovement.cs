using Enums;
using UnityEngine;

public class OpponetMovement : MonoBehaviour
{
    [SerializeField] CharacterController controller;
    [HideInInspector] public GameState playerState;
    
    private Animator _animator;
    private GameManager _gameManager;

    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;
    private static readonly int Run = Animator.StringToHash("run");

    private float _horizontal;
    private float _vertical;

    public bool isPlayer;

    private void Awake()
    {
        playerState = GameState.Pause;
        controller.detectCollisions = false;
        _gameManager = FindObjectOfType<GameManager>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (playerState == GameState.Play && _gameManager.GetCurrentState() == GameState.Play)
            MoveCharacter();
    }

    private void MoveCharacter()
    {
        var direction = new Vector3(_horizontal, 0f, _vertical).normalized;
        
        _animator.SetBool(Run, direction.magnitude >= 0.1f);
        if (!(direction.magnitude >= 0.1f)) return;

        var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        controller.Move(direction * (speed * Time.deltaTime));
    }

}
