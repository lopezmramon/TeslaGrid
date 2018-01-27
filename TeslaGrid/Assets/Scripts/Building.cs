using System.Collections.Generic;
using UnityEngine;

//Created by Ramon Lopez - @RamonDev - npgdev@gmail.com

public class Building : MonoBehaviour
{
    public int range;
    public float cost;
    public BuildingType buildingType;
    public int signalPower;

    SpriteRenderer spriteRenderer;
    public List<Tile> tilesAffected = new List<Tile>();
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnDestroy()
    {
        foreach(Tile tile in tilesAffected)
        {
            tile.DecreaseSignal(1);
        }
    }
}