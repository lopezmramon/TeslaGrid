using System.Collections;
using UnityEngine;

public class TentativePlacementEvent : CodeControl.Message 
{

    Tile tile;

    public Tile GetTile()
    {
        return tile;
    }

    public TentativePlacementEvent(Tile tile)
    {
        this.tile = tile;
    }

}