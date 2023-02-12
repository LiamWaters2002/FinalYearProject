using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Direction : MonoBehaviour
{
    public static Direction direction { get; private set; }
    private static string currentDirection;

    private Direction()
    {
        currentDirection = "down";
    }

    public static Direction DirectionInstance()
    {
        if (direction == null)
        {
            direction = new Direction();
        }
        return direction;
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

    public Direction getDirection()
    {
        return direction;
    }

    public string getCurrentDirection()
    {
        return currentDirection;
    }
}