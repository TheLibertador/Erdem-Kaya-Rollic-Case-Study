using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance; 
    public static UIManager Instance { get { return _instance; } }
    
    
    [SerializeField] private GameObject gameStartPanel;
    [SerializeField] private GameObject levelFailedPanel;
    [SerializeField] private GameObject levelPassedPanel;
    [SerializeField] private TextMeshProUGUI levelIndicatorText;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }

    }

    public void OnStartGameClicked()
    {
        GameManager.Instance.gameStopped = false;
        gameStartPanel.SetActive(false);
        
    }

    public void OnNextLevelClicked()
    {
        LevelManager.Instance.GenerateNextLevel(ref GameManager.Instance.currentLevel);
        levelPassedPanel.SetActive(false);
        GameManager.Instance.gameStopped = false;
    }

    public void OnRetryClicked()
    {
        LevelManager.Instance.RetryLevel();
        levelFailedPanel.SetActive(false);
        GameManager.Instance.gameStopped = false;
    }

    public void UpdateLevelIndicatorPanel(int currentLevel)
    {
        currentLevel += 1;
        var nextLevel = currentLevel + 1;
        levelIndicatorText.text = String.Format("{0} ------> {1}", currentLevel, nextLevel);
    }

    public void ActivateSuccessPanel()
    {
        levelPassedPanel.SetActive(true);
    }

    public void ActivateFailPanel()
    {
        levelFailedPanel.SetActive(true);
    }
    
}
