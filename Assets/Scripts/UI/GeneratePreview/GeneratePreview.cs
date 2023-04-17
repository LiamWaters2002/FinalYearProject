using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(1)]
public class GeneratePreview : MonoBehaviour
{
    public static GeneratePreview Instance;
    [SerializeField] public List<PlaceableObject> placeableObjectList;
    [SerializeField] public List<Sprite> previewObjectImages;
    [SerializeField] public RenderTexture renderTexture;

    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            previewObjectImages = new List<Sprite>();
            renderTexture = Resources.Load<RenderTexture>("RenderTexture/test");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
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
        prefabInstance.transform.position = new Vector3(-2000, -2000, -2000); // move the prefab away from the camera
        SceneManager.MoveGameObjectToScene(prefabInstance, SceneManager.GetSceneByName("HiddenScene"));

        //Camera Setup
        GameObject cameraObject = new GameObject("Camera");
        Camera camera = cameraObject.AddComponent<Camera>();


        if (placeableObject.GetxWidth() < 5 && placeableObject.GetzDepth() < 5)
        {
            camera.transform.position = prefabInstance.transform.position + new Vector3(placeableObject.GetxWidth() * 5, 7, placeableObject.GetzDepth() * 5);
        }
        else if (placeableObject.GetxWidth() > 15 && placeableObject.GetzDepth() > 15)
        {
            camera.transform.position = prefabInstance.transform.position + new Vector3(placeableObject.GetxWidth() * 10, 70, placeableObject.GetzDepth() * 10); // position the camera in front of the prefab
            camera.orthographicSize = 20;
            camera.orthographic = true;
        }
        else
        {
            camera.transform.position = prefabInstance.transform.position + new Vector3(placeableObject.GetxWidth() * 10, 70, placeableObject.GetzDepth() * 10); // position the camera in front of the prefab
            camera.orthographicSize = 15;
            camera.orthographic = true;
        }
        
        camera.transform.LookAt(prefabInstance.transform.Find("Plane")); //Focus on object with current setup.
        camera.clearFlags = CameraClearFlags.Color;
        camera.backgroundColor = new Color(0.84f, 0.84f, 0.84f);

        // Create a RenderTexture to hold the camera view
        renderTexture = new RenderTexture(1024, 1024, 24);

        // Make camera render the RenderTexture
        camera.targetTexture = renderTexture;
        camera.Render();

        // Save the RenderTexture as a Texture2D
        Texture2D texture = new Texture2D(1024, 1024, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
        texture.ReadPixels(new Rect(0, 0, 1024, 1024), 0, 0);
        texture.Apply();

        // Destroy the instantiated prefab and unload the hidden scene
        //SceneManager.UnloadSceneAsync(hiddenScene);
        DestroyImmediate(prefabInstance);
        DestroyImmediate(cameraObject);

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