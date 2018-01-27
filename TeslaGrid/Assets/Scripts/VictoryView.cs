using System.Collections;
using UnityEngine;

public class VictoryView : MonoBehaviour 
{

    public void NextLevel()
    {
        ViewChanger.instance.ChangeView(1);
        int nextLevel = PlayerPrefs.GetInt("CurrentLevel") + 1;
        Debug.Log(nextLevel);
        CodeControl.Message.Send<RemoveLevelRequestEvent>(new RemoveLevelRequestEvent());

        CodeControl.Message.Send<GenerateLevelRequestEvent>(new GenerateLevelRequestEvent(nextLevel));

    }

    public void RetryLevel()
    {
        ViewChanger.instance.ChangeView(1);
        CodeControl.Message.Send<RemoveLevelRequestEvent>(new RemoveLevelRequestEvent());

        CodeControl.Message.Send<GenerateLevelRequestEvent>(new GenerateLevelRequestEvent(PlayerPrefs.GetInt("CurrentLevel")));
    }

}