using System;
using System.Collections;
using UnityEngine;

public class BuildingPlacementController : MonoBehaviour
{
    GameObject heldBuilding;
    Tile tentativeTile;
    private void Awake()
    {
        CodeControl.Message.AddListener<GrabbedBuildingEvent>(OnBuildingGrabbed);
        CodeControl.Message.AddListener<TentativePlacementEvent>(OnTentativePlacementFound);
    }

    private void OnBuildingGrabbed(GrabbedBuildingEvent obj)
    {
        this.heldBuilding = obj.GetBuilding();
    }

    private void OnTentativePlacementFound(TentativePlacementEvent obj)
    {
        this.tentativeTile = obj.GetTile();
        SetHighlights();
    }

    void SetHighlights()
    {
        if (heldBuilding == null) return;
        for (int i = 0; i == heldBuilding.GetComponent<Building>().range; i++)
        {
            

        }
    }
}