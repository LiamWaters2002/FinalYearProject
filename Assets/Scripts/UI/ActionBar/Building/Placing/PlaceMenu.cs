using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceMenu : MonoBehaviour
{
    public Canvas ActionBar;
    public Canvas BuildMenu;
    public Canvas BuildingTypeMenu;

    public void OnCloseBuildMenuClick()
    {
        BuildMenu.enabled = false;
        ActionBar.enabled = true;
    }
}
