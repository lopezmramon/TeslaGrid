using System.Collections;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int money;
    public int repeaterCost, p2pAntennaCost, satelliteCost;
    private void Awake()
    {
        CodeControl.Message.AddListener<DroppedBuildingEvent>(OnBuildingDropped);
    }

    void OnBuildingDropped(DroppedBuildingEvent e)
    {
        if (e.placed)
        {
            ChangeMoney(e.tile.occupyingBuilding, e.tile.type);
        }
    }

    void ChangeMoney(BuildingType buildingType, TileType tileType)
    {
        int cost = 0;
        switch (buildingType)
        {
            case BuildingType.RepeaterAntenna:
                cost = repeaterCost;
                break;
            case BuildingType.P2PAntennaHorizontal:
                cost = p2pAntennaCost;
                break;
            case BuildingType.P2PAntennaVertical:
                cost = p2pAntennaCost;
                break;
            case BuildingType.SatelliteHorizontal:
                cost = satelliteCost;
                break;
        }
        if (tileType == TileType.Woods)
        {
            cost += 100;
        }
        money -= cost;
    }

    void DispatchMoneyChangeEvent()
    {
        CodeControl.Message.Send<MoneyChangeEvent>(new MoneyChangeEvent(money));
    }
}