using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Direction : MonoBehaviour
{
    private static Direction direction;
    private string currentDirection;

    private void Awake()
    {
        if (direction == null)
        {
            direction = this;
        }
        else if (direction != this)
        {
            Destroy(gameObject);
        }

        currentDirection = "Up";//Default - Make this face user later on...
    }


    public void TurnClockwise()
    {
        switch (currentDirection)
        {
            case "Up":
                currentDirection = "Right";
                break;
            case "Right":
                currentDirection = "Down";
                break;
            case "Down":
                currentDirection = "Left";
                break;
            case "Left":
                currentDirection = "Up";
                break;
        }
        Debug.Log(currentDirection);
    }

    public void TurnAntiClockwise()
    {
        switch (currentDirection)
        {
            case "Up":
                currentDirection = "Left";
                break;
            case "Left":
                currentDirection = "Down";
                break;
            case "Down":
                currentDirection = "Right";
                break;
            case "Right":
                currentDirection = "Up";
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