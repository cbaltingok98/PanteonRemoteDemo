using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject wallFillPercentTxt;
    [SerializeField] private GameObject joystick;
    [SerializeField] private GameObject playerVictory;
    [SerializeField] private GameObject aiVictory;
    [SerializeField] private GameObject tutorial;
    [SerializeField] private GameObject paintTutorial;
    [SerializeField] private Text currentRank;
    [SerializeField] private Text finishRank;
    [SerializeField] private Text coin;
    private void Start()
    {
        StartLevel();
        if (PlayerPrefs.HasKey("coin"))
            coin.text = PlayerPrefs.GetInt("coin").ToString();
    }

    private void StartLevel()
    {
        IsActiveInGameUI(true);
        IsActiveMenuUI(false);
    }
    public void EndLevel(bool isPlayer)
    {
        IsActiveInGameUI(false);
        IsActiveMenuUI(true);
        
        GetFinishRank();
        playerVictory.SetActive(isPlayer);
        aiVictory.SetActive(!isPlayer);
    }

    public void AIVictory()
    {
        EndLevel(false);
    }


    private void IsActiveInGameUI(bool set)
    {
        inGameUI.SetActive(set);
    }

    private void IsActiveMenuUI(bool set)
    {
        menu.SetActive(set);
    }

    public void IsActiveWallPercent(bool set)
    {
        wallFillPercentTxt.SetActive(set);
        IsActivePaintTutorial(set);
    }

    public void WallPercentUpdate(string updatedText)
    {
        wallFillPercentTxt.GetComponentInChildren<Text>().text = updatedText;
    }

    private void IsActivePaintTutorial(bool set)
    {
        paintTutorial.SetActive(set);
    }

    public void UpdateRankText(string rank)
    {
        currentRank.text = rank;
    }

    private void GetFinishRank()
    {
        finishRank.text = "Rank #" + currentRank.text;
    }

    public void UpdateCoinText(int set)
    {
        coin.text = set.ToString();
    }

    public void PaintingJoystick()
    {
        joystick.SetActive(false);
    }

    public void SetTutorialUI(bool set)
    {
        tutorial.SetActive(set);
    }
}
