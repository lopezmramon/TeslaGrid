using System.Collections;
using UnityEngine;

public class TileGoalReachedEvent : CodeControl.Message
{

    public Tile tile;
    public TileGoalReachedEvent(Tile tile)
    {
        this.tile = tile;
    }

}