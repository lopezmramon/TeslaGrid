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
    public Sprite shadowSprite;
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
        CreateShadow(grid.transform);
    }
    private void OnRemoveLevelRequested(RemoveLevelRequestEvent obj)
    {
        Destroy(currentLevel);
        CheckError();

    }
    void CheckError()
    {
        error = FindObjectOfType<Building>();
        if (error != null)
            Destroy(error.gameObject);
    }
    private void OnGenerateLevelRequested(GenerateLevelRequestEvent obj)
    {
        CheckError();
        if (currentLevel != null) Destroy(currentLevel);

        if (obj.retry)
        {
            if (progress >= levels.Length)
            {
                OnRandomGenerateLevelRequested(new RandomLevelRequest(new Vector2(8, 8), 3, 1, 3, 5, 1400));
                return;
            }
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
    void CreateShadow(Transform transform)
    {
       

        GameObject shadow = new GameObject();
        var shadowSprite = shadow.AddComponent<SpriteRenderer>();
        shadowSprite.sprite = this.shadowSprite;
        shadowSprite.sortingOrder = 0;
        shadow.transform.position = new Vector3(0, 1.24f);
        shadow.transform.localScale = new Vector3(0.7f, 0.7f);
        shadow.transform.SetParent(transform);
    }

}