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
        MusicManager.instance.PlaySound(6);
        ViewChanger.instance.ChangeView(4);
        CheckError();
    }
    void CheckError()
    {
        var error = FindObjectsOfType<Building>();
        for (int i = 0; i < error.Length; i++)
        {
            Destroy(error[i].gameObject);
        }
    }
}