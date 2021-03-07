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
    
    private void Awake()
    {
        _gameState = GameState.Pause;
        _uiManager = FindObjectOfType<UIManager>();
        
        painter.SetActive(false);
        paintWall.SetActive(false);
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

    public void EndLevel()
    {
        _gameState = GameState.Finish;
        StartCoroutine(nameof(LateFinish));
    }

    private IEnumerator LateFinish()
    {
        winParticle.Play();
        yield return new WaitForSeconds(2f);
        _uiManager.EndLevel();
    }

    public void PlayAgainBtn()
    {
        SceneManager.LoadScene(0);
    }
}
