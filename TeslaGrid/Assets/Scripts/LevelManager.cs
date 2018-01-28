using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Level[] levels;
    GameObject currentLevel;
    public GameObject gridPrefab;
    public Grid grid;
    public static LevelManager instance;
    int progress;
    bool random;
    RandomLevelRequest randomRequest;
    Building error;
    
    private void Awake()
    {
        progress = 0;
        CodeControl.Message.AddListener<GenerateLevelRequestEvent>(OnGenerateLevelRequested);
        CodeControl.Message.AddListener<RemoveLevelRequestEvent>(OnRemoveLevelRequested);
        CodeControl.Message.AddListener<GridCreatedEvent>(OnGridCreated);
        CodeControl.Message.AddListener<RandomLevelRequest>(OnRandomGenerateLevelRequested);
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

        error = FindObjectOfType<Building>();
        if(error !=null)
        Destroy(error.gameObject);
        if (currentLevel != null) Destroy(currentLevel);

        if (obj.retry)
        {
            currentLevel = Instantiate(levels[progress].gameObject);
            currentLevel.GetComponent<Grid>().DetectGrid();
        }
        else if (!obj.retry && !obj.random)
        {
            progress++;
            if (progress >= levels.Length)
            {
                OnRandomGenerateLevelRequested(new RandomLevelRequest(new Vector2(8, 8), 3, 1, 3, 5, 1400));
            }
            else
            {
                currentLevel = Instantiate(levels[progress].gameObject);
                currentLevel.GetComponent<Grid>().DetectGrid();
            }

        }
        else if (random)
        {
            OnRandomGenerateLevelRequested(randomRequest);
        }
    }

    private void OnRandomGenerateLevelRequested(RandomLevelRequest e)
    {
        if (currentLevel != null) Destroy(currentLevel);
        PlayerPrefs.SetInt("CurrentLevel", 1500);
        grid = Instantiate(gridPrefab).GetComponent<Grid>();
        currentLevel = grid.gameObject;
        grid.GenerateGrid(e.size, e.mountainAmount, e.waterAmount, e.woodsAmount, e.cityAmount, e.money);
        randomRequest = e;
        random = true;
    }


}