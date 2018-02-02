using System;
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
        CodeControl.Message.AddListener<RandomLevelRequest>(OnRandomLevelRequested);
    }

   

    private void Start()
    {
        foreach(GameObject g in views)
        {
            g.SetActive(false);
        }
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
           // MusicManager.instance.PlaySong(1);
        }else if(view == 0)
        {
            MusicManager.instance.PlaySong(0);

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
  
    void DispatchLevelRequestEvent(int level)
    {
        CodeControl.Message.Send<GenerateLevelRequestEvent>(new GenerateLevelRequestEvent(false,true));
    }
    void DispatchLevelRemoveRequestEvent()
    {
        CodeControl.Message.Send<RemoveLevelRequestEvent>(new RemoveLevelRequestEvent());
    }

    private void OnRandomLevelRequested(RandomLevelRequest obj)
    {
        views[currentView].SetActive(false);
        views[1].SetActive(true);
        currentView = 1;
    }
}