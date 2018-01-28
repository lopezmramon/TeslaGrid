using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ViewChanger : MonoBehaviour
{
    public GameObject[] views;
    
    int currentView;
    public static ViewChanger instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Debug.LogWarning("More than one Viewchanger");
    }
    private void Start()
    {
        currentView = 0;
        ChangeView(0);
    }
    public void ChangeView(int view)
    {
        views[currentView].SetActive(false);
        views[view].SetActive(true);
        currentView = view;
        if(view == 1)
        {
            DispatchLevelRequestEvent(0);
        }
        else
        {
            DispatchLevelRemoveRequestEvent();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void RandomLevelGameplay()
    {
        views[currentView].SetActive(false);
        views[1].SetActive(true);
        currentView = 1;
        views[1].GetComponent<GameplayView>().RandomGame();
    }
    void DispatchLevelRequestEvent(int level)
    {
        CodeControl.Message.Send<GenerateLevelRequestEvent>(new GenerateLevelRequestEvent(level));
    }
    void DispatchLevelRemoveRequestEvent()
    {
        CodeControl.Message.Send<RemoveLevelRequestEvent>(new RemoveLevelRequestEvent());
    }
}