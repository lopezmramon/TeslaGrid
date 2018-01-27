using UnityEngine;

//Created by Ramon Lopez - @RamonDev - npgdev@gmail.com

public class GrabbedBuildingEvent : CodeControl.Message 
{
    int requestedBulding;
    public int GetBuilding()
    {
        return requestedBulding;
    }
    public GrabbedBuildingEvent (int requestedBulding)
    {
        this.requestedBulding = requestedBulding;
    }


}