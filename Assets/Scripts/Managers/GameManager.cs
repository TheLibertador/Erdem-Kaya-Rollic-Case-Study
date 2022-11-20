using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    [NonSerialized] public bool gameStopped = true;
    [NonSerialized] public bool gameFinished = false;
    [NonSerialized] public bool retryLevel = false;
    public int currentLevel = 0;

    [NonSerialized] public float pitWaitTime = 1.5f;
    
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
        
    }

    private void Start()
    {
        LevelManager.Instance.GenerateLevel(currentLevel);
    }
    
}
