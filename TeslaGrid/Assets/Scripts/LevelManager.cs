using System;
using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour 
{
    public Level[] levels;
    GameObject currentLevel;
    public Grid grid;
    public static LevelManager instance;
    private void Awake()
    {
        CodeControl.Message.AddListener<GenerateLevelRequestEvent>(OnGenerateLevelRequested);
        CodeControl.Message.AddListener<RemoveLevelRequestEvent>(OnRemoveLevelRequested);
        CodeControl.Message.AddListener<GridCreatedEvent>(OnGridCreated);
        instance = this;
    }

    private void OnGridCreated(GridCreatedEvent e)
    {
        this.grid = e.grid; 
    }
    private void OnRemoveLevelRequested(RemoveLevelRequestEvent obj)
    {
        Destroy(currentLevel);
    }

    private void OnGenerateLevelRequested(GenerateLevelRequestEvent obj)
    {
        PlayerPrefs.SetInt("CurrentLevel", obj.levelRequested);
        currentLevel =  Instantiate(levels[obj.levelRequested].gameObject);
    }
}