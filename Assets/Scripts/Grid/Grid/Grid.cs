using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int height;
    private int width;
    private float cellSize;
    private GridObject gridObject;


    public Grid(int height, int width, float cellSize)
    {
        this.height = height;
        this.width = width;
        this.cellSize = cellSize;
        gridObject = new GridObject(this);
        loop();
    }

    public void loop()
    {

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                //gridObject.AddObject(this, GridPosition);
                Debug.DrawLine(GetWorldPosition(gridPosition), GetWorldPosition(gridPosition) + Vector3.right * .9f, Color.red, 1000);
            }
        }
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition)
    {
        return new Vector3(gridPosition.getX(), 0, gridPosition.getZ()) * cellSize;
    }

    public GridPosition GetGridPosition(Vector3 worldPosition)
    {
        return new GridPosition(
            Mathf.RoundToInt(worldPosition.x / cellSize),
            Mathf.RoundToInt(worldPosition.z / cellSize));
    }

    public GridObject GetGridObject()
    {
        return gridObject;
    }

    public int getWidth()
    {
        return width;
    }

    public int getHeight()
    {
        return height;
    }
}
