using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager Instance { get { return _instance; } }
    
    private const string levelsPath = "Assets/Resources/Levels";
    public List<string> Levels = new List<string>();
    
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
        
        Debug.Log(Levels.Count);
        if (currentLevel < Levels.Count)
        {
            GameObject instance = Instantiate(Resources.Load(String.Format("Levels/{0}", Levels[currentLevel]), typeof(GameObject))) as GameObject;
        }
        else
        { 
            var randomLevelIndex = Random.Range(0, Levels.Count);
            Debug.Log(randomLevelIndex);
            var instantiatedLevel = Instantiate(Resources.Load(String.Format("Levels/{0}", Levels[randomLevelIndex]), typeof(GameObject))) as GameObject;
        }
        
    }
}
