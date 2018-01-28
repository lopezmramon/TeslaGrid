using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameplayView : MonoBehaviour 
{
    public Text moneyText;

    private void Awake()
    {
        CodeControl.Message.AddListener<MoneyChangeEvent>(OnMoneyChanged);
    }

    private void OnMoneyChanged(MoneyChangeEvent obj)
    {

        moneyText.text = obj.money.ToString();

    }

    public void RandomGame()
    {
        CodeControl.Message.Send<RandomLevelRequest>(new RandomLevelRequest(10, new Vector2(7, 7),5,1,2,3,2000));
    }
}