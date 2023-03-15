using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LearnMenu : MonoBehaviour
{
    public GameObject buildingButtonPrefab;
    public GameObject scrollViewContent;
    public ScrollRect scrollView;
    public Canvas ActionBarCanvas;
    public Canvas BuildMenuCanvas;
    public Canvas PlaceMenuCanvas;
    public GeneratePreview generatePreview;
    public WorldGrid worldGrid;


    //Move start code to another script in future, it wont run when the menu is disabled...
    void Start()
    {
        
    }

    public void OnCloseBuildMenuClick()
    {
        BuildMenuCanvas.enabled = false;
        ActionBarCanvas.enabled = true;
    }
}
