using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour 
{
    private void Awake()
    {
        CodeControl.Message.AddListener<TileGoalReachedEvent>(OnTileGoalReached);
    }

    private void OnTileGoalReached(TileGoalReachedEvent obj)
    {

        ViewChanger.instance.ChangeView(4);
        CheckError();
    }
    void CheckError()
    {
       var  error = FindObjectOfType<Building>();
        if (error != null)
            Destroy(error.gameObject);
    }
}