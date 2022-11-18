using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private PlaceableObject[,] gridObjectArray;
    private Grid grid;
    public GridObject(Grid grid)
    {
        gridObjectArray = new PlaceableObject[grid.getWidth(), grid.getHeight()];
    }

    public void AddObject(PlaceableObject placeableObject, GridPosition gridPosition)
    {
        gridObjectArray[gridPosition.getX(), gridPosition.getZ()] = placeableObject;
    }

    public void RemoveObject(GridPosition gridPosition)
    {
        gridObjectArray[gridPosition.getX(), gridPosition.getZ()] = null;
    }

    public bool isObstructed(GridPosition gridPosition)
    {
        if (gridObjectArray[gridPosition.getX(), gridPosition.getZ()] != null)
        {
            return true;
        }
        return false;
    }

    public PlaceableObject GetObject(GridPosition gridPosition)
    {
        return gridObjectArray[gridPosition.getX(), gridPosition.getZ()];
    }
}
