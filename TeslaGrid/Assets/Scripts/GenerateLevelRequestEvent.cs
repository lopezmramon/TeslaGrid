using System.Collections;
using UnityEngine;

public class GenerateLevelRequestEvent : CodeControl.Message 
{
    public int levelRequested;

    public GenerateLevelRequestEvent(int levelRequested)
    {
        this.levelRequested = levelRequested;
    }


}