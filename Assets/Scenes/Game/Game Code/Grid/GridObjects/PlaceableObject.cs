using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class PlaceableObject: ScriptableObject
{

    [SerializeField] private Transform prefab;
    [SerializeField] private Transform buttonImage; //Do later...
    [SerializeField] private string name;
    [SerializeField] private int width;
    [SerializeField] private int height;
    private Object ingameObject;

    /// <summary>
    /// Creates a placeable object that stores the type of object you want to be displayed.
    /// </summary>
    /// <param name="placeableObjectPrefab"></param>
    public PlaceableObject(Object ingameObject)
    {
        this.ingameObject = ingameObject;
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public string GetName()
    {
        return name;
    }
    public Object GetPrefab()
    {
        return prefab;
    }
}
