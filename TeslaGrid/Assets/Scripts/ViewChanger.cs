using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ViewChanger : MonoBehaviour
{
    public GameObject[] views;
    int currentView;
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
    }

    public void Quit()
    {
        Application.Quit();
    }
}