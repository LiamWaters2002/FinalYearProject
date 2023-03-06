using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacingMenu : MonoBehaviour
{
    public Canvas ActionBar;
    public Canvas PlaceMenu;

    public void OnClosePlaceMenuClick()
    {
        PlaceMenu.enabled = false;
        ActionBar.enabled = true;
    }
}
