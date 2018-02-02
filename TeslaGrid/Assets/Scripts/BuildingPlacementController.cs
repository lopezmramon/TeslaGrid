using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class BuildingPlacementController : MonoBehaviour
{
    GameObject heldBuilding;
    public GameObject[] buildings;
    Tile tentativeTile;
    List<Tile> tentativeTileNeighbors = new List<Tile>();
    public static bool isBuildingHeld;
    private void Awake()
    {
        CodeControl.Message.AddListener<GrabbedBuildingEvent>(OnBuildingGrabbed);
        CodeControl.Message.AddListener<TentativePlacementEvent>(OnTentativePlacementFound);
        CodeControl.Message.AddListener<TentativePlacementRejectedEvent>(OnTentativePlacementRejected);
        CodeControl.Message.AddListener<BuildingGrabbedFromTileEvent>(OnBuildingGrabbedFromTile);
    }

    private void OnBuildingGrabbedFromTile(BuildingGrabbedFromTileEvent obj)
    {
        StartCoroutine(GenerateBuilding((int)obj.GetBuilding()));

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
        if (isBuildingHeld) RemoveLinearHighlights(tentativeTile);
        this.tentativeTile = null;
    }
    void SetLinearHighlights()
    {
        if (tentativeTile.GetSignal() <= 0)
        {
            return;
        }
        int range = heldBuilding.GetComponent<Building>().range;

        tentativeTile.Highlight(tentativeTile.x, tentativeTile.y, false);
        if (heldBuilding.GetComponent<Building>().buildingType == BuildingType.RepeaterAntenna)
        {
            for (int i = 1; i <= range; i++)
            {
                if (tentativeTile.x - i >= 0)
                {
                    LevelManager.instance.grid.tiles[tentativeTile.x - i, tentativeTile.y].Highlight(tentativeTile.x, tentativeTile.y, false);
                    tentativeTileNeighbors.Add(LevelManager.instance.grid.tiles[tentativeTile.x - i, tentativeTile.y]);
                }
                if (tentativeTile.y - i >= 0)
                {
                    LevelManager.instance.grid.tiles[tentativeTile.x, tentativeTile.y - i].Highlight(tentativeTile.x, tentativeTile.y, false);
                    tentativeTileNeighbors.Add(LevelManager.instance.grid.tiles[tentativeTile.x, tentativeTile.y - i]);

                }
                if (tentativeTile.x + i < LevelManager.instance.grid.tiles.GetLength(0))
                {
                    LevelManager.instance.grid.tiles[tentativeTile.x + i, tentativeTile.y].Highlight(tentativeTile.x, tentativeTile.y, false);
                    tentativeTileNeighbors.Add(LevelManager.instance.grid.tiles[tentativeTile.x + i, tentativeTile.y]);

                }
                if (tentativeTile.y + i < LevelManager.instance.grid.tiles.GetLength(1))
                {
                    LevelManager.instance.grid.tiles[tentativeTile.x, tentativeTile.y + i].Highlight(tentativeTile.x, tentativeTile.y, false);
                    tentativeTileNeighbors.Add(LevelManager.instance.grid.tiles[tentativeTile.x, tentativeTile.y + i]);


                }

            }
        }
        else if (heldBuilding.GetComponent<Building>().buildingType == BuildingType.P2PAntennaHorizontal)
        {
            for (int i = 1; i <= range; i++)
            {
                if (tentativeTile.x - i >= 0)
                {
                    LevelManager.instance.grid.tiles[tentativeTile.x - i, tentativeTile.y].Highlight(tentativeTile.x, tentativeTile.y, false);
                    tentativeTileNeighbors.Add(LevelManager.instance.grid.tiles[tentativeTile.x - i, tentativeTile.y]);
                }
                if (tentativeTile.x - 2 * i >= 0)
                {
                    LevelManager.instance.grid.tiles[tentativeTile.x - 2 * i, tentativeTile.y].Highlight(tentativeTile.x, tentativeTile.y, false);
                    tentativeTileNeighbors.Add(LevelManager.instance.grid.tiles[tentativeTile.x - 2 * i, tentativeTile.y]);
                }
                if (tentativeTile.x + i < LevelManager.instance.grid.tiles.GetLength(0))
                {
                    LevelManager.instance.grid.tiles[tentativeTile.x + i, tentativeTile.y].Highlight(tentativeTile.x, tentativeTile.y, false);
                    tentativeTileNeighbors.Add(LevelManager.instance.grid.tiles[tentativeTile.x + i, tentativeTile.y]);

                }


            }
        }
        else if (heldBuilding.GetComponent<Building>().buildingType == BuildingType.P2PAntennaVertical)
        {

            for (int i = 1; i <= range; i++)
            {
                if (tentativeTile.y - i >= 0)
                {
                    LevelManager.instance.grid.tiles[tentativeTile.x, tentativeTile.y - i].Highlight(tentativeTile.x, tentativeTile.y, false);
                    tentativeTileNeighbors.Add(LevelManager.instance.grid.tiles[tentativeTile.x, tentativeTile.y - i]);
                }
                if (tentativeTile.y - 2 * i >= 0)
                {
                    LevelManager.instance.grid.tiles[tentativeTile.x, tentativeTile.y - 2 * i].Highlight(tentativeTile.x, tentativeTile.y, false);
                    tentativeTileNeighbors.Add(LevelManager.instance.grid.tiles[tentativeTile.x, tentativeTile.y - 2 * i]);
                }
                if (tentativeTile.y + i < LevelManager.instance.grid.tiles.GetLength(1))
                {
                    LevelManager.instance.grid.tiles[tentativeTile.x, tentativeTile.y + i].Highlight(tentativeTile.x, tentativeTile.y, false);
                    tentativeTileNeighbors.Add(LevelManager.instance.grid.tiles[tentativeTile.x, tentativeTile.y + i]);

                }


            }
        }
        else if (heldBuilding.GetComponent<Building>().buildingType == BuildingType.SatelliteVertical)
        {
            for (int i = 1; i <= range; i++)
            {

                if (tentativeTile.y - 2 * i >= 0)
                {
                    LevelManager.instance.grid.tiles[tentativeTile.x, tentativeTile.y - 2 * i].Highlight(tentativeTile.x, tentativeTile.y, true);
                    tentativeTileNeighbors.Add(LevelManager.instance.grid.tiles[tentativeTile.x, tentativeTile.y - 2 * i]);
                }
                if (tentativeTile.y + 2 * i < LevelManager.instance.grid.tiles.GetLength(1))
                {
                    LevelManager.instance.grid.tiles[tentativeTile.x, tentativeTile.y + 2 * i].Highlight(tentativeTile.x, tentativeTile.y, true);
                    tentativeTileNeighbors.Add(LevelManager.instance.grid.tiles[tentativeTile.x, tentativeTile.y + 2 * i]);

                }


            }
        }
        else if (heldBuilding.GetComponent<Building>().buildingType == BuildingType.SatelliteHorizontal)
        {
            for (int i = 1; i <= range; i++)
            {

                if (tentativeTile.x - 2 * i >= 0)
                {
                    LevelManager.instance.grid.tiles[tentativeTile.x - 2 * i, tentativeTile.y].Highlight(tentativeTile.x, tentativeTile.y, true);
                    tentativeTileNeighbors.Add(LevelManager.instance.grid.tiles[tentativeTile.x - 2 * i, tentativeTile.y]);
                }
                if (tentativeTile.x + 2 * i < LevelManager.instance.grid.tiles.GetLength(0))
                {
                    LevelManager.instance.grid.tiles[tentativeTile.x + 2 * i, tentativeTile.y].Highlight(tentativeTile.x, tentativeTile.y, true);
                    tentativeTileNeighbors.Add(LevelManager.instance.grid.tiles[tentativeTile.x + 2 * i, tentativeTile.y]);

                }


            }
        }

    }

    void RemoveLinearHighlights(Tile tile)
    {
        tentativeTileNeighbors.Clear();
        if (tile == null || tile.GetIsOccupied()) return;
        foreach (Tile t in LevelManager.instance.grid.tiles)
        {
            if (t.GetSignal() <= 0)
            {
                t.StopHighlight();
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
        Building building = heldBuilding.GetComponent<Building>();

        if (tentativeTile == null ||tentativeTile.GetIsOccupied() || tentativeTile.GetSignal() < 1)
        {
            DisregardHeldBuilding();
            return;
        }
        else if (ResourceManager.instance.money < building.cost)
        {
            DispatchNotEnoughMoneyEvent();
            DisregardHeldBuilding();
            return;
        }
        else
        {
            tentativeTile.SetOccupyingBuilding(heldBuilding.GetComponent<Building>());
            if (building.buildingType == BuildingType.P2PAntennaHorizontal || building.buildingType == BuildingType.P2PAntennaVertical)
            {
                MusicManager.instance.PlaySound(0);
            }
            else if (building.buildingType == BuildingType.SatelliteHorizontal || building.buildingType == BuildingType.SatelliteVertical)
            {
                MusicManager.instance.PlaySound(1);
            }
            else if (building.buildingType == BuildingType.RepeaterAntenna)
            {
                MusicManager.instance.PlaySound(2);
            }
            heldBuilding.transform.SetParent(tentativeTile.transform);
            heldBuilding.transform.localPosition = Vector3.zero;

            foreach (Tile tile in tentativeTileNeighbors)
            {
                tile.IncreaseSignal(1, tentativeTile.x, tentativeTile.y, building.buildingType == BuildingType.SatelliteHorizontal || heldBuilding.GetComponent<Building>().buildingType == BuildingType.SatelliteVertical ? true : false);
                heldBuilding.GetComponent<Building>().tilesAffected.Add(tile);

            }
            CodeControl.Message.Send<DroppedBuildingEvent>(new DroppedBuildingEvent(true, tentativeTile));
            isBuildingHeld = false;

            heldBuilding = null;
            return;
        }

        //  tentativeTileNeighbors.Clear();
        // DisregardHeldBuilding();
    }
    void DisregardHeldBuilding()
    {
        Destroy(heldBuilding.gameObject);
        heldBuilding = null;
        isBuildingHeld = false;
    }
    void DispatchNotEnoughMoneyEvent()
    {
        Destroy(heldBuilding);
        heldBuilding = null;
        CodeControl.Message.Send<NotEnoughMoneyEvent>(new NotEnoughMoneyEvent());

    }
}