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

    private void snapRoads()
    {
        //worldGrid.GetObjectAtGridPosition();
    }
}
