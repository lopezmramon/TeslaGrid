using System.Collections;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int money;
    public int repeaterCost, p2pAntennaCost, satelliteCost;
    public static ResourceManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Debug.LogWarning("More than one Resource Manager");
        CodeControl.Message.AddListener<DroppedBuildingEvent>(OnBuildingDropped);
    }

    void OnBuildingDropped(DroppedBuildingEvent e)
    {
        if (e.placed)
        {
            ChangeMoney(e.tile.occupyingBuilding, e.tile.type);
        }
    }
    public void AssignMoney(int totalMoneyAmount)
    {
        money = totalMoneyAmount;
        CodeControl.Message.Send<MoneyChangeEvent>(new MoneyChangeEvent(money));

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
        DispatchMoneyChangeEvent();
    }

    void DispatchMoneyChangeEvent()
    {
        CodeControl.Message.Send<MoneyChangeEvent>(new MoneyChangeEvent(money));
    }
}