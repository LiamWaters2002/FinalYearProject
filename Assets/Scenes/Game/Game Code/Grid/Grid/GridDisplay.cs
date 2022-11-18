using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDisplay : MonoBehaviour
{
    [SerializeField] private Transform gridDisplayPrefab;

    private GridCell[,] gridCellArray;

    private void Start()
    {
        gridCellArray = new GridCell[WorldGrid.Instance.GetWidth(), WorldGrid.Instance.GetHeight()];


        Debug.Log("I was executed");
        for (int x = 0; x < WorldGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < WorldGrid.Instance.GetWidth(); z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                Transform gridCellTransform = Instantiate(gridDisplayPrefab, WorldGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);
                gridCellArray[x, z] = gridCellTransform.GetComponent<GridCell>();
            }

        }
    }

    public void HideGrid()
    {
        for (int x = 0; x < WorldGrid.Instance.GetWidth(); x++)
        {
            for (int z = 0; z < WorldGrid.Instance.GetWidth(); z++)
            {
                gridCellArray[x, z].HideCell();
            }

        }
    }

    public void DisplayGrid(List<GridPosition> gridPositionList)
    {
        foreach (GridPosition position in gridPositionList)
        {
            gridCellArray[position.getX(), position.getZ()].DisplayCell();
        }
    }
}
