using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridLocation {

    public int x;
    public int y;

    public GridLocation(int xLoc, int yLoc)
    {
        x = xLoc;
        y = yLoc;
    }

    public override bool Equals(object obj)
    {
        if (obj is GridLocation)
        {
            GridLocation t = (GridLocation)obj;
            if (t.x == x && t.y == y)
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
