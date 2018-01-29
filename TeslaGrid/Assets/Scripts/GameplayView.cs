using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameplayView : MonoBehaviour
{
    public Text moneyText;
    public Text notEnoughMoneyText;
    public Button retryButton, regenerateButton;
    bool randomLevel;
    RandomLevelRequest r;
    private void Awake()
    {
        CodeControl.Message.AddListener<MoneyChangeEvent>(OnMoneyChanged);
        CodeControl.Message.AddListener<NotEnoughMoneyEvent>(OnNotEnoughMoney);
        CodeControl.Message.AddListener<RandomLevelRequest>(OnRandomLevelRequested);
        CodeControl.Message.AddListener<GenerateLevelRequestEvent>(OnGenerateLevelRequest);
    }

    private void OnGenerateLevelRequest(GenerateLevelRequestEvent obj)
    {
        retryButton.gameObject.SetActive(true);
        regenerateButton.gameObject.SetActive(false);
        randomLevel = false;
    }

    private void OnRandomLevelRequested(RandomLevelRequest obj)
    {
        r = obj;
        retryButton.gameObject.SetActive(false);
        regenerateButton.gameObject.SetActive(true);
        randomLevel = true;

    }

    private void Start()
    {
        moneyText.text = ResourceManager.instance.money.ToString();
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
    public void RegenerateLevel()
    {
        CodeControl.Message.Send<RandomLevelRequest>(r);
    }
}