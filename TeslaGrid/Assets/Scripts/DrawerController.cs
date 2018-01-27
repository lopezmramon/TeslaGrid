using System.Collections;
using UnityEngine;

public class DrawerController : MonoBehaviour 
{
    bool open;
    private void Start()
    {
        open = false;
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
    public void DispatchGrabbedBuildingEvent(GameObject gameObject)
    {
        CodeControl.Message.Send<GrabbedBuildingEvent>(new GrabbedBuildingEvent(gameObject));
    }
}