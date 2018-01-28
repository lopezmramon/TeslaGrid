using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Created by Ramon Lopez - @RamonDev - npgdev@gmail.com

public class Tile : MonoBehaviour
{
    public int x;
    public int y;
    [HideInInspector]
    public bool occupied;
    [SerializeField]
    int signal;
    public int GetSignal()
    {
        return signal;
    }
    public Sprite[] sprites;
    public BuildingType occupyingBuilding;
    public TileType type;
    public List<Tile> neighboringTiles = new List<Tile>();
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    [HideInInspector]
    public bool isObjective;
    [HideInInspector]
    public int objectiveSignal;
    Animator animator;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        originalColor = spriteRenderer.color;
    }
    private void Start()
    {
      


    }
    public void Initialize()
    {
        transform.position = new Vector3(x * 0.5f + y * -0.5f, x * 0.3f + y * 0.3f);
        transform.name = string.Format("Tile {0},{1}", x, y);
        signal = InitialSignal();
        Highlight(x, y, false);
        StopHighlight();
        spriteRenderer.sprite = sprites[(int)type];
        spriteRenderer.sortingOrder = 500 - y * 10 - x * 10;
        gameObject.AddComponent<PolygonCollider2D>();
        animator.SetBool("Online", signal > 0);
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
   
    public void Highlight(int originX, int originY, bool satellite)
    {
        
        if (!satellite && !CheckDistanceTransmissibility(originX,originY) || signal<0) return;

        animator.SetBool("Online", true);

    }
    
    public void StopHighlight()
    {
        if (signal > 0 ) return;
        animator.SetBool("Online", false);

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
    public void IncreaseSignal(int amount, int originX, int originY, bool satellite)
    {

        if(originX == x && originY == y)
        {
            signal += amount;
        }else if (satellite || CheckDistanceTransmissibility(originX, originY))
        {
            signal += amount;

        }
        animator.SetBool("Online", signal > 0);
        if (signal == objectiveSignal && isObjective)
        {
            DispatchGoalReachedEvent();
        }
        StopHighlight();
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
        CodeControl.Message.Send<GrabbedBuildingEvent>(new GrabbedBuildingEvent((int)this.occupyingBuilding));

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
    public void SetAsObjective()
    {
        isObjective = true;
        animator.SetBool("Objective", isObjective);
        originalColor = Color.red;
        spriteRenderer.color = originalColor;
    }
    void DispatchGoalReachedEvent()
    {
        CodeControl.Message.Send<TileGoalReachedEvent>(new TileGoalReachedEvent(this));
    }
}