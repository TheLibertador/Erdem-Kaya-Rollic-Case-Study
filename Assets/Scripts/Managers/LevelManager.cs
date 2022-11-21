using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Security.Cryptography;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager Instance { get { return _instance; } }
    
    private const string levelsPath = "Assets/Resources/Levels";
    public List<string> Levels = new List<string>();
    public GameObject currentActiveLevel;
    private int tempLevelIndex;
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
        
        GetLevelPrefabs();
        
        
    }
    
    private void GetLevelPrefabs()
    {
        string [] files = Directory.GetFiles (levelsPath, "*.*");
        foreach (string sourceFile in files)
        {
            string fileName = Path.GetFileNameWithoutExtension(sourceFile);
            if (fileName.Contains(".meta") == false && fileName.Contains(".prefab") == false)
            {
                Levels.Add(fileName);
            }
           
        }
    }

    public void GenerateLevel(int currentLevel)
    {
        UIManager.Instance.UpdateLevelIndicatorPanel(GameManager.Instance.currentLevel);
        if (currentLevel < Levels.Count)
        {
            currentActiveLevel = Instantiate(Resources.Load(String.Format("Levels/{0}", Levels[currentLevel]), typeof(GameObject))) as GameObject;
        }
        else
        { 
            var randomLevelIndex = Random.Range(0, Levels.Count);
            tempLevelIndex = randomLevelIndex;
            Debug.Log(randomLevelIndex);
            currentActiveLevel = Instantiate(Resources.Load(String.Format("Levels/{0}", Levels[randomLevelIndex]), typeof(GameObject))) as GameObject;
        }
        
    }

    public void RetryLevel()
    {
        Destroy(currentActiveLevel);
        currentActiveLevel = Instantiate(Resources.Load(String.Format("Levels/{0}", Levels[tempLevelIndex]), typeof(GameObject))) as GameObject;
    }

    public void GenerateNextLevel(ref int currentLevel)
    {
        Destroy(currentActiveLevel);
        currentLevel++;
        GenerateLevel(currentLevel);
        PlayerPrefs.SetInt("CurrentLevel",currentLevel);
    }
}
