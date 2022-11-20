using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameStartPanel;
    [SerializeField] private GameObject levelFailedPanel;
    [SerializeField] private GameObject levelPassedPanel;
    [SerializeField] private TextMeshProUGUI levelIndicatorText;
    public void OnStartGameClicked()
    {
        GameManager.Instance.gameStopped = false;
        gameStartPanel.SetActive(false);
        
    }

    public void OnNextLevelClicked()
    {
        GameManager.Instance.gameStopped = false;
        levelPassedPanel.SetActive(false);
    }

    public void OnRetryClicked()
    {
        GameManager.Instance.gameStopped = false;
        levelFailedPanel.SetActive(false);
    }

    public void UpdateLevelIndicatorPanel(int currentLevel, int nextLevel)
    {
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
