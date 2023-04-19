using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class Direction : MonoBehaviour
{
    public static Direction Instance { get; private set; }
    private static string currentDirection;

    private Direction()
    {
        currentDirection = "down";
    }

    private void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        Instance = this;
    }

        public void TurnClockwise()
    {
        switch (currentDirection)
        {
            case "up":
                currentDirection = "right";
                break;
            case "right":
                currentDirection = "down";
                break;
            case "down":
                currentDirection = "left";
                break;
            case "left":
                currentDirection = "up";
                break;
        }
        Debug.Log(currentDirection);
    }

    public void TurnAntiClockwise()
    {
        switch (currentDirection)
        {
            case "up":
                currentDirection = "left";
                break;
            case "left":
                currentDirection = "down";
                break;
            case "down":
                currentDirection = "right";
                break;
            case "right":
                currentDirection = "up";
                break;
        }
        Debug.Log(currentDirection);
    }

    //public Direction getDirection()
    //{
    //    return direction;
    //}

    public string getCurrentDirection()
    {
        return currentDirection;
    }

    public Vector3 getOffset(PlaceableObject placeableObject, float cellSize)
    {

        Vector3 offset = new Vector3(0, 0, 0);

        if (currentDirection.Equals("down"))
        {
            offset = new Vector3(0, 0, 0);
        }
        else if (currentDirection.Equals("left"))
        {
            offset = new Vector3(0, 0, (placeableObject.GetxWidth() * cellSize) - cellSize);
        }
        else if (currentDirection.Equals("up"))
        {
            offset = new Vector3((placeableObject.GetxWidth() * cellSize) - cellSize, 0, (placeableObject.GetzDepth() * cellSize) - cellSize);
        }
        else if (currentDirection.Equals("right"))
        {
            offset = new Vector3((placeableObject.GetzDepth() * cellSize) - cellSize, 0, 0);
        }

        return offset;
    }
}