using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DrawerController : MonoBehaviour 
{
    Image image;
    private void Awake()
    {
        image = GetComponent<Image>();

    }
    
    private void OnEnable()
    {
       // image.color = new Color32((byte)Random.Range(0, 255), (byte)Random.Range(0, 255), (byte)Random.Range(0, 255), 235);
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