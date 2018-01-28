using System.Collections;
using UnityEngine;

public class VictoryView : MonoBehaviour 
{

    public void NextLevel()
    {
        ViewChanger.instance.ChangeView(1);
        CodeControl.Message.Send<RemoveLevelRequestEvent>(new RemoveLevelRequestEvent());

        CodeControl.Message.Send<GenerateLevelRequestEvent>(new GenerateLevelRequestEvent(false,false));

    }

    public void RetryLevel()
    {
        ViewChanger.instance.ChangeView(1);
        CodeControl.Message.Send<RemoveLevelRequestEvent>(new RemoveLevelRequestEvent());

        CodeControl.Message.Send<GenerateLevelRequestEvent>(new GenerateLevelRequestEvent(false,true));
    }

}