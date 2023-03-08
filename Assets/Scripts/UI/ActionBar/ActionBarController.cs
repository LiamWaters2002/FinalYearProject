using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBarController : MonoBehaviour
{
    public Canvas ActionBarCanvas;

    public Canvas BuildCanvas;
    public Canvas PlaceCanvas;
    public Canvas LearnCanvas;

    public void Start()
    {
        ActionBarCanvas.enabled = true;
        BuildCanvas.enabled = false;
        PlaceCanvas.enabled = false;
        LearnCanvas.enabled = false;
    }

    public void OnBuildActionBarClick()
    {
        //ActionBarCanvas.enabled = false;
        BuildCanvas.enabled = true;

    }

    public void OnLearnActionBarClick()
    {
        //ActionBarCanvas.enabled = false;
        LearnCanvas.enabled = true;

    }
}
