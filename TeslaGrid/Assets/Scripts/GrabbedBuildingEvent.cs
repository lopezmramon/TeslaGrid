using UnityEngine;

//Created by Ramon Lopez - @RamonDev - npgdev@gmail.com

public class GrabbedBuildingEvent : CodeControl.Message 
{
    BuildingType requestedBulding;
    public BuildingType GetBuilding()
    {
        return requestedBulding;
    }
    public GrabbedBuildingEvent (BuildingType requestedBulding)
    {
        this.requestedBulding = requestedBulding;
    }


}