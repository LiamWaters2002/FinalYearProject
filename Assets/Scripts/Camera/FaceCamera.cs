using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform.position);
    }

    private void OnMouseDown()
    {
        Vector3 position = transform.position + new Vector3(15, 5, 0);
        mainCamera.transform.position = position;
        mainCamera.transform.LookAt(transform.position);
    }
}