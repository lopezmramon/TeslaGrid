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

        ViewChanger.instance.ChangeView(0);

    }
}