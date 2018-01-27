using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Created by Ramon Lopez - @RamonDev - npgdev@gmail.com

public class Tile : MonoBehaviour
{
    public int x;
    public int y;
    public bool occupied, hasSignal;
    public BuildingType occupyingBuilding;
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
    }

    private void OnMouseEnter()
    {
        DispatchTentativePlacementEvent();
    }
    private void OnMouseExit()
    {
        DispatchTentativePlacementRejectedEvent();
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
        spriteRenderer.color = Color.green;
    }
    public void StopHighlight()
    {
        spriteRenderer.color = originalColor;
    }

    void DispatchTentativePlacementRejectedEvent()
    {
        CodeControl.Message.Send<TentativePlacementRejectedEvent>(new TentativePlacementRejectedEvent());
    }
    void DispatchTentativePlacementEvent()
    {
        CodeControl.Message.Send<TentativePlacementEvent>(new TentativePlacementEvent(this));
    }
}