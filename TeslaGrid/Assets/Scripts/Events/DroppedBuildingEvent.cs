

//Created by Ramon Lopez - @RamonDev - npgdev@gmail.com

public class DroppedBuildingEvent : CodeControl.Message 
{
    public bool placed;
    public Tile tile;

    public DroppedBuildingEvent(bool placed, Tile tile)
    {
        this.placed = placed;
        this.tile = tile;
    }



}