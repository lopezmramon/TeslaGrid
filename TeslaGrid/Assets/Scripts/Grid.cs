using System;
using System.Collections.Generic;
using UnityEngine;

//Created by Ramon Lopez - @RamonDev - npgdev@gmail.com

public class Grid : MonoBehaviour
{
    public int sizeX;
    public int sizeY;
    public Tile[,] tiles;


    private void OnEnable()
    {
        
        
    }
    private void Start()
    {
        DetectGrid();
    }
    void DetectGrid()
    {
        tiles = new Tile[sizeX, sizeY];

        foreach (Tile tile in GetComponentsInChildren<Tile>())
        {
            tiles[tile.x, tile.y] = tile;
        }
        SetTileNeighbors();
        DispatchGridCreatedEvent();
    }

    private void DispatchGridCreatedEvent()
    {
        CodeControl.Message.Send<GridCreatedEvent>(new GridCreatedEvent(this));

    }

    void GenerateGrid()
    {

    }
    void SetTileNeighbors()
    {
        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                if (i == 0 && j == 0) // bottom left 
                {
                    tiles[i, j].neighboringTiles.Add(tiles[i, j + 1]);
                    tiles[i, j].neighboringTiles.Add(tiles[i + 1, j]);

                }
                else if (i == 0 && j > 0 && j < tiles.GetLength(1) - 1)// left upwards but not top left
                {
                    tiles[i, j].neighboringTiles.Add(tiles[i, j + 1]);
                    tiles[i, j].neighboringTiles.Add(tiles[i, j - 1]);
                    tiles[i, j].neighboringTiles.Add(tiles[i + 1, j + 1]);

                }
                else if (i > 0 && i < tiles.GetLength(0) - 1 && j > 0 && j < tiles.GetLength(1) - 1) //Center tiles
                {
                    tiles[i, j].neighboringTiles.Add(tiles[i, j - 1]);
                    tiles[i, j].neighboringTiles.Add(tiles[i, j + 1]);
                    tiles[i, j].neighboringTiles.Add(tiles[i - 1, j]);
                    tiles[i, j].neighboringTiles.Add(tiles[i + 1, j]);


                }
                else if (j == 0 && i > 0 && i < tiles.GetLength(0) - 1) //Center tiles
                {
                    tiles[i, j].neighboringTiles.Add(tiles[i, j + 1]);
                    tiles[i, j].neighboringTiles.Add(tiles[i - 1, j]);
                    tiles[i, j].neighboringTiles.Add(tiles[i + 1, j]);


                }
                else if (i > 0 && i < tiles.GetLength(0) - 1 && j == tiles.GetLength(1) - 1) //Center tiles
                {
                    tiles[i, j].neighboringTiles.Add(tiles[i, j - 1]);
                    tiles[i, j].neighboringTiles.Add(tiles[i - 1, j]);
                    tiles[i, j].neighboringTiles.Add(tiles[i + 1, j]);


                }
                else if (i == tiles.GetLength(0) - 1 && j == tiles.GetLength(1) - 1) // top right
                {
                    tiles[i, j].neighboringTiles.Add(tiles[i, j - 1]);
                    tiles[i, j].neighboringTiles.Add(tiles[i - 1, j]);
                }
                else if (i == tiles.GetLength(0) - 1 && j > 0 && j < tiles.GetLength(1) - 1) // right upwards but not top right
                {
                    tiles[i, j].neighboringTiles.Add(tiles[i, j + 1]);
                    tiles[i, j].neighboringTiles.Add(tiles[i, j - 1]);
                    tiles[i, j].neighboringTiles.Add(tiles[i - 1, j]);
                }
                else if (i == tiles.GetLength(0) - 1 && j == 0) // bottom right
                {
                    tiles[i, j].neighboringTiles.Add(tiles[i, j + 1]);
                    tiles[i, j].neighboringTiles.Add(tiles[i - 1, j]);
                }
                else if (i == 0 && j == tiles.GetLength(1) - 1) // top left
                {
                    tiles[i, j].neighboringTiles.Add(tiles[i, j - 1]);
                    tiles[i, j].neighboringTiles.Add(tiles[i + 1, j]);
                }
            }
        }
    }

   
}