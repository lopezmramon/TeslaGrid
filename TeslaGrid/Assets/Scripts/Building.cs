using System.Collections;
using UnityEngine;

//Created by Ramon Lopez - @RamonDev - npgdev@gmail.com

public class Building : MonoBehaviour
{
    public int range;
    public float cost;
    public BuildingType buildingType;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

}