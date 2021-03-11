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
    [SerializeField] private Text currentRank;
    [SerializeField] private Text finishRank;
    private void Start()
    {
        StartLevel();
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
    }

    public void WallPercentUpdate(string updatedText)
    {
        wallFillPercentTxt.GetComponentInChildren<Text>().text = updatedText;
    }

    public void UpdateRankText(string rank)
    {
        currentRank.text = rank;
    }

    private void GetFinishRank()
    {
        finishRank.text = "Rank #" + currentRank.text;
    }

    public void PaintingJoystick()
    {
        joystick.GetComponent<Image>().enabled = false;
    }
}
