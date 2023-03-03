using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PlaceableObject : ScriptableObject //ScriptableObject - Defining types for placeable objects
{

    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform buttonImage; //Do later...
    [SerializeField] private string name;
    [SerializeField] private int xWidth;
    [SerializeField] private int zDepth;
    private Object ingameObject;
    public List<GridPosition> worldPositionList;

    /// <summary>
    /// Creates a placeable object that stores the type of object you want to be displayed.
    /// </summary>
    /// <param name="placeableObjectPrefab"></param>
    public PlaceableObject(Object ingameObject)
    {
        this.ingameObject = ingameObject;
        worldPositionList = new List<GridPosition>();
    }

    public int GetDirection(string direction)
    {
        Debug.Log("Dirrection:" + direction);
        if (direction.Equals("down"))
        {
            return 0;
        }
        else if (direction.Equals("left"))
        {
            return 90;
        }
        else if (direction.Equals("up"))
        {
            return 180;
        }
        else if (direction.Equals("right"))
        {
            return 270;
        }
        return 0;
    }

    public int GetxWidth()
    {
        return xWidth;
    }

    public int GetzDepth()
    {
        return zDepth;
    }

    public string GetName()
    {
        return name;
    }
    public GameObject GetPrefab()
    {
        return prefab;
    }
}
