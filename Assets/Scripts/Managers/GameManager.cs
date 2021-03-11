using System;
using System.Collections;
using Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameState _gameState;
    private UIManager _uiManager;

    [SerializeField] private ParticleSystem winParticle;
    [SerializeField] private GameObject painter;
    [SerializeField] private GameObject paintWall;
    
    [SerializeField] private bool _useKeyboard;
    private int _coin;
    private void Awake()
    {
        _gameState = GameState.Pause;
        _uiManager = FindObjectOfType<UIManager>();
        
        painter.SetActive(false);
        paintWall.SetActive(false);

        if (!PlayerPrefs.HasKey("coin"))
            PlayerPrefs.SetInt("coin", 0);
        
    }

    private void Start()
    {
        _coin = PlayerPrefs.GetInt("coin");
        
#if UNITY_EDITOR
        _useKeyboard = true;
#else
        _useKeyboard = false;        
#endif
    }

    public bool IsKeyboard()
    {
        return _useKeyboard;
    }

    public GameState GetCurrentState()
    {
        return _gameState;
    }
    public void SetGameState(GameState state)
    {
        _gameState = state;
    }

    public void IsActivePainting(bool set)
    {
        paintWall.SetActive(set);
        painter.SetActive(set);
    }

    public void AIVictory()
    {
        _gameState = GameState.Finish;
        _uiManager.EndLevel(false);
    }

    public void EndLevel()
    {
        _gameState = GameState.Finish;
        StartCoroutine(nameof(LateFinish));
    }

    private IEnumerator LateFinish()
    {
        winParticle.Play();
        yield return new WaitForSeconds(2f);
        _uiManager.EndLevel(true);
    }

    public void PlayAgainBtn()
    {
        SceneManager.LoadScene(0);
    }

    [ContextMenu("Play State")]
    public void ChangeStatePlay()
    {
        _gameState = GameState.Play;
    }

    public void UpdateCoin(int set)
    {
        PlayerPrefs.SetInt("coin", PlayerPrefs.GetInt("coin") + set);
        _uiManager.UpdateCoinText(PlayerPrefs.GetInt("coin"));
    }
}
