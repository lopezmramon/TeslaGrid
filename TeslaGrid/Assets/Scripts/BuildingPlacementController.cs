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

        tentativeTile.Highlight(tentativeTile.x, tentativeTile.y,false);
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
                    LevelManager.instance.grid.tiles[tentativeTile.x + i, tentativeTile.y].Highlight(tentativeTile.x, tentativeTile.y,false);
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
                if (tentativeTile.y + i < LevelManager.instance.grid.tiles.GetLength(0))
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
                    LevelManager.instance.grid.tiles[tentativeTile.x, tentativeTile.y - 2 * i].Highlight(tentativeTile.x, tentativeTile.y,true);
                    tentativeTileNeighbors.Add(LevelManager.instance.grid.tiles[tentativeTile.x, tentativeTile.y - 2 * i]);
                }
                if (tentativeTile.y + 2 * i < LevelManager.instance.grid.tiles.GetLength(0))
                {
                    LevelManager.instance.grid.tiles[tentativeTile.x, tentativeTile.y + 2 * i].Highlight(tentativeTile.x, tentativeTile.y,true);
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
                    LevelManager.instance.grid.tiles[tentativeTile.x - 2 * i, tentativeTile.y ].Highlight(tentativeTile.x, tentativeTile.y, true);
                    tentativeTileNeighbors.Add(LevelManager.instance.grid.tiles[tentativeTile.x - 2 * i, tentativeTile.y ]);
                }
                if (tentativeTile.x + 2 * i < LevelManager.instance.grid.tiles.GetLength(0))
                {
                    LevelManager.instance.grid.tiles[tentativeTile.x + 2 * i, tentativeTile.y ].Highlight(tentativeTile.x, tentativeTile.y, true);
                    tentativeTileNeighbors.Add(LevelManager.instance.grid.tiles[tentativeTile.x + 2 * i, tentativeTile.y ]);

                }


            }
        }

    }

    void RemoveLinearHighlights(Tile tile)
    {
        tentativeTileNeighbors.Clear();
        if (tile == null || tile.occupied) return;
        foreach (Tile t in LevelManager.instance.grid.tiles)
        {
            if (t.GetSignal() <= 0)
            {
                t.StopHighlight();
            }
        }
        /* for (int i = 0; i <= heldBuilding.GetComponent<Building>().range; i++)
         {
             if (tentativeTile.x - i >= 0)
             {
                 LevelManager.instance.grid.tiles[tentativeTile.x - i, tentativeTile.y].StopHighlight();
             }
             if (tentativeTile.y - i >= 0)
             {
                 LevelManager.instance.grid.tiles[tentativeTile.x, tentativeTile.y - i].StopHighlight();

             }
             if (tentativeTile.x + i < LevelManager.instance.grid.tiles.GetLength(0))
             {
                 LevelManager.instance.grid.tiles[tentativeTile.x + i, tentativeTile.y].StopHighlight();

             }
             if (tentativeTile.y + i < LevelManager.instance.grid.tiles.GetLength(1))
             {
                 LevelManager.instance.grid.tiles[tentativeTile.x, tentativeTile.y + i].StopHighlight();

             }

         }*/
    }


    private void Update()
    {
        if (!isBuildingHeld) return;
        if (heldBuilding == null) isBuildingHeld = false;
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
                tile.IncreaseSignal(1, tentativeTile.x, tentativeTile.y);
                heldBuilding.GetComponent<Building>().tilesAffected.Add(tile);

            }
        }

        tentativeTileNeighbors.Clear();
        this.heldBuilding = null;
        isBuildingHeld = false;
    }
}