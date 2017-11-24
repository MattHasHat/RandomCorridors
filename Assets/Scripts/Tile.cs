using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileSet { None, Empty, EndNorth, EndEast, EndSouth, EndWest, NorthEast, NorthSouth, NorthWest, EastSouth, EastWest, SouthWest, NoNorth, NoEast, NoSouth, NoWest, Cross, Room }

public class Tile : MonoBehaviour
{
    public Transform Empty;
    public Transform EndNorth;
    public Transform EndEast;
    public Transform EndSouth;
    public Transform EndWest;
    public Transform NorthEast;
    public Transform NorthSouth;
    public Transform NorthWest;
    public Transform EastSouth;
    public Transform EastWest;
    public Transform SouthWest;
    public Transform NoNorth;
    public Transform NoEast;
    public Transform NoSouth;
    public Transform NoWest;
    public Transform Cross;
    public Transform Room;

    public Transform GetPrefabFromTileSet(TileSet tile)
    {
        switch (tile)
        {
            case TileSet.Empty:
                return Empty;
            case TileSet.EndNorth:
                return EndNorth;
            case TileSet.EndEast:
                return EndEast;
            case TileSet.EndSouth:
                return EndSouth;
            case TileSet.EndWest:
                return EndWest;
            case TileSet.NorthEast:
                return NorthEast;
            case TileSet.NorthSouth:
                return NorthSouth;
            case TileSet.NorthWest:
                return NorthWest;
            case TileSet.EastSouth:
                return EastSouth;
            case TileSet.EastWest:
                return EastWest;
            case TileSet.SouthWest:
                return SouthWest;
            case TileSet.NoNorth:
                return NoNorth;
            case TileSet.NoEast:
                return NoEast;
            case TileSet.NoSouth:
                return NoSouth;
            case TileSet.NoWest:
                return NoWest;
            case TileSet.Cross:
                return Cross;
            case TileSet.Room:
                return Room;
            default:
                return Empty;
        }
    }
}
