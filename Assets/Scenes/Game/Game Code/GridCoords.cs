using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCoords : MonoBehaviour
{
    private int x;
    private int z;

    public GridCoords(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public int getX()
    {
        return x;
    }

    public int getZ()
    {
        return z;
    }
}
