using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int zDepth;
    private int xWidth;
    private float cellSize;
    private GridObject gridObject;


    public Grid(int zDepth, int xWidth, float cellSize)
    {
        this.zDepth = zDepth;
        this.xWidth = xWidth;
        this.cellSize = cellSize;
        gridObject = new GridObject(this);
        loop();
    }

    /// <summary>
    /// Create new GridPositions
    /// </summary>
    public void loop()
    {

        for (int x = 0; x < xWidth; x++)
        {
            for (int z = 0; z < zDepth; z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
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
        return xWidth;
    }

    public int getHeight()
    {
        return zDepth;
    }
}
