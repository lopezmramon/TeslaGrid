using System.Collections;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int moneyAmount;
    public LevelType levelType;
    public int coverTileAmount;
    public int specificTileX, specificTileY;
    public int specificTileSignalAmount;

    private void Start()
    {
        Grid.instance.tiles[specificTileX, specificTileY].isObjective = true;
        Grid.instance.tiles[specificTileX, specificTileY].objectiveSignal = specificTileSignalAmount;
        if(levelType == LevelType.CoverTiles)
        {

        }

    }
}

public enum LevelType
{
    CoverTiles,
    //SpecificTile,
    // SpecificArea, TODO
    SpecificTileSignal,

}