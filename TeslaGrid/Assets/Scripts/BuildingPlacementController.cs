using System;
using System.Collections;
using UnityEngine;

public class BuildingPlacementController : MonoBehaviour
{
    GameObject heldBuilding;
    Tile tentativeTile;
    bool isBuildingHeld;
    private void Awake()
    {
        CodeControl.Message.AddListener<GrabbedBuildingEvent>(OnBuildingGrabbed);
        CodeControl.Message.AddListener<TentativePlacementEvent>(OnTentativePlacementFound);
        CodeControl.Message.AddListener<DroppedBuildingEvent>(OnBuildingDropped);
        CodeControl.Message.AddListener<TentativePlacementRejectedEvent>(OnTentativePlacementRejected);
    }

    private void OnBuildingDropped(DroppedBuildingEvent obj)
    {
        this.heldBuilding = null;
        isBuildingHeld = false;
    }

    private void OnBuildingGrabbed(GrabbedBuildingEvent obj)
    {
        this.heldBuilding = Instantiate(obj.GetBuilding());
        isBuildingHeld = true;
    }

    private void OnTentativePlacementFound(TentativePlacementEvent obj)
    {
        this.tentativeTile = obj.GetTile();
        if (isBuildingHeld) SetLinearHighlights();
    }

    private void OnTentativePlacementRejected(TentativePlacementRejectedEvent obj)
    {
        RemoveLinearHighlights(tentativeTile);
        this.tentativeTile = null;
    }
    void SetLinearHighlights()
    {
        for (int i = 0; i <= heldBuilding.GetComponent<Building>().range; i++)
        {
            if (tentativeTile.x - i >= 0)
            {
                Grid.instance.tiles[tentativeTile.x - i, tentativeTile.y].Highlight();
            }
            if (tentativeTile.y - i >= 0)
            {
                Grid.instance.tiles[tentativeTile.x, tentativeTile.y - i].Highlight();

            }
            if (tentativeTile.x + i < Grid.instance.tiles.GetLength(0))
            {
                Grid.instance.tiles[tentativeTile.x + i, tentativeTile.y].Highlight();

            }
            if (tentativeTile.y + i < Grid.instance.tiles.GetLength(1))
            {
                Grid.instance.tiles[tentativeTile.x, tentativeTile.y + i].Highlight();

            }

        }
    }

    void RemoveLinearHighlights(Tile tile)
    {
        for (int i = 0; i <= heldBuilding.GetComponent<Building>().range; i++)
        {
            if (tentativeTile.x - i >= 0)
            {
                Grid.instance.tiles[tentativeTile.x - i, tentativeTile.y].StopHighlight();
            }
            if (tentativeTile.y - i >= 0)
            {
                Grid.instance.tiles[tentativeTile.x, tentativeTile.y - i].StopHighlight();

            }
            if (tentativeTile.x + i < Grid.instance.tiles.GetLength(0))
            {
                Grid.instance.tiles[tentativeTile.x + i, tentativeTile.y].StopHighlight();

            }
            if (tentativeTile.y + i < Grid.instance.tiles.GetLength(1))
            {
                Grid.instance.tiles[tentativeTile.x, tentativeTile.y + i].StopHighlight();

            }

        }
    }


    private void Update()
    {
        if (heldBuilding == null) return;
        heldBuilding.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}