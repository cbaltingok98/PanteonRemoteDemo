using UnityEngine;

public class StartPainting : MonoBehaviour
{
    private GameManager _gameManager;
    private UIManager _uiManager;
    private Rigidbody _rb;
    
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform paintingPosition;
    
    private float _speed;
    private bool _move;

    private void Awake()
    {
        _speed = 2f;
        _move = false;
        _gameManager = FindObjectOfType<GameManager>();
        _uiManager = FindObjectOfType <UIManager>();
        _rb = GetComponent<Rigidbody>();
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
        _uiManager.IsActiveWallPercent(true);
        _uiManager.PaintingJoystick();
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
