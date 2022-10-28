using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int height;
    private int width;
    private float cellSize;
    private DisplayGrid[,] gridArray;


    public Grid(int height, int width, float cellSize)
    {
        this.height = height;
        this.width = width;
        this.cellSize = cellSize;
        gridArray = new DisplayGrid[width, height];
        loop();
    }

    public void loop(Transform visualPrefab)
    {

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                GridCoords gridCoords = new GridCoords(x, z);
                gridArray[x, z] = new DisplayGrid(this, gridCoords);
                GameObject.Instantiate(visualPrefab, GetWorldPosition(x, z), Quaternion.identity);
            }
        }
    }

    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z) * cellSize;
    }

    public GridCoords GetGridPosition(Vector3 worldPosition)
    {
        return new GridCoords(
            Mathf.RoundToInt(worldPosition.x / cellSize),
            Mathf.RoundToInt(worldPosition.z / cellSize)
            );
    }
}
