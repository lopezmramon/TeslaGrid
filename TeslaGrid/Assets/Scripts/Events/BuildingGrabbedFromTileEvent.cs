using System.Collections;
using UnityEngine;

public class BuildingGrabbedFromTileEvent : CodeControl.Message 
{
    int grabbedBuilding;
    public int GetBuilding()
    {
        return grabbedBuilding;
    }
    public BuildingGrabbedFromTileEvent(int requestedBulding)
    {
        this.grabbedBuilding = requestedBulding;
    }

}