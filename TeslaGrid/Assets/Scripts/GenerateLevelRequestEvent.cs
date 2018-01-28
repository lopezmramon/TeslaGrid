using System.Collections;
using UnityEngine;

public class GenerateLevelRequestEvent : CodeControl.Message 
{
    public bool random;
    public bool retry;

    public GenerateLevelRequestEvent(bool random, bool retry)
    {
        this.random = random;
        this.retry = retry; 
    }


}