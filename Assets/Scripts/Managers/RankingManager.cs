using Enums;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    private MoveToGoalAgent[] _opponents;
    private Transform _player;

    private UIManager _uiManager;
    private GameManager _gameManager;

    private int _currentRank;
    private void Awake()
    {
        _uiManager = FindObjectOfType<UIManager>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        _opponents = FindObjectsOfType<MoveToGoalAgent>();
        _player = FindObjectOfType<InputManager>().GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        if(_gameManager.GetCurrentState() == GameState.Play)
            CheckRank();
    }

    private void CheckRank()
    {
        _currentRank = 1;
        foreach (var opponent in _opponents)
        {
            _currentRank += opponent.transform.position.z > _player.position.z ? 1 : 0;
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        _uiManager.UpdateRankText(_currentRank.ToString());
    }
}
