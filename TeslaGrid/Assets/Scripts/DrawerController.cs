using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DrawerController : MonoBehaviour 
{
    bool open;
    public Button[] buttons;
    private void Start()
    {
        open = false;
        for(int i =0; i < buttons.Length; i++)
        {

        }
    }
    public void ToggleDrawer()
    {
        if (open)
        {

        }
        else
        {

        }
        open = !open;
    }
    public void GrabBuilding(int buildingIndex)
    {
        DispatchGrabbedBuildingEvent(buildingIndex);
    }
    public void DispatchGrabbedBuildingEvent(int requestedBuilding)
    {
        CodeControl.Message.Send<GrabbedBuildingEvent>(new GrabbedBuildingEvent(requestedBuilding));
    }
}