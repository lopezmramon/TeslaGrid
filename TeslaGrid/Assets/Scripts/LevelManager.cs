using System;
using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour 
{
    public Level[] levels;
    GameObject currentLevel;

    private void Awake()
    {
        CodeControl.Message.AddListener<GenerateLevelRequestEvent>(OnGenerateLevelRequested);
        CodeControl.Message.AddListener<RemoveLevelRequestEvent>(OnRemoveLevelRequested);
    }

    private void OnRemoveLevelRequested(RemoveLevelRequestEvent obj)
    {
        Destroy(currentLevel);
    }

    private void OnGenerateLevelRequested(GenerateLevelRequestEvent obj)
    {
        currentLevel =  Instantiate(levels[obj.levelRequested].gameObject);
    }
}