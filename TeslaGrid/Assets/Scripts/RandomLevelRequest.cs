using System.Collections;
using UnityEngine;

public class RandomLevelRequest : CodeControl.Message
{

    public int difficulty;
    public Vector2 size = new Vector2();
    public int woodsAmount;
    public int cityAmount;
    public int waterAmount;
    public int mountainAmount;
    public int money;
  
    public RandomLevelRequest(int difficulty, Vector2 size, int woodsAmount, int cityAmount, int waterAmount, int mountainAmount, int money)
    {
        this.difficulty = difficulty;
        this.size = size;
        this.woodsAmount = woodsAmount;
        this.cityAmount = cityAmount;
        this.waterAmount = waterAmount;
        this.mountainAmount = mountainAmount;
        this.money = money;
    }

}