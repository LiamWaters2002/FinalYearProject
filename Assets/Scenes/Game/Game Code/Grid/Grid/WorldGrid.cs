using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldGrid : MonoBehaviour
{
    public static WorldGrid Instance { get; private set; }

    public List<PlaceableObject> placeableObjectList;
    private PlaceableObject placeableObject;
    private Direction direction;

    private Grid grid;
    private GridObject gridObject;
    int gridWidth = 20;
    int gridHeight = 20;
    float cellSize = 2f;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one Grid being used! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;

        grid = new Grid(gridWidth, gridHeight, cellSize);
        gridObject = grid.GetGridObject();

        direction = Direction.DirectionInstance();

        placeableObject = placeableObjectList[0]; 
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            placeableObject = placeableObjectList[0];
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            placeableObject = placeableObjectList[1];
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (EventSystem.current.IsPointerOverGameObject() || EventSystem.current.IsPointerOverGameObject(0) || Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                return;
            }

            GridPosition gridPosition = grid.GetGridPosition(PressedPosition.getClickPosition());

            int pressedPositionX = gridPosition.getX();
            int pressedPositionZ = gridPosition.getZ();



            Debug.Log(pressedPositionX);
            Debug.Log(pressedPositionZ);

            Vector3 position = new Vector3((pressedPositionX * cellSize), 0.0f, (pressedPositionZ * cellSize));

            if (!ObstructionAtGridPosition(gridPosition, placeableObject))
            {
                string objectDirection = direction.getCurrentDirection();
                Object ingameObject = Instantiate(placeableObject.GetPrefab(), position, Quaternion.Euler(0, placeableObject.GetDirection(objectDirection), 0));
                //PlaceableObject placedObject = new PlaceableObject(ingameObject);
                AddObjectAtGridPosition(placeableObject, gridPosition, objectDirection);

            }
            else
            {
                Debug.Log("Building in the way");
            }


        }
    }

    public WorldGrid GetInstance()
    {
        return Instance;
    }

    public void AddObjectAtGridPosition(PlaceableObject placeableObject, GridPosition gridPosition, string direction)
    { 
        gridObject.AddObject(placeableObject, gridPosition, direction);
    }

    public PlaceableObject GetObjectListAtGridPosition(GridPosition gridPosition)
    {
        return gridObject.GetObject(gridPosition);
    }

    public bool ObstructionAtGridPosition(GridPosition gridPosition, PlaceableObject placeableObject)
    {
        return gridObject.isObstructed(gridPosition, placeableObject);
    }

    public void RemoveObjectAtGridPosition(GridPosition gridPosition)
    {
        gridObject.RemoveObject(gridPosition);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return grid.GetGridPosition(worldPosition);
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return grid.GetWorldPosition(gridPosition);
    }

    public int GetWidth()
    {
        return grid.getWidth();
    }

    public int GetHeight()
    {
        return grid.getHeight();
    }

    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        return 
            gridPosition.getX() >= 0 && 
            gridPosition.getZ() >= 0 &&
            gridPosition.getZ() < gridWidth &&
            gridPosition.getZ() < gridHeight
            ;
    }

}
