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

    public void AddObject(PlaceableObject placeableObject, GridPosition gridPosition, string direction)
    {
        //gridObjectArray[gridPosition.getX(), gridPosition.getZ()] = placeableObject;
        int width = placeableObject.GetWidth();
        int height = placeableObject.GetHeight();
        Debug.Log("Width = " + width + " Height = " + height);
        if(direction == "down")
        {  
            for(int x = 0; x < width; x++)
            {
                for(int z = 0; z < height; z++)
                {
                    
                    gridObjectArray[gridPosition.getX() + x, gridPosition.getZ() + z] = placeableObject;
                }
            }
        }
    }

    public void RemoveObject(GridPosition gridPosition)
    {
        gridObjectArray[gridPosition.getX(), gridPosition.getZ()] = null;
    }

    public bool isObstructed(GridPosition gridPosition, PlaceableObject placeableObject)
    {
        //if (gridObjectArray[gridPosition.getX(), gridPosition.getZ()] != null)
        //{
        //    return true;
        //}

        int clickedX = gridPosition.getX();
        int clickedZ = gridPosition.getZ();

        int objectWidth = placeableObject.GetWidth();
        int objectHeight = placeableObject.GetHeight();

        //Debug.Log("Height:" + height + "width: " + width);
        for (int x= 0; x < objectWidth; x++)
        {
            for (int z = 0; z < objectHeight; z++)
            {
                if(gridObjectArray[clickedX + x,clickedZ + z] != null)
                {
                    Debug.Log("X:" + x + "Z:" + z);
                    return true;
                }
            }
        }
        return false;
    }

    public PlaceableObject GetObject(GridPosition gridPosition)
    {
        return gridObjectArray[gridPosition.getX(), gridPosition.getZ()];
    }
}
