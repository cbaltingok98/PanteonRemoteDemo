using UnityEngine;

public class StartPainting : MonoBehaviour
{
    private GameManager _gameManager;
    private UIManager _uiManager;
    
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
    }

    private void Update()
    {
        if(_move)
            GoToPaintingPos();
    }

    public void StartPaintSequence()
    {
        _move = true;
        SetCamera();
        _uiManager.IsActiveWallPercent(true);
        _gameManager.IsActivePainting(true);
    }
    
    private void GoToPaintingPos()
    {
        transform.position =
            Vector3.MoveTowards(transform.position, paintingPosition.position, _speed * Time.deltaTime);

        if (transform.position == paintingPosition.position)
            _move = false;
    }

    private void SetCamera()
    {   
        mainCamera.GetComponent<FollowPlayer>().PaintingCamera();
    }
}
