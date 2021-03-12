using UnityEngine;

public class StartPainting : MonoBehaviour
{
    private GameManager _gameManager;
    private UIManager _uiManager;
    private Rigidbody _rb;
    private Animator _animator;
    
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform paintingPosition;
    [SerializeField] private ParticleSystem explosionParticle;
    
    private float _speed;
    private bool _move;
    
    private static readonly int Dance = Animator.StringToHash("dance");

    private void Awake()
    {
        _speed = 2f;
        _move = false;
        _gameManager = FindObjectOfType<GameManager>();
        _uiManager = FindObjectOfType <UIManager>();
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        if(_move)
            GoToPaintingPos();
    }

    public void StartPaintSequence()
    {
        _move = true;
        SetCamera();
        _animator.SetTrigger(Dance);
        _uiManager.IsActiveWallPercent(true);
        explosionParticle.Play();
        _gameManager.IsActivePainting(true);
    }
    
    [ContextMenu("Go to painting pos")]
    private void GoToPaintingPos()
    {
        _rb.MovePosition(Vector3.Lerp(transform.position, paintingPosition.position, Time.deltaTime * _speed));
        
        if (paintingPosition.position.x == transform.position.x)
            _move = false;
    }

    private void SetCamera()
    {   
        mainCamera.GetComponent<FollowPlayer>().PaintingCamera();
    }
}
