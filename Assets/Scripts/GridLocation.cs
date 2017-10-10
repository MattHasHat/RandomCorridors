using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLocation
{
    public int x;
    public int z;

    public GridLocation(int xLoc, int zLoc)
    {
        x = xLoc;
        z = zLoc;
    }

    public override bool Equals(object obj)
    {
        if (obj is GridLocation)
        {
            GridLocation t = (GridLocation)obj;
            if (t.x == x && t.z == z)
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
