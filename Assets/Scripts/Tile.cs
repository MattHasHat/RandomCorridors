using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileSet { None, Empty, EndNorth, EndEast, EndSouth, EndWest, NorthEast, NorthSouth, NorthWest, EastSouth, EastWest, SouthWest, NoNorth, NoEast, NoSouth, NoWest, Cross }

public class Tile : MonoBehaviour
{
    public Transform Tile_Empty;
    public Transform Tile_EndNorth;
    public Transform Tile_EndEast;
    public Transform Tile_EndSouth;
    public Transform Tile_EndWest;
    public Transform Tile_NorthEast;
    public Transform Tile_NorthSouth;
    public Transform Tile_NorthWest;
    public Transform Tile_EastSouth;
    public Transform Tile_EastWest;
    public Transform Tile_SouthWest;
    public Transform Tile_NoNorth;
    public Transform Tile_NoEast;
    public Transform Tile_NoSouth;
    public Transform Tile_NoWest;
    public Transform Tile_Cross;

    public Transform GetPrefabFromTileSet(TileSet tile)
    {
        switch (tile)
        {
            case TileSet.Empty:
                return Tile_Empty;
            case TileSet.EndNorth:
                return Tile_EndNorth;
            case TileSet.EndEast:
                return Tile_EndEast;
            case TileSet.EndSouth:
                return Tile_EndSouth;
            case TileSet.EndWest:
                return Tile_EndWest;
            case TileSet.NorthEast:
                return Tile_NorthEast;
            case TileSet.NorthSouth:
                return Tile_NorthSouth;
            case TileSet.NorthWest:
                return Tile_NorthWest;
            case TileSet.EastSouth:
                return Tile_EastSouth;
            case TileSet.EastWest:
                return Tile_EastWest;
            case TileSet.SouthWest:
                return Tile_SouthWest;
            case TileSet.NoNorth:
                return Tile_NoNorth;
            case TileSet.NoEast:
                return Tile_NoEast;
            case TileSet.NoSouth:
                return Tile_NoSouth;
            case TileSet.NoWest:
                return Tile_NoWest;
            case TileSet.Cross:
                return Tile_Cross;
            default:
                return Tile_Empty;
        }
    }

    public ArrayList GetListOfAllTiles()
    {
        ArrayList returnList = new ArrayList();
        foreach (TileSet value in TileSet.GetValues(typeof(TileSet)))
        {
            returnList.Add(value);
        }
        return returnList;
    }
}
