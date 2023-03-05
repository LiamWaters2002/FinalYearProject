using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GeneratePreview : MonoBehaviour
{
    public static GeneratePreview Instance;
    [SerializeField] public List<PlaceableObject> placeableObjectList;
    [SerializeField] public List<Sprite> previewObjectImages;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            previewObjectImages = new List<Sprite>();
        }
        else
        {
            Destroy(gameObject);
        }

        Scene hiddenScene = SceneManager.CreateScene("HiddenScene");
        foreach (PlaceableObject placeableObject in placeableObjectList)
        {
            Debug.Log("Part 1");
            Texture2D texture = createImage(placeableObject, hiddenScene);
            texture.Apply();
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            previewObjectImages.Add(sprite);
        }
    }

    public Texture2D createImage(PlaceableObject placeableObject, Scene hiddenScene)
    {
        //Instantiate and move prefab into hidden scene
        GameObject prefabInstance = Instantiate(placeableObject.GetPrefab());
        prefabInstance.transform.position = new Vector3(0, 0, 10); // move the prefab away from the camera
        SceneManager.MoveGameObjectToScene(prefabInstance, SceneManager.GetSceneByName("hiddenScene"));

        //Camera Setup
        GameObject cameraObject = new GameObject("Camera");
        Camera camera = cameraObject.AddComponent<Camera>();
        camera.transform.position = new Vector3(placeableObject.GetxWidth() * 30, 50, -50); // position the camera in front of the prefab
        camera.transform.LookAt(prefabInstance.transform); //Focus on object with current setup.
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

    public Sprite GetPreview(int i)
    {
        return previewObjectImages[i];
    }
    public int GetPreviewSize()
    {
        Debug.Log(previewObjectImages.Count + " is the size");
        return previewObjectImages.Count;
    }

    public PlaceableObject GetPlaceableObject(int i)
    {
        Debug.Log("i is equal to " + i);
        return placeableObjectList[i];
    }
}