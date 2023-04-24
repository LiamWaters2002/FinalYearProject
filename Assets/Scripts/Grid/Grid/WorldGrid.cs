using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WorldGrid : MonoBehaviour
{
    public static WorldGrid Instance { get; private set; }

    public DetermineOutcome determineOutcome;

    public List<PlaceableObject> placeableObjectList;
    private PlaceableObject placeableObject;
    private Direction direction;
    public Canvas placeMenu;
    public Text governmentMoney;
    public GameObject BuildingContainer;
    public GameObject PlaceableContainer;

    private Grid grid;
    private GridObject gridObject;
    int gridWidth = 100;
    int gridHeight = 100;
    float cellSize = 2f;

    public GameObject ingameGraphPrefab;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        grid = new Grid(gridWidth, gridHeight, cellSize);
        gridObject = grid.GetGridObject();
        direction = Direction.Instance;
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            //Prevent clicking grid when a canvas is being dispalyed.
            if (EventSystem.current.IsPointerOverGameObject() || EventSystem.current.IsPointerOverGameObject(0) || Input.touchCount > 0 && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                return;
            }
            GridPosition gridPosition = grid.GetGridPosition(PressedPosition.getClickPosition());


            int pressedPositionX = gridPosition.getX();
            int pressedPositionZ = gridPosition.getZ();

            Vector3 position = new Vector3((pressedPositionX * cellSize), 0.0f, (pressedPositionZ * cellSize));

            if (placeMenu.isActiveAndEnabled && placeMenu.isRootCanvas)
            {
                if (!ObstructionAtGridPosition(gridPosition, placeableObject))
                {
                    string governmentMoneyString = governmentMoney.text.Replace(",", "");
                    int governmentMoneyInteger = int.Parse(governmentMoneyString);
                    int result = governmentMoneyInteger - placeableObject.GetPrice();


                    if (result > 0) //if player can afford building
                    {
                        governmentMoney.text = result.ToString("N0");
                        string objectDirection = direction.getCurrentDirection();

                        //Offset is used since transform of object is in the corner...
                        Vector3 offset = direction.getOffset(placeableObject, cellSize);

                        Debug.Log(offset.ToString());
                        GameObject ingameObject = Instantiate(placeableObject.GetPrefab(), position + offset, Quaternion.Euler(0, placeableObject.GetDirection(objectDirection), 0));
                        AddObjectAtGridPosition(placeableObject, gridPosition, objectDirection);
                        //Add market grid to game
                        if (placeableObject.hasMarket())
                        {
                            GameObject ingameGraph = Instantiate(ingameGraphPrefab, ingameObject.transform.Find("Plane").transform.position, Quaternion.Euler(0, placeableObject.GetDirection(objectDirection), 0));
                            ingameGraph.transform.parent = ingameObject.transform;
                            ingameObject.transform.parent = BuildingContainer.transform;
                        }
                        else
                        {
                            ingameObject.transform.parent = PlaceableContainer.transform;
                        }


                    }
                }
                else
                {
                    Debug.Log("Building in the way");
                }
            }
            else
            {
                if (IsValidGridPosition(gridPosition) && ObstructionAtGridPosition(gridPosition, placeableObject))
                {
                    PlaceableObject gridObject = GetObjectAtGridPosition(gridPosition);

                }

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

    public PlaceableObject GetObjectAtGridPosition(GridPosition gridPosition)
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

    /// <summary>
    /// Is it within the actual grid, starting at (0,0) and (gridHeight, gridWidth)
    /// </summary>
    /// <param name="gridPosition"></param>
    /// <returns></returns>
    public bool IsValidGridPosition(GridPosition gridPosition)
    {
        return
            gridPosition.getX() >= 0 &&
            gridPosition.getZ() >= 0 &&
            gridPosition.getZ() < gridWidth &&
            gridPosition.getZ() < gridHeight
            ;
    }

    internal void setPlacableObject(PlaceableObject placeableObject)
    {
        this.placeableObject = placeableObject;
    }
}