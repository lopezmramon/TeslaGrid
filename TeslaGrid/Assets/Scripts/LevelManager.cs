using System;
using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Level[] levels;
    GameObject currentLevel;
    public GameObject gridPrefab;
    public Grid grid;
    public static LevelManager instance;
    private void Awake()
    {
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
        if (currentLevel != null) Destroy(currentLevel);
        PlayerPrefs.SetInt("CurrentLevel", obj.levelRequested);
        if (obj.levelRequested <= levels.Length)
        {
            currentLevel = Instantiate(levels[obj.levelRequested].gameObject);
            currentLevel.GetComponent<Grid>().DetectGrid();
        }
        else
        {
            OnRandomGenerateLevelRequested(new RandomLevelRequest(10, new Vector2(7, 7), 5, 1, 2, 3,2000));
        }
    }
    private void OnRandomGenerateLevelRequested(RandomLevelRequest e)
    {
        grid = Instantiate(gridPrefab).GetComponent<Grid>();
        currentLevel = grid.gameObject;
        grid.GenerateGrid(e.difficulty, e.size,e.mountainAmount,e.waterAmount,e.woodsAmount,e.cityAmount);
    }
}