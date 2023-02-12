using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingMenu : MonoBehaviour
{
    //[SerializeField] private List<PlaceableObject> placeableObjectList;
    public GameObject buildingButtonPrefab;

    public GameObject scrollViewContent;

    void Start()
    {

        Debug.Log("Start");
        // Add a button for each object in the menu
        foreach (PlaceableObject placeableObject in WorldGrid.Instance.placeableObjectList)
        {
            Debug.Log("Item added");
            GameObject button = Instantiate(buildingButtonPrefab) as GameObject;
            button.transform.SetParent(scrollViewContent.transform, false);
            button.GetComponent<Button>().onClick.AddListener(() => SelectObject(placeableObject));


            Image image = button.GetComponent<Image>();
            //Texture2D texture = GetObjectImage(placeableObject);
            //image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

        }
    }

    // Callback function for when an object button is clicked
    void SelectObject(PlaceableObject placeableObject)
    {
        Debug.Log(placeableObject.GetName());
        // Close Menu
        //Have placeableObject select in build mode...
    }

    //public Texture2D GetObjectImage(PlaceableObject placeableObject)
    //{
    //    Vector3 position = new Vector3(0.0f, 0.0f, 0.0f);

    //    // Position the camera to capture a screenshot of the object
    //    Camera camera = Camera.main;
    //    Vector3 objectPosition = objectInstance.transform.position;
    //    camera.transform.position = objectPosition - camera.transform.forward * 10f; // Move the camera back from the object
    //    camera.transform.LookAt(objectPosition);

    //    Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
    //    // Capture a screenshot of the scene and save it to the texture
    //    texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
    //    texture.Apply();

    //    Destroy(objectInstance);

    //    return texture;
    //}
}
