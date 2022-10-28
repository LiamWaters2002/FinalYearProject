using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    private Grid grid;


    // Start is called before the first frame update
    private void Start()
    {
        grid = new Grid(10, 10, 2f);

        //GridCoords coords = new GridCoords(10, 10);
        //Debug.Log("X:" + coords.getX().ToString() + " Y:" + coords.getZ().ToString());
    }

    private void Update()
    {
        //Debug.Log(PressedPosition.getClickPosition()); //Test to see if user click detects location.

        Debug.Log(grid.GetGridPosition(PressedPosition.getClickPosition()).getX());
        Debug.Log(grid.GetGridPosition(PressedPosition.getClickPosition()).getZ());
    }
}
