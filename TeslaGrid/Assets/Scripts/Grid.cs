using System.Collections.Generic;
using UnityEngine;

//Created by Ramon Lopez - @RamonDev - npgdev@gmail.com

public class Grid : MonoBehaviour
{
    public int sizeX;
    public int sizeY;
    public Tile[,] tiles;
    public GameObject tilePrefab;
    Tile cityTile;
    public void DetectGrid()
    {
        tiles = new Tile[sizeX, sizeY];

        foreach (Tile tile in GetComponentsInChildren<Tile>())
        {
            tiles[tile.x, tile.y] = tile;
            tile.Initialize();
        }
        SetTileNeighbors();
        DispatchGridCreatedEvent();
    }

    private void DispatchGridCreatedEvent()
    {
        CodeControl.Message.Send<GridCreatedEvent>(new GridCreatedEvent(this));

    }

    public void GenerateGrid(Vector2 size, int maxMountains, int maxWater, int maxWoods, int maxCities, int money)
    {
        Level level = null;
        if (GetComponent<Level>() == null)
        {
            level = gameObject.AddComponent<Level>();
        }
        else level = GetComponent<Level>();

        level.levelType = LevelType.SpecificTileSignal;
        level.moneyAmount = money;



        tiles = new Tile[(int)size.x, (int)size.y];
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                GameObject tileObject = Instantiate(tilePrefab, this.transform);
                tiles[i, j] = tileObject.GetComponent<Tile>();
                tiles[i, j].x = i;
                tiles[i, j].y = j;
            }
        }
        int woodsChance = Random.Range(0, randomModifier(maxWoods));

        int waterChance = Random.Range(0, randomModifier(maxWater));
        int mountainChance = Random.Range(0, randomModifier(maxMountains));

        int cityAmount = 0;
        int mountainAmount = 0;
        int woodsAmount = 0;
        int waterAmount = 0;
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                int randomGeneration = Random.Range(1, 10);
                if (randomGeneration < woodsChance && woodsAmount < maxWoods)
                {
                    tiles[i, j].type = TileType.Woods;
                    woodsAmount++;
                }
                else if (randomGeneration < waterChance && waterAmount < maxWater)
                {
                    tiles[i, j].type = TileType.Water;
                    waterAmount++;

                }
                else if (randomGeneration < mountainChance && mountainAmount < maxMountains)
                {
                    tiles[i, j].type = TileType.Mountain;
                    mountainAmount++;

                }
                else
                {
                    tiles[i, j].type = TileType.Plain;
                }

                tiles[i, j].Initialize();

            }
        }
        cityTile = null;
        while (cityAmount < maxCities)
        {
            Vector2Int randomForCities = RollRandomForAdjustments();
            while (tiles[randomForCities.x, randomForCities.y].type != TileType.Plain)
            {
                randomForCities = RollRandomForAdjustments();

            }
            tiles[randomForCities.x, randomForCities.y].type = TileType.City;
            tiles[randomForCities.x, randomForCities.y].Initialize();
            cityTile = tiles[randomForCities.x, randomForCities.y];
            cityAmount++;

        }

        while (woodsAmount < maxWoods)
        {
            Vector2Int randomForWoods = RollRandomForAdjustments();
            while (tiles[randomForWoods.x, randomForWoods.y].type != TileType.Plain)
            {
                randomForWoods = RollRandomForAdjustments();

            }
            tiles[randomForWoods.x, randomForWoods.y].type = TileType.Woods;
            tiles[randomForWoods.x, randomForWoods.y].Initialize();

            woodsAmount++;
        }
        while (waterAmount < maxWater)
        {
            Vector2Int randomForWater = RollRandomForAdjustments();
            while (tiles[randomForWater.x, randomForWater.y].type != TileType.Plain)
            {
                randomForWater = RollRandomForAdjustments();

            }
            tiles[randomForWater.x, randomForWater.y].type = TileType.Water;
            tiles[randomForWater.x, randomForWater.y].Initialize();

            waterAmount++;
        }
        while (mountainAmount < maxMountains)
        {
            Vector2Int randomForMountains = RollRandomForAdjustments();
            while (tiles[randomForMountains.x, randomForMountains.y].type != TileType.Plain)
            {
                randomForMountains = RollRandomForAdjustments();

            }
            tiles[randomForMountains.x, randomForMountains.y].type = TileType.Mountain;
            tiles[randomForMountains.x, randomForMountains.y].Initialize();

            mountainAmount++;
        }
      /*  Vector2Int randomForObjective = RollRandomForAdjustments();
        bool nearCity = true;
        for (int attempts = 0; attempts < 40; attempts++)
        {
            randomForObjective = RollRandomForAdjustments();

            nearCity = CheckIfTileIsFarEnoughFromCity(randomForObjective.x, randomForObjective.y, (int)size.x - 3);
            if (!nearCity && tiles[randomForObjective.x, randomForObjective.y].type != TileType.City)
            {
                attempts = 40;
            }
        }*/

        Vector2Int objectiveTileCoordinates = ChooseObjectiveTileRandomly();

        level.specificTileX = objectiveTileCoordinates.x;
        level.specificTileY = objectiveTileCoordinates.y;
        level.specificTileSignalAmount = 1;
        StartCoroutine(level.SetObjectives());


    }

    Vector2Int RollRandomForAdjustments()
    {
        int randomXforObjective = Random.Range(0, tiles.GetLength(0));
        int randomYforObjective = Random.Range(0, tiles.GetLength(1));
        return new Vector2Int(randomXforObjective, randomYforObjective);

    }
    private void SetTileNeighbors()
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

    private int randomModifier(int maxAmount)
    {
        return Mathf.RoundToInt(maxAmount / Random.Range(1, 2));
    }
    bool CheckIfTileIsCity(int x, int y)
    {
        return (tiles[x, y].type == TileType.City);
    }

    bool CheckIfTileIsFarEnoughFromCity(int x, int y, int distance)
    {
        bool near = true;
        int cityTileX = cityTile.x;
        int cityTileY = cityTile.y;
        if (Mathf.Abs(cityTileX - x) >= distance || Mathf.Abs(cityTileY - y) >= distance)
        {
            near = false;
        }
        return near;

    }

    Vector2Int ChooseObjectiveTileRandomly()
    {
        int randomDirection = (int)Mathf.Sign(Mathf.Sin(Random.Range(0, 360) * Mathf.Deg2Rad));
        int distance = Mathf.RoundToInt(tiles.GetLength(0) / 2);
     
        int objectiveX = cityTile.x + distance * randomDirection;

        int objectiveY = cityTile.y + distance * randomDirection;

        if (objectiveX < 0)
        {
            objectiveX = cityTile.x - distance * randomDirection;

        }
         if (objectiveX >= tiles.GetLength(0))
        {
            objectiveX = cityTile.x - distance * randomDirection;

        }
        if (objectiveY < 0)
        {
            objectiveY = cityTile.y - distance * randomDirection;
        }
        if (objectiveY >= tiles.GetLength(1))
        {
            objectiveY = cityTile.y - distance * randomDirection;

        }
        if(cityTile.y - distance * randomDirection < 0 && cityTile.y - distance * randomDirection >= tiles.GetLength(1))
        {
            objectiveY = cityTile.y + (distance-1) * randomDirection;

        }
        if (cityTile.x - distance * randomDirection < 0 && cityTile.x - distance * randomDirection >= tiles.GetLength(0))
        {
            objectiveX = cityTile.x + (distance - 1) * randomDirection;

        }
        return new Vector2Int(objectiveX, objectiveY);
    }

}