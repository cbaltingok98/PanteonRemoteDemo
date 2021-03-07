using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject wallFillPercentTxt;
    
    private void Start()
    {
        StartLevel();
    }

    public void StartLevel()
    {
        IsActiveInGameUI(true);
        IsActiveMenuUI(false);
    }
    public void EndLevel()
    {
        IsActiveInGameUI(false);
        IsActiveMenuUI(true);
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
}
