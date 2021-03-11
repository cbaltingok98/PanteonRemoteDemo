using System;
using Enums;
using UnityEngine;

public class RankingManager : MonoBehaviour
{
    private MoveToGoalAgent[] opponents;
    private Transform player;

    private UIManager _uiManager;
    private GameManager _gameManager;
    private int currentRank;
    private void Awake()
    {
        opponents = FindObjectsOfType<MoveToGoalAgent>();
        player = FindObjectOfType<InputManager>().GetComponent<Transform>();
        _uiManager = FindObjectOfType<UIManager>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void FixedUpdate()
    {
        if(_gameManager.GetCurrentState() == GameState.Play)
            CheckRank();
    }

    private void CheckRank()
    {
        currentRank = 1;
        foreach (var opponent in opponents)
        {
            currentRank += opponent.transform.position.z > player.position.z ? 1 : 0;
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        _uiManager.UpdateRankText(currentRank.ToString());
    }
}
