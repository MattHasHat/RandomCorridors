using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dungeon : MonoBehaviour {

    private enum TileSet { None, Empty, EndNorth, EndEast, EndSouth, EndWest, NorthEast, NorthSouth, NorthWest, EastSouth, EastWest, SouthWest, NoNorth, NoEast, NoSouth, NoWest, All }
    private enum Direction { North, East, South, West}
    public enum StairPlacement { Random, AtEdge }

    public ScreenManager ScreeManager;
    public Adventurer Adventurer;

    public Transform PrefabBuildingBlock_Empty;
    public Transform PrefabBuildingBlock_EndNorth;
    public Transform PrefabBuildingBlock_EndEast;
    public Transform PrefabBuildingBlock_EndSouth;
    public Transform PrefabBuildingBlock_EndWest;
    public Transform PrefabBuildingBlock_NorthEast;
    public Transform PrefabBuildingBlock_NorthSouth;
    public Transform PrefabBuildingBlock_NorthWest;
    public Transform PrefabBuildingBlock_EastSouth;
    public Transform PrefabBuildingBlock_EastWest;
    public Transform PrefabBuildingBlock_SouthWest;
    public Transform PrefabBuildingBlock_NoNorth;
    public Transform PrefabBuildingBlock_NoEast;
    public Transform PrefabBuildingBlock_NoSouth;
    public Transform PrefabBuildingBlock_NoWest;
    public Transform PrefabBuildingBlock_All;

    public Transform PrefabStairsUp;
    public Transform PrefabStairsDown;

    public Material MaterialOverrideStairsUp;
    public Material MaterialOverrideStairsDown;

    public Transform PrefabBuildingBlockParent;
    public Transform OtherStuffParent;
    public int NumBuildingBlocksAcross;
    public int NumBuildingBlocksUp;

    public StairPlacement stairPlacement = StairPlacement.Random;

    private float prefabBuildingBlockWidth;
    private float prefabBuildingBlockHeight;

    private GridLocation heroLocation;
    private GridLocation stairsUpLocation;
    private GridLocation stairsDownLocation;

    [HideInInspector]
    public int DungeonFloorNumber = 1;

    private TileSet[,] TileSetGrid;

    Transform GetPrefabFromBuildingBlockType(TileSet tile)
    {
        switch (tile)
        {

            case TileSet.Empty:

                return PrefabBuildingBlock_Empty;

            case TileSet.EndNorth:

                return PrefabBuildingBlock_EndNorth;

            case TileSet.EndEast:

                return PrefabBuildingBlock_EndEast;

            case TileSet.EndSouth:

                return PrefabBuildingBlock_EndSouth;

            case TileSet.EndWest:

                return PrefabBuildingBlock_EndWest;

            case TileSet.NorthEast:

                return PrefabBuildingBlock_NorthEast;

            case TileSet.NorthSouth:

                return PrefabBuildingBlock_NorthSouth;

            case TileSet.NorthWest:

                return PrefabBuildingBlock_NorthWest;

            case TileSet.EastSouth:

                return PrefabBuildingBlock_EastSouth;

            case TileSet.EastWest:

                return PrefabBuildingBlock_EastWest;

            case TileSet.SouthWest:

                return PrefabBuildingBlock_SouthWest;

            case TileSet.NoNorth:

                return PrefabBuildingBlock_NoNorth;

            case TileSet.NoEast:

                return PrefabBuildingBlock_NoEast;

            case TileSet.NoSouth:

                return PrefabBuildingBlock_NoSouth;

            case TileSet.NoWest:

                return PrefabBuildingBlock_NoWest;

            case TileSet.All:

                return PrefabBuildingBlock_All;

            default:

                return PrefabBuildingBlock_Empty;

        }
    }

    TileSet GetRandomTile()
    {
        ArrayList allTiles = GetListOfAllTiles();
        return (TileSet)allTiles[Random.Range(0, allTiles.Count)];
    }

    ArrayList GetListOfAllTiles()
    {
        ArrayList returnList = new ArrayList();
        foreach (TileSet value in TileSet.GetValues(typeof(TileSet)))
        {
            returnList.Add(value);
        }
        return returnList;

    }
}
