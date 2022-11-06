using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDisplay : MonoBehaviour
{
    [SerializeField] private Transform gridDisplayPrefab;

    WorldGrid worldGrid;

    private void Start()
    {
        for (int x = 0; x < worldGrid.GetInstance().GetWidth(); x++)
        {
            for (int z = 0; z < worldGrid.GetInstance().GetWidth(); z++)
            {
                GridPosition gridPosition = new GridPosition(x, z);
                Instantiate(gridDisplayPrefab, worldGrid.GetInstance().GetWorldPosition(gridPosition), Quaternion.identity);
            }

        }
    }
}
