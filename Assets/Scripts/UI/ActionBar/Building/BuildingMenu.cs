using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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


    //Move start code to another script in future, it wont run when the menu is disabled...
    void Start()
    {
        Debug.Log("Start");
        //Scene hiddenScene = SceneManager.CreateScene("HiddenScene");
        // Add a button for each object in the menu

        worldGrid = WorldGrid.Instance;
        generatePreview = GeneratePreview.Instance;

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

            Debug.Log("Got here");
            image.sprite = generatePreview.GetPreview(i);
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

    public Texture2D createImage(PlaceableObject placeableObject, Scene hiddenScene)
    {
        // Load the prefab from the Resources folder

        // Instantiate the prefab in a hidden scene
        GameObject prefabInstance = Instantiate(placeableObject.GetPrefab());
        prefabInstance.transform.position = new Vector3(0, 0, 10); // move the prefab away from the camera
        SceneManager.MoveGameObjectToScene(prefabInstance, SceneManager.GetSceneByName("hiddenScene"));

        // Create and configure the camera
        GameObject cameraObject = new GameObject("Camera");
        Camera camera = cameraObject.AddComponent<Camera>();
        camera.transform.position = new Vector3(placeableObject.GetxWidth() * 30, 50, -50); // position the camera in front of the prefab
        camera.transform.LookAt(prefabInstance.transform);
        camera.orthographic = true;
        camera.orthographicSize = 5;
        camera.nearClipPlane = 0.01f;
        camera.farClipPlane = 1000f;
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = new Color(1f, 1f, 1f, 0f);

        // Create a RenderTexture to hold the camera view
        RenderTexture renderTexture = new RenderTexture(1024, 1024, 24);

        // Render the camera view to the RenderTexture
        camera.targetTexture = renderTexture;
        camera.Render();

        // Save the RenderTexture as a Texture2D
        Texture2D texture = new Texture2D(1024, 1024, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0, 0, 1024, 1024), 0, 0);
        texture.Apply();

        // Destroy the instantiated prefab and unload the hidden scene
        SceneManager.UnloadSceneAsync(hiddenScene);
        DestroyImmediate(prefabInstance);

        return texture;
    }

    }
