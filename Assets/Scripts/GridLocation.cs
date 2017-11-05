using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLocation
{
    private int X;
    private int Z;

    public GridLocation(int x, int z)
    {
        X = x;
        Z = z;
    }

    public int GetX()
    {
        return X;
    }

    public int GetZ()
    {
        return Z;
    }

    public void SetX(int x)
    {
        X = x;
    }

    public void SetZ(int z)
    {
        Z = z;
    }

    public override bool Equals(object obj)
    {
        if (obj is GridLocation)
        {
            GridLocation test = (GridLocation)obj;
            if (test.X == X && test.Z == Z)
            {
                return true;
            }
        }
        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
