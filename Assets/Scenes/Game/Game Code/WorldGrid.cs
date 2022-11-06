using System.Collections.Generic;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{
    public static WorldGrid instance;

    private Grid grid;

    private void Awake()
    {
        instance = this;

        grid = new Grid(10, 10, 2f);
    }

    public WorldGrid GetInstance()
    {
        return instance;
    }

    public void AddObjectAtGridPosition(GridPosition gridPosition, Object obj)
    {
        GridObject gridObject = grid.GetGridObject(gridPosition);
        gridObject.AddObject(obj);
    }

    public List<Object> GetObjectListAtGridPosition(GridPosition gridPosition)
    {
        GridObject gridObject = grid.GetGridObject(gridPosition);
        return gridObject.GetObjectList();
    }

    public void RemoveObjectAtGridPosition(GridPosition gridPosition, Object obj)
    {
        GridObject gridObject = grid.GetGridObject(gridPosition);
        gridObject.RemoveObject(obj);
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

}
