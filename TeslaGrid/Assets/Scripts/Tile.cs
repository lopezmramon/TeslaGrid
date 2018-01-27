using System.Collections.Generic;
using UnityEngine;

//Created by Ramon Lopez - @RamonDev - npgdev@gmail.com

public class Tile : MonoBehaviour
{
    public int x;
    public int y;
    public bool occupied;
    public BuildingType occupyingBuilding;
    public List<Tile> neighboringTiles = new List<Tile>();

    public Tile(int x, int y, bool occupied, BuildingType occupyingBuilding, List<Tile> neighboringTiles)
    {
        this.x = x;
        this.y = y;
        this.occupied = occupied;
        this.occupyingBuilding = occupyingBuilding;
        this.neighboringTiles = neighboringTiles;
    }

    private void Start()
    {
        transform.position = new Vector3(x, y);
        transform.name = string.Format("Tile {0},{1}", x, y);
    }
}