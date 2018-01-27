using UnityEngine;

//Created by Ramon Lopez - @RamonDev - npgdev@gmail.com

public class GrabbedBuildingEvent : CodeControl.Message 
{
    GameObject building;
    public GameObject GetBuilding()
    {
        return building;
    }
    public GrabbedBuildingEvent (GameObject building)
    {
        this.building = building;
    }


}