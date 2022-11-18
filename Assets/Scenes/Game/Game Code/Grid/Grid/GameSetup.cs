using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    public static Grid grid;

    [SerializeField] private Transform transformBuilding;
    int gridWidth = 8;
    int gridHeight = 8;
    float cellSize = 2f;
    private Transform transform;

        // Start is called before the first frame update
    private void Start()
    {
        grid = new Grid(gridWidth, gridHeight, cellSize);
        //Debug.Log("X:" + coords.getX().ToString() + " Y:" + coords.getZ().ToString());
    }

    private void Update()
    {
        //Debug.Log(PressedPosition.getClickPosition()); //Test to see if user click detects location.
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GridPosition gridPosition = grid.GetGridPosition(PressedPosition.getClickPosition());

            int pressedPositionX = gridPosition.getX();
            int pressedPositionZ = gridPosition.getZ();
   
            Debug.Log(pressedPositionX);
            Debug.Log(pressedPositionZ);

            //grid.GetGridObject(gridPosition);

            Vector3 position = new Vector3( (pressedPositionX * cellSize), 0.0f, (pressedPositionZ * cellSize) );

            Instantiate(transformBuilding, position, Quaternion.identity);
        }
        
    }

    public void SetTransform(Transform transform)
    {
        this.transform = transform;
    }

    public bool CanBuild()
    {
        return transform == null;
    }

    public void RemoveTransform()
    {
        transform = null;
    }

    public void GetTransform()
    {

    }

}
