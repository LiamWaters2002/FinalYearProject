using System;
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
        int xWidth = placeableObject.GetxWidth();
        int zDepth = placeableObject.GetzDepth();
        Debug.Log("Width = " + xWidth + " Height = " + zDepth);
        
        //Direction of blocking building positions when object is placed.
        if(direction == "down" || direction == "up")
        {  
            for(int x = 0; x < xWidth; x++)
            {
                for(int z = 0; z < zDepth; z++)
                {
                    
                    gridObjectArray[gridPosition.getX() + x, gridPosition.getZ() + z] = placeableObject;
                }
            }
        }
        else if (direction == "left" || direction == "right")
        {
            for (int x = 0; x < zDepth; x++) //switch xWidth to zDepth
            {
                for (int z = 0; z < xWidth; z++)
                {

                    gridObjectArray[gridPosition.getX() + x, gridPosition.getZ() + z] = placeableObject;
                    //placeableObject.addLocationPosition(gridPosition.getX() + x, gridPosition.getZ() + z);
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

        int objectWidth = placeableObject.GetxWidth();
        int objectHeight = placeableObject.GetzDepth();

        //Debug.Log("Height:" + zDepth + "xWidth: " + xWidth);
        for (int x= 0; x < objectWidth; x++)
        {
            for (int z = 0; z < objectHeight; z++)
            {
                try
                {
                    if(gridObjectArray[clickedX + x,clickedZ + z] != null)
                    {
                        return true;
                    }
                }
                catch (IndexOutOfRangeException exception)
                {
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
