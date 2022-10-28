using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayGrid : MonoBehaviour
{
    private Grid grid;
    private GridCoords gridCoords;

    public DisplayGrid(Grid grid, GridCoords gridCoords)
    {
        this.grid = grid;
        this.gridCoords = gridCoords;
    }
}
