using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landscape : MonoBehaviour
{
    public GameObject PortraitStartMenu;
    public GameObject LandscapeStartMenu;

    // Update is called once per frame
    void Update()
    {
        setOrientation();
    }

    public void setOrientation()
    {
        if (Screen.orientation == ScreenOrientation.Portrait || Screen.orientation == ScreenOrientation.PortraitUpsideDown)
        {
            Debug.Log("portrait");
            LandscapeStartMenu.SetActive(false);
            PortraitStartMenu.SetActive(true);
        }
        else
        {
            Debug.Log("landscape");
            PortraitStartMenu.SetActive(false);
            LandscapeStartMenu.SetActive(true);
        }
    }
}
