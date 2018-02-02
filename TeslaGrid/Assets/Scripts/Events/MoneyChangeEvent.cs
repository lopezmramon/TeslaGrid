using System.Collections;
using UnityEngine;

public class MoneyChangeEvent : CodeControl.Message
{
    public int money;

    public MoneyChangeEvent(int money)
    {
        this.money = money;
    }


}