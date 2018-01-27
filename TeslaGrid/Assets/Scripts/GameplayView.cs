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
}