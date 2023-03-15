using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public GameObject roadPrefab;
    public GameObject tJunctionPrefab;
    public GameObject crossRoadPrefab;
    public GameObject cornerRoadPrefab;
    public WorldGrid worldGrid;


    private void Start()
    {
        worldGrid = WorldGrid.Instance;
    }
    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        // Calculate the grid position based on the mouse click
    //        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //        RaycastHit hit;
    //        if (Physics.Raycast(ray, out hit))
    //        {
    //            int x = Mathf.RoundToInt(hit.point.x);
    //            int z = Mathf.RoundToInt(hit.point.z);

    //            // Check if there is already a road at this grid position
    //            if (grid[x, z].transform.childCount > 0)
    //            {
    //                // Replace the road with a T-junction if there is a road next to it
    //                bool hasRoadToNorth = z < gridSize - 1 && grid[x, z + 1].transform.childCount > 0;
    //                bool hasRoadToSouth = z > 0 && grid[x, z - 1].transform.childCount > 0;
    //                bool hasRoadToEast = x < gridSize - 1 && grid[x + 1, z].transform.childCount > 0;
    //                bool hasRoadToWest = x > 0 && grid[x - 1, z].transform.childCount > 0;

    //                if (hasRoadToNorth || hasRoadToSouth || hasRoadToEast || hasRoadToWest)
    //                {
    //                    Destroy(grid[x, z].transform.GetChild(0).gameObject);
    //                    Instantiate(tJunctionPrefab, grid[x, z].transform.position, Quaternion.identity, grid[x, z].transform);
    //                }
    //                // Otherwise, do nothing
    //            }
    //            else
    //            {
    //                // Check the neighboring grid positions to determine the type of road to place
    //                bool hasRoadToNorth = z < gridSize - 1 && grid[x, z + 1].transform.childCount > 0;
    //                bool hasRoadToSouth = z > 0 && grid[x, z - 1].transform.childCount > 0;
    //                bool hasRoadToEast = x < gridSize - 1 && grid[x + 1, z].transform.childCount > 0;
    //                bool hasRoadToWest = x > 0 && grid[x - 1, z].transform.childCount > 0;

    //                if (hasRoadToNorth && hasRoadToSouth && hasRoadToEast && hasRoadToWest)
    //                {
    //                    Instantiate(crossRoadPrefab, grid[x, z].transform.position, Quaternion.identity, grid[x, z].transform);
    //                }
    //                else if ((hasRoadToNorth && hasRoadToEast && !hasRoadToSouth && !hasRoadToWest) ||
    //                         (hasRoadToSouth && hasRoadToWest && !hasRoadToNorth && !hasRoadToEast) ||
    //                         (hasRoadToEast && hasRoadToSouth && !hasRoadToNorth && !hasRoadToWest) ||
    //                         (hasRoadToWest && hasRoadToNorth && !hasRoadToSouth && !hasRoadToEast))
    //                {
    //                    Instantiate(cornerRoadPrefab, grid[x, z].transform.position, Quaternion.Euler(0, 90, 0), grid[x, z].transform);
    //                }
    //                else if ((hasRoadToNorth && hasRoadToSouth && !hasRoadToEast && !hasRoadToWest) ||
    //                         (hasRoadToEast && hasRoadToWest && !hasRoadToNorth && !hasRoadToSouth))
    //                {
    //                    Instantiate(roadPrefab, grid[x, z].transform.position, Quaternion.identity, grid[x, z].transform);
    //                }
    //                else
    //                {
    //                    // Invalid placement, do nothing
    //                }
    //            }
    //        }
    //    }
    //}
}
