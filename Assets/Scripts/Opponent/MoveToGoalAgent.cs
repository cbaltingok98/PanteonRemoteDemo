using System.Collections;
using Enums;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using UnityEngine;

public class MoveToGoalAgent : Agent
{
    private GameState _agentState;
    
    private GameObject _targetPosition;
    private GameObject[] _obstacles;

    private Rigidbody _rb;
    private Animator _animator;
    private GameManager _gameManager;

    private float _horizontal;
    private float _vertical;
    private float _speed;
    public float turnSmoothTime = 0.1f;
    private float _turnSmoothVelocity;
    private float _movePlayerSpeed;
    
    private static readonly int Run = Animator.StringToHash("run");
    private static readonly int Restart = Animator.StringToHash("restart");
    private static readonly int Die = Animator.StringToHash("die");

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        _rb.isKinematic = true;
        _obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        _targetPosition = GameObject.FindWithTag("Finish");
    }

    private void Update()
    {
        if(_gameManager.GetCurrentState() == GameState.Finish || _gameManager.GetCurrentState() == GameState.Paint)
            Destroy(gameObject);
    }

    public override void OnEpisodeBegin()
    {
        _animator.SetTrigger(Restart);
        transform.position = new Vector3(UnityEngine.Random.Range(-5f,5f), 0.7f, -17.45f);
        _agentState = GameState.Play;
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        var dirToFinish = (_targetPosition.transform.localPosition - transform.localPosition).normalized;
        sensor.AddObservation(dirToFinish.x);
        sensor.AddObservation(dirToFinish.z);

        foreach (var obstacle in _obstacles)
        {
            var dirToObject = (obstacle.transform.localPosition - transform.localPosition).normalized;
            sensor.AddObservation(dirToObject.x);
            sensor.AddObservation(dirToObject.z);
        }
    }

    public override void OnActionReceived(float[] vectorAction)
    {
        var addForce = Vector3.zero;
        _horizontal = vectorAction[0];  // 0 = Don't Move; 1 = Left; 2 = Right
        _vertical = vectorAction[1];    // 0 = Don't Move; 1 = Back; 2 = Forward
        _speed = 5f;
        
        switch (_horizontal)
        {
            case 0: addForce.x = 0f; break;
            case 1: addForce.x = -1f; break;
            case 2: addForce.x = 1f; break;
        }

        switch (_vertical)
        {
            case 0: addForce.z = 0f; break;
            case 1: addForce.z = -1f; break;
            case 2: addForce.z = 1f; break;
        }
        
        if(_agentState == GameState.Play && _gameManager.GetCurrentState() == GameState.Play)
            HandleMovement(addForce);

        AddReward(-1f / MaxStep);
    }

    private void HandleMovement(Vector3 addForce)
    {
        _rb.isKinematic = false; // temporary
        
        var targetAngle = Mathf.Atan2(addForce.x, addForce.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        _rb.velocity = addForce * _speed + new Vector3(0, _rb.velocity.y, 0);
        _rb.AddForce(Vector3.left * _movePlayerSpeed);

        _animator.SetBool(Run, addForce.magnitude >= 0.1);
    }

    public override void Heuristic(float[] actionsOut)
    {
        switch (Mathf.RoundToInt(Input.GetAxisRaw("Horizontal")))
        {
            case -1: actionsOut[0] = 1; break;
            case 0: actionsOut[0] = 0; break;
            case +1: actionsOut[0] = 2; break;
        }
        switch (Mathf.RoundToInt(Input.GetAxisRaw("Vertical")))
        {
            case -1: actionsOut[1] = 1; break;
            case 0: actionsOut[1] = 0; break;
            case +1: actionsOut[1] = 2; break;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Obstacle"))
        {
            SetReward(-0.5f);
            StartCoroutine(LateRestart());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Fall"))
        {
            SetReward(-1f);
            StartCoroutine(LateRestart());
        }
        else if (other.transform.CompareTag("Finish"))
        {
           // SetReward(+1f);
           // EndEpisode();
           _gameManager.AIVictory();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Mover"))
        {
            _movePlayerSpeed = other.GetComponent<PushPlayer>().movePlayerSpeed;
        }
        else if (other.GetComponent<Reward>())
        {
            SetReward(other.GetComponent<Reward>().GetReward());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _movePlayerSpeed = 0f;
    }

    IEnumerator LateRestart()
    {
        _agentState = GameState.Restart;
        _animator.SetTrigger(Die);
        yield return new WaitForSeconds(2f);
        EndEpisode();
    }
}
