using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressedPosition : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask planeLayerMask;

    public static PressedPosition Instance;
    public static Vector3 clickPosition;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            setClickPosition(getRayCastHit());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Position of where the user clicked on the world's plane</returns>
    private Vector3 getRayCastHit()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, planeLayerMask);
        return raycastHit.point;
    }

    /// <summary>
    /// Sets the static variable clickPosition.
    /// </summary>
    /// <param name="position">raycastHit.point, includes coordinates of the last click on the world's plane.</param>
    private void setClickPosition(Vector3 point)
    {
        clickPosition = point;
    }

    /// <summary>
    /// Returns the static variable clickPositon.
    /// </summary>
    /// <returns>Coordinates of the last click.</returns>
    public static Vector3 getClickPosition()
    {
        return clickPosition;
    }
}