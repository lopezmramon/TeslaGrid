using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Created by Ramon Lopez - @RamonDev - npgdev@gmail.com

public class Tile : MonoBehaviour
{
    public int x;
    public int y;
    public bool occupied;
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
    public void HighlightNeighbors(int horizontalDepth, int verticalDepth)
    {
        for (int i = 1; i <= horizontalDepth; i++)
        {
            for (int j = 1; j <= verticalDepth; i++)
            {
                if (Grid.instance.tiles[x, y + j] != null) Grid.instance.tiles[x, y + j].Highlight();
                if (Grid.instance.tiles[x + i, y] != null) Grid.instance.tiles[x + i, y].Highlight();
                if (Grid.instance.tiles[x - i, y] != null) Grid.instance.tiles[x - i, y].Highlight();
                if (Grid.instance.tiles[x, y - j] != null) Grid.instance.tiles[x, y - j].Highlight();
            }
        }
    }
    public void Highlight()
    {
        if (signal + 1 <= 0) return;
        spriteRenderer.color = Color.green;
    }
    public void StopHighlight()
    {
        if (signal > 0) return;
        spriteRenderer.color = originalColor;
    }
    public void SetOccupyingBuilding(Building building)
    {
        occupyingBuilding = building.buildingType;
        occupied = true;
        if (building.buildingType == BuildingType.Antenna || building.buildingType == BuildingType.RepeaterAntenna)
        {
            IncreaseSignal(1);
        }
    }
    public void IncreaseSignal(int amount)
    {
        signal += amount;
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
}