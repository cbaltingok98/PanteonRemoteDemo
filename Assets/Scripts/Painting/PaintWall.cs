using System.Collections;
using UnityEngine;

public class PaintWall : MonoBehaviour
{
    private GameManager _gameManager;
    private UIManager _uiManager;

    private float _paintedPercentage;
    private float _addValue;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _uiManager = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        _paintedPercentage = 0f;
        _addValue = 100f / gameObject.transform.childCount;
    }

    public void UpdatePercentage()
    {
        _paintedPercentage += _addValue;
        _uiManager.WallPercentUpdate(_paintedPercentage.ToString("0.00") + " %");
        CheckForFinish();
    }

    private void CheckForFinish()
    {
        if (_paintedPercentage >= 100f)
        {
            _gameManager.EndLevel();
        }
    }
}
