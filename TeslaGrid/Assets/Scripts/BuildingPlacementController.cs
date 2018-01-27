using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class BuildingPlacementController : MonoBehaviour
{
    GameObject heldBuilding;
    public GameObject[] buildings;
    Tile tentativeTile;
    bool isBuildingHeld;
    List<Tile> tentativeTileNeighbors = new List<Tile>();
    private void Awake()
    {
        CodeControl.Message.AddListener<GrabbedBuildingEvent>(OnBuildingGrabbed);
        CodeControl.Message.AddListener<TentativePlacementEvent>(OnTentativePlacementFound);
        CodeControl.Message.AddListener<TentativePlacementRejectedEvent>(OnTentativePlacementRejected);
    }

    private void OnBuildingGrabbed(GrabbedBuildingEvent obj)
    {
        StartCoroutine(GenerateBuilding((int)obj.GetBuilding()));
    }

    private void OnTentativePlacementFound(TentativePlacementEvent obj)
    {
        this.tentativeTile = obj.GetTile();
        if (isBuildingHeld) SetLinearHighlights();
    }

    private void OnTentativePlacementRejected(TentativePlacementRejectedEvent obj)
    {
        if(isBuildingHeld) RemoveLinearHighlights(tentativeTile);
        this.tentativeTile = null;
    }
    void SetLinearHighlights()
    {
        for (int i = 0; i <= heldBuilding.GetComponent<Building>().range; i++)
        {
            if (tentativeTile.x - i >= 0)
            {
                Grid.instance.tiles[tentativeTile.x - i, tentativeTile.y].Highlight();
                tentativeTileNeighbors.Add(Grid.instance.tiles[tentativeTile.x - i, tentativeTile.y]);
            }
            if (tentativeTile.y - i >= 0)
            {
                Grid.instance.tiles[tentativeTile.x, tentativeTile.y - i].Highlight();
                tentativeTileNeighbors.Add(Grid.instance.tiles[tentativeTile.x, tentativeTile.y - i]);

            }
            if (tentativeTile.x + i < Grid.instance.tiles.GetLength(0))
            {
                Grid.instance.tiles[tentativeTile.x + i, tentativeTile.y].Highlight();
                tentativeTileNeighbors.Add(Grid.instance.tiles[tentativeTile.x + i, tentativeTile.y]);

            }
            if (tentativeTile.y + i < Grid.instance.tiles.GetLength(1))
            {
                Grid.instance.tiles[tentativeTile.x, tentativeTile.y + i].Highlight();
                tentativeTileNeighbors.Add(Grid.instance.tiles[tentativeTile.x, tentativeTile.y + i]);


            }

        }
    }

    void RemoveLinearHighlights(Tile tile)
    {
        tentativeTileNeighbors.Clear();
        if (tile == null || tile.occupied) return;
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
        if (!isBuildingHeld) return;
        heldBuilding.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            DispatchBuildingDroppedEvent();
        }
    }
    IEnumerator GenerateBuilding(int buildingType)
    {
        this.heldBuilding = Instantiate(buildings[buildingType]);
        yield return new WaitForEndOfFrame();
        isBuildingHeld = true;
     
        yield return null;
    }
    void DispatchBuildingDroppedEvent()
    {
        if (tentativeTile == null)
        {
            Destroy(heldBuilding.gameObject);

        }
        else
        {
            tentativeTile.SetOccupyingBuilding(heldBuilding.GetComponent<Building>());
            heldBuilding.transform.SetParent(tentativeTile.transform);
            foreach (Tile tile in tentativeTileNeighbors)
            {
                tile.IncreaseSignal(1);
                heldBuilding.GetComponent<Building>().tilesAffected.Add(tile);

            }
        }
        
        tentativeTileNeighbors.Clear();
        this.heldBuilding = null;
        isBuildingHeld = false;
    }
}