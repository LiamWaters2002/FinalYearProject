using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBarController : MonoBehaviour
{
    public Canvas ActionBarCanvas;

    public Canvas BuildingCanvas;
    public Canvas LearnCanvas;

    public void Start()
    {
        ActionBarCanvas.enabled = true;
        BuildingCanvas.enabled = false;
    }

    public void OnBuildActionBarClick()
    {
        ActionBarCanvas.enabled = false;
        BuildingCanvas.enabled = true;

    }

    public void OnLearnActionBarClick()
    {
        ActionBarCanvas.enabled = false;
        LearnCanvas.enabled = true;

    }
}
