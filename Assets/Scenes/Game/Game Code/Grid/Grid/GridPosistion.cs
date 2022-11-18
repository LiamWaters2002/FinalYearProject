using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPosition
{
    private int x;
    private int z;

    public GridPosition(int x, int z)
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
