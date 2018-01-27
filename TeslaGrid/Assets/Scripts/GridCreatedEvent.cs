using System.Collections;
using UnityEngine;

public class GridCreatedEvent : CodeControl.Message 
{
    public Grid grid;

    public GridCreatedEvent(Grid grid)
    {
        this.grid = grid;
    }


}