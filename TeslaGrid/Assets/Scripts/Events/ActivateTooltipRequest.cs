using System.Collections;
using UnityEngine;

public class ActivateTooltipRequest : CodeControl.Message 
{
    public Tile tile;

    public ActivateTooltipRequest(Tile tile)
    {
        this.tile = tile;
    }




}