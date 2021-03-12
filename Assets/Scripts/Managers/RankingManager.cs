using Enums;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    private MoveToGoalAgent[] _opponents;
    private Transform _player;

    private UIManager _uiManager;
    private GameManager _gameManager;

    private int _currentRank;
    private bool load;
    private void Awake()
    {
        _uiManager = FindObjectOfType<UIManager>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        load = true;
    }

    private void FixedUpdate()
    {
        if (load)
            LoadPlayers();
        
        if(_gameManager.GetCurrentState() == GameState.Play)
            CheckRank();
    }

    private void LoadPlayers()
    {
        load = false;
        _opponents = FindObjectsOfType<MoveToGoalAgent>();
        _player = FindObjectOfType<InputManager>().GetComponent<Transform>();
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
