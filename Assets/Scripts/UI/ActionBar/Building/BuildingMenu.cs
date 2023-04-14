using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BuildingMenu : MonoBehaviour
{
    public GameObject buildingButtonPrefab;
    public GameObject scrollViewContent;
    public ScrollRect scrollView;
    public Canvas ActionBarCanvas;
    public Canvas BuildMenuCanvas;
    public Canvas PlaceMenuCanvas;
    public GeneratePreview generatePreview;
    public WorldGrid worldGrid;

    public bool loopCompleted;

    void Start()
    {
        Debug.Log("Start");
        //Scene hiddenScene = SceneManager.CreateScene("HiddenScene");
        // Add a button for each object in the menu

        worldGrid = WorldGrid.Instance;
        generatePreview = GeneratePreview.Instance;

    }

    void Update()
    {
        if (BuildMenuCanvas.enabled && !loopCompleted)
        {
            loopCompleted = true;
            for (int i = 0; i < generatePreview.placeableObjectList.Count; i++)
            {
                int x = generatePreview.GetPreviewSize();

                PlaceableObject placeableObject = generatePreview.GetPlaceableObject(i);

                Debug.Log("Item added");
                GameObject button = Instantiate(buildingButtonPrefab) as GameObject;
                button.transform.SetParent(scrollViewContent.transform, false);
                button.GetComponent<Button>().onClick.AddListener(() => SelectObject(placeableObject));


                Transform imageComponent = button.gameObject.transform.Find("Image");
                Image image = imageComponent.GetComponent<Image>();
                image.sprite = generatePreview.GetPreview(i);

                Transform name = button.transform.Find("Name");
                Text txtName = name.GetComponent<Text>();
                txtName.text = placeableObject.GetName();

                Transform description = button.transform.Find("Description");
                Text txtDescription = description.GetComponent<Text>();
                txtDescription.text = placeableObject.GetDescription();

                Transform price = button.transform.Find("Price");
                Text txtPrice = price.GetComponent<Text>();
                txtPrice.text = placeableObject.GetPrice().ToString("n0");
            }
        }
    }

    // Callback function for when an object button is clicked
    void SelectObject(PlaceableObject placeableObject)
    {
        Debug.Log(placeableObject.GetName());
        BuildMenuCanvas.enabled = false;
        PlaceMenuCanvas.enabled = true;

        worldGrid.setPlacableObject(placeableObject);

        //Have placeableObject select in build mode...
    }

    public void OnCloseBuildMenuClick()
    {
        BuildMenuCanvas.enabled = false;
        ActionBarCanvas.enabled = true;
    }
}

