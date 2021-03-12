using System.Collections;
using Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameState _gameState;
    private UIManager _uiManager;
    private AudioManager _audioManager;
    
    [SerializeField] private ParticleSystem winParticle;
    [SerializeField] private GameObject painter;
    [SerializeField] private GameObject paintWall;
    
    [SerializeField] private bool _useKeyboard;
    private int _coin;
    private void Awake()
    {
        _gameState = GameState.Pause;
        _uiManager = FindObjectOfType<UIManager>();
        _audioManager = AudioManager.instance;
        
        painter.SetActive(false);
        paintWall.SetActive(false);

        if (!PlayerPrefs.HasKey("coin"))
            PlayerPrefs.SetInt("coin", 0);
        
    }

    private void Start()
    {
        _coin = PlayerPrefs.GetInt("coin");
        PlayGameTheme();
#if UNITY_EDITOR
        _useKeyboard = true;
        _uiManager.SetJoyStick(false);
#else
        _useKeyboard = false;        
#endif
    }

    public bool IsKeyboard()
    {
        return _useKeyboard;
    }
    
    private void PlayGameTheme()
    {
        _audioManager.SetVolume("GameTheme", 0.5f);
        _audioManager.Play("GameTheme");
    }

    private void GameOverSound()
    {
        _audioManager.SetVolume("GameTheme", 0.25f);
        _audioManager.Play("GameOver");
    }

    private void VictorySound()
    {
        _audioManager.SetVolume("GameTheme", 0.25f);
        _audioManager.Play("Victory");
    }

    public void DeathSound()
    {
        _audioManager.Play("Death");
    }

    private void CoinSound()
    {
        _audioManager.Play("Coin");
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
        GameOverSound();
    }

    public void EndLevel()
    {
        _gameState = GameState.Finish;
        StartCoroutine(nameof(LateFinish));
    }

    private IEnumerator LateFinish()
    {
        winParticle.Play();
        VictorySound();
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
        CoinSound();
    }
}
