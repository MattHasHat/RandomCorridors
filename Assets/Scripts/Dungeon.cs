using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour {

    private enum TileSet
    {
        None,
        Empty,
        North, East, South, West,
        NorthEast, NorthSouth, NorthWest,
        EastSouth, EastWest, SouthWest,
        NoNorth, NoEast, NoSouth, NoWest,
        AllDirections,
    }

}
