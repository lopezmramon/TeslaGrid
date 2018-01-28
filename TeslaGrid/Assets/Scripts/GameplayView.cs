using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameplayView : MonoBehaviour
{
    public Text moneyText;
    public Text notEnoughMoneyText;
    public Button retryButton;
    bool randomLevel;
    private void Awake()
    {
        CodeControl.Message.AddListener<MoneyChangeEvent>(OnMoneyChanged);
        CodeControl.Message.AddListener<NotEnoughMoneyEvent>(OnNotEnoughMoney);
        CodeControl.Message.AddListener<RandomLevelRequest>(OnRandomLevelRequested);
    }

    private void OnRandomLevelRequested(RandomLevelRequest obj)
    {

        retryButton.gameObject.SetActive(false);
        randomLevel = true;

    }
    private void OnEnable()
    {
        retryButton.gameObject.SetActive(true);
        randomLevel = false;
    }
    private void Start()
    {
        moneyText.text = "Cash: $" + ResourceManager.instance.money.ToString();
    }
    void OnNotEnoughMoney(NotEnoughMoneyEvent obj)
    {
        StartCoroutine(NotEnoughMoney());
        if (!randomLevel)
        {
            retryButton.gameObject.SetActive(true);

        }

    }

    IEnumerator NotEnoughMoney()
    {
        notEnoughMoneyText.text = "Not Enough Money to build";
        yield return new WaitForSeconds(2.5f);
        notEnoughMoneyText.text = "";

    }
    private void OnMoneyChanged(MoneyChangeEvent obj)
    {

        moneyText.text = obj.money.ToString();

    }

    public void RandomGame()
    {
        ViewChanger.instance.ChangeView(5);
    }
    public void RetryLevel()
    {
        // ViewChanger.instance.ChangeView(1);
        CodeControl.Message.Send<RemoveLevelRequestEvent>(new RemoveLevelRequestEvent());

        CodeControl.Message.Send<GenerateLevelRequestEvent>(new GenerateLevelRequestEvent(false, true));
    }
}