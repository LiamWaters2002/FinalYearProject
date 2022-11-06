using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : Object
{
    private Grid grid;
    private GridPosition gridPosition;
    private List<Object> objectList;

    public GridObject(Grid grid, GridPosition gridPosition)
    {
        this.grid = grid;
        this.gridPosition = gridPosition;
        objectList = new List<Object>();
    }

    public void AddObject(Object obj)
    {
        objectList.Add(obj);
    }

    public void RemoveObject(Object obj)
    {
        objectList.Remove(obj);
    }

    public List<Object> GetObjectList()
    {
        return objectList;
    }
}
