using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Created by Ramon Lopez - @RamonDev - npgdev@gmail.com

public class Tile : MonoBehaviour
{
    public int x;
    public int y;
    public bool occupied;
    [SerializeField]
    int signal;
    public int GetSignal()
    {
        return signal;
    }
    public BuildingType occupyingBuilding;
    public TileType type;
    public List<Tile> neighboringTiles = new List<Tile>();
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public bool isObjective;
    public int objectiveSignal;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }
    private void Start()
    {
        transform.position = new Vector3(x, y);
        transform.name = string.Format("Tile {0},{1}", x, y);
        signal = InitialSignal();
        Highlight(x, y);
        StopHighlight();
    }

    int InitialSignal()
    {
        switch (type)
        {
            case TileType.Plain: return 0;
            case TileType.Mountain: return -3;
            case TileType.City: return 1;
            case TileType.Water: return -1;
            case TileType.Woods: return -2;
        }
        return 0;
    }
   
    public void Highlight(int originX, int originY)
    {
        if (!CheckDistanceTransmissibility(originX,originY)) return;
      
        spriteRenderer.color = Color.green;
    }
    
    public void StopHighlight()
    {
        if (signal > 0 ) return;
        
        spriteRenderer.color = originalColor;
    }
    bool CheckTileTransmissibility(int x, int y)
    {
        if (LevelManager.instance.grid.tiles[x, y] == null) return false;
        return LevelManager.instance.grid.tiles[x, y].signal >= 0;
    }
    bool CheckDistanceTransmissibility(int originX, int originY)
    {
        int distanceX = Mathf.Abs(x - originX);
        int distanceY = Mathf.Abs(y - originY);
       
        if (y < originY)
        {
            for (int i = 1; i <= distanceY; i++)
            {
                if (!CheckTileTransmissibility(x, y + i))
                {
                    return false;
                }

            }
        }
        else if (x < originX)
        {
            for (int i = 1; i <= distanceX; i++)
            {
                if (!CheckTileTransmissibility(x + i, y))
                {
                    return false;
                }

            }
        }
        else if (x > originX)
        {
            for (int i = 1; i <= distanceX; i++)
            {
                if (!CheckTileTransmissibility(x - i, y))
                {
                    return false;
                }

            }

        }
        else if (y > originY)
        {
            for (int i = 1; i <= distanceY; i++)
            {
                if (!CheckTileTransmissibility(x, y - i))
                {
                    return false;
                }

            }

        }
        return true;
    }
    public void SetOccupyingBuilding(Building building)
    {
        occupyingBuilding = building.buildingType;
        occupied = true;
        signal += building.signalPower;
    }
    public void IncreaseSignal(int amount, int originX, int originY)
    {
        if(originX == x && originY == y)
        {
            signal += amount;
            return;
        }
        if (CheckDistanceTransmissibility(originX, originY))
        signal += amount;

        if(signal == objectiveSignal && isObjective)
        {
            DispatchGoalReachedEvent();
        }
    }
    public void DecreaseSignal(int amount)
    {
        signal -= amount;
    }
    void DispatchTentativePlacementRejectedEvent()
    {
        CodeControl.Message.Send<TentativePlacementRejectedEvent>(new TentativePlacementRejectedEvent());
    }
    void DispatchTentativePlacementEvent()
    {
        if (type == TileType.Mountain || type == TileType.Water) return;
        CodeControl.Message.Send<TentativePlacementEvent>(new TentativePlacementEvent(this));
    }
    void DispatchBuildingGrabbedEvent()
    {
        occupied = false;
        DecreaseSignal(1);
        StopHighlight();
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        CodeControl.Message.Send<GrabbedBuildingEvent>(new GrabbedBuildingEvent(this.occupyingBuilding));

    }
    private void OnMouseEnter()
    {
        DispatchTentativePlacementEvent();
    }
    private void OnMouseExit()
    {
        DispatchTentativePlacementRejectedEvent();
    }
    private void OnMouseDown()
    {
        if (occupied)
        {
            DispatchBuildingGrabbedEvent();
        }
    }

    void DispatchGoalReachedEvent()
    {
        CodeControl.Message.Send<TileGoalReachedEvent>(new TileGoalReachedEvent(this));
    }
}