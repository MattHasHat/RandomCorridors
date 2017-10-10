﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSpawner : MonoBehaviour
{
    private enum TileSet { None, Empty, EndNorth, EndEast, EndSouth, EndWest, NorthEast, NorthSouth, NorthWest, EastSouth, EastWest, SouthWest, NoNorth, NoEast, NoSouth, NoWest, All }
    public enum Direction { North, East, South, West }
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

    private GridLocation adventurerLocation;
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

    TileSet GetQualifyingBlockType(int placementGridX, int placementGridZ, bool hasWest, bool hasNorth, bool hasEast, bool hasSouth, bool noWest, bool noNorth, bool noEast, bool noSouth, bool useDeadEnds)
    {
        if (useDeadEnds)
        {
            return TileSet.Empty;
        }

        ArrayList currentQualifyingTypes = GetListOfAllTiles();

        currentQualifyingTypes.Remove(TileSet.Empty);
        currentQualifyingTypes.Remove(TileSet.None);

        currentQualifyingTypes.Remove(TileSet.EndNorth);
        currentQualifyingTypes.Remove(TileSet.EndEast);
        currentQualifyingTypes.Remove(TileSet.EndSouth);
        currentQualifyingTypes.Remove(TileSet.EndWest);

        if (hasWest == true)
        {
            currentQualifyingTypes.Remove(TileSet.NorthEast);
            currentQualifyingTypes.Remove(TileSet.NorthSouth);
            currentQualifyingTypes.Remove(TileSet.EastSouth);
            currentQualifyingTypes.Remove(TileSet.NoWest);
        }

        if (hasNorth == true)
        {
            currentQualifyingTypes.Remove(TileSet.EastWest);
            currentQualifyingTypes.Remove(TileSet.SouthWest);
            currentQualifyingTypes.Remove(TileSet.EastSouth);
            currentQualifyingTypes.Remove(TileSet.NoNorth);
        }

        if (hasEast == true)
        {
            currentQualifyingTypes.Remove(TileSet.NorthSouth);
            currentQualifyingTypes.Remove(TileSet.NorthWest);
            currentQualifyingTypes.Remove(TileSet.SouthWest);
            currentQualifyingTypes.Remove(TileSet.NoEast);
        }

        if (hasSouth == true)
        {
            currentQualifyingTypes.Remove(TileSet.NorthEast);
            currentQualifyingTypes.Remove(TileSet.NorthWest);
            currentQualifyingTypes.Remove(TileSet.EastWest);
            currentQualifyingTypes.Remove(TileSet.NoSouth);
        }

        if (placementGridX == 0 || noWest == true)
        {
            currentQualifyingTypes.Remove(TileSet.NorthWest);
            currentQualifyingTypes.Remove(TileSet.EastWest);
            currentQualifyingTypes.Remove(TileSet.SouthWest);
            currentQualifyingTypes.Remove(TileSet.NoNorth);
            currentQualifyingTypes.Remove(TileSet.NoEast);
            currentQualifyingTypes.Remove(TileSet.NoSouth);
            currentQualifyingTypes.Remove(TileSet.All);
        }

        if (placementGridX == NumBuildingBlocksAcross - 1 || noEast == true)
        {
            currentQualifyingTypes.Remove(TileSet.NorthEast);
            currentQualifyingTypes.Remove(TileSet.EastWest);
            currentQualifyingTypes.Remove(TileSet.EastSouth);
            currentQualifyingTypes.Remove(TileSet.NoNorth);
            currentQualifyingTypes.Remove(TileSet.NoSouth);
            currentQualifyingTypes.Remove(TileSet.NoWest);
            currentQualifyingTypes.Remove(TileSet.All);
        }

        if (placementGridZ == 0 || noSouth == true)
        {
            currentQualifyingTypes.Remove(TileSet.NorthSouth);
            currentQualifyingTypes.Remove(TileSet.EastSouth);
            currentQualifyingTypes.Remove(TileSet.SouthWest);
            currentQualifyingTypes.Remove(TileSet.NoNorth);
            currentQualifyingTypes.Remove(TileSet.NoEast);
            currentQualifyingTypes.Remove(TileSet.NoWest);
            currentQualifyingTypes.Remove(TileSet.All);
        }

        if (placementGridZ == NumBuildingBlocksUp - 1 || noNorth == true)
        {
            currentQualifyingTypes.Remove(TileSet.NorthEast);
            currentQualifyingTypes.Remove(TileSet.NorthSouth);
            currentQualifyingTypes.Remove(TileSet.NorthWest);
            currentQualifyingTypes.Remove(TileSet.NoEast);
            currentQualifyingTypes.Remove(TileSet.NoSouth);
            currentQualifyingTypes.Remove(TileSet.NoWest);
            currentQualifyingTypes.Remove(TileSet.All);
        }

        if (currentQualifyingTypes.Count == 0)
        {
            return TileSet.Empty;
        }
        else
        {
            return (TileSet)currentQualifyingTypes[Random.Range(0, currentQualifyingTypes.Count)];
        }
    }

    bool CellLocationHasEast(GridLocation testLocation)
    {
        if (testLocation.x == NumBuildingBlocksAcross - 1)
        {
            return false;
        }

        TileSet testType = TileSetGrid[testLocation.x, testLocation.z];
        switch (testType)
        {
            case TileSet.EndEast:
            case TileSet.NorthEast:
            case TileSet.EastSouth:
            case TileSet.EastWest:
            case TileSet.NoNorth:
            case TileSet.NoSouth:
            case TileSet.NoWest:
            case TileSet.All:
                return true;
            default:
                return false;
        }
    }

    bool CellLocationHasWest(GridLocation testLocation)
    {
        if (testLocation.x == 0)
        {
            return false;
        }

        TileSet testType = TileSetGrid[testLocation.x, testLocation.z];
        switch (testType)
        {
            case TileSet.EndWest:
            case TileSet.NorthWest:
            case TileSet.EastWest:
            case TileSet.SouthWest:
            case TileSet.NoNorth:
            case TileSet.NoEast:
            case TileSet.NoSouth:
            case TileSet.All:
                return true;
            default:
                return false;
        }
    }

    bool CellLocationHasNorth(GridLocation testLocation)
    {
        if (testLocation.z == NumBuildingBlocksUp - 1)
        {
            return false;
        }

        TileSet testType = TileSetGrid[testLocation.x, testLocation.z];
        switch (testType)
        {
            case TileSet.EndNorth:
            case TileSet.NorthEast:
            case TileSet.NorthSouth:
            case TileSet.NorthWest:
            case TileSet.NoEast:
            case TileSet.NoSouth:
            case TileSet.NoWest:
            case TileSet.All:
                return true;
            default:
                return false;
        }
    }

    bool CellLocationHasSouth(GridLocation testLocation)
    {
        if (testLocation.z == 0)
        {
            return false;
        }

        TileSet testType = TileSetGrid[testLocation.x, testLocation.z];
        switch (testType)
        {
            case TileSet.EndSouth:
            case TileSet.NorthSouth:
            case TileSet.EastSouth:
            case TileSet.SouthWest:
            case TileSet.NoNorth:
            case TileSet.NoEast:
            case TileSet.NoWest:
            case TileSet.All:
                return true;
            default:
                return false;
        }
    }

    TileSet ChooseValidBuildingBlockType(GridLocation location)
    {
        bool hasWest = false;

        bool hasNorth = false;

        bool hasEast = false;

        bool hasSouth = false;

        bool noWest = false;

        bool noNorth = false;

        bool noEast = false;

        bool noSouth = false;

        if (location.x > 0 && TileSetGrid[location.x - 1, location.z] != TileSet.Empty)
        {
            if (CellLocationHasEast(new GridLocation(location.x - 1, location.z)))
            {
                hasWest = true;
            }
            else
            {
                noWest = true;
            }
        }

        if (location.x < NumBuildingBlocksAcross - 1 && TileSetGrid[location.x + 1, location.z] != TileSet.Empty)
        {
            if (CellLocationHasWest(new GridLocation(location.x + 1, location.z)))
            {
                hasEast = true;
            }
            else
            {
                noEast = true;
            }
        }

        if (location.z > 0 && TileSetGrid[location.x, location.z - 1] != TileSet.Empty)
        {
            if (CellLocationHasNorth(new GridLocation(location.x, location.z - 1)))
            {
                hasSouth = true;
            }
            else

            {
                noSouth = true;
            }
        }

        if (location.z < NumBuildingBlocksUp - 1 && TileSetGrid[location.x, location.z + 1] != TileSet.Empty)
        {
            if (CellLocationHasSouth(new GridLocation(location.x, location.z + 1)))
            {
                hasNorth = true;
            }
            else
            {
                noNorth = true;
            }
        }

        return GetQualifyingBlockType(location.x, location.z, hasWest, hasNorth, hasEast, hasSouth, noWest, noNorth, noEast, noSouth, false);
    }

    void FillOutGrid()
    {
        for (int i = 0; i < NumBuildingBlocksAcross; ++i)
        {
            for (int j = 0; j < NumBuildingBlocksUp; ++j)
            {
                if (TileSetGrid[i, j] == TileSet.Empty)
                {
                    TileSetGrid[i, j] = ChooseValidBuildingBlockType(new GridLocation(i, j));
                }
            }
        }
    }

    bool CellConnectedToCell(int startLocX, int startLocZ, int goalLocX, int goalLocZ, ref ArrayList knownExistingConnections)
    {
        ArrayList alreadySearchedList = new ArrayList();
        ArrayList toSearchList = new ArrayList();

        bool foundPath = false;
        bool doneWithSearch = false;
        toSearchList.Add(new GridLocation(startLocX, startLocZ));

        while (!doneWithSearch)
        {
            if (toSearchList.Count == 0)
            {
                doneWithSearch = true;
                break;
            }

            GridLocation toSearch = (GridLocation)toSearchList[0];
            toSearchList.RemoveAt(0);
            if (alreadySearchedList.Contains(toSearch) == false)
            {
                alreadySearchedList.Add(toSearch);
            }

            if ((toSearch.x == goalLocX && toSearch.z == goalLocZ) || knownExistingConnections.Contains(toSearch))
            {
                doneWithSearch = true;
                foundPath = true;

                foreach (GridLocation pos in alreadySearchedList)
                {
                    knownExistingConnections.Add(pos);
                }

                foreach (GridLocation pos in toSearchList)
                {
                    knownExistingConnections.Add(pos);
                }

                break;
            }
            else
            {
                if (CellLocationHasEast(new GridLocation(toSearch.x, toSearch.z)))
                {
                    GridLocation newLocation = new GridLocation(toSearch.x + 1, toSearch.z);
                    if (toSearchList.Contains(newLocation) == false && alreadySearchedList.Contains(newLocation) == false)
                    {
                        toSearchList.Add(newLocation);
                    }

                }

                if (CellLocationHasWest(new GridLocation(toSearch.x, toSearch.z)))
                {
                    GridLocation newLocation = new GridLocation(toSearch.x - 1, toSearch.z);
                    if (toSearchList.Contains(newLocation) == false && alreadySearchedList.Contains(newLocation) == false)
                    {
                        toSearchList.Add(newLocation);
                    }
                }

                if (CellLocationHasNorth(new GridLocation(toSearch.x, toSearch.z)))
                {
                    GridLocation newLocation = new GridLocation(toSearch.x, toSearch.z + 1);
                    if (toSearchList.Contains(newLocation) == false && alreadySearchedList.Contains(newLocation) == false)
                    {
                        toSearchList.Add(newLocation);
                    }
                }

                if (CellLocationHasSouth(new GridLocation(toSearch.x, toSearch.z)))
                {
                    GridLocation newLocation = new GridLocation(toSearch.x, toSearch.z - 1);
                    if (toSearchList.Contains(newLocation) == false && alreadySearchedList.Contains(newLocation) == false)
                    {
                        toSearchList.Add(newLocation);
                    }
                }
            }
        }

        return foundPath;
    }

    void FixUpIslands()
    {
        ArrayList knownExistingConnections = new ArrayList();

        for (int i = 0; i < NumBuildingBlocksAcross; ++i)
        {
            for (int j = 0; j < NumBuildingBlocksUp; ++j)
            {
                if (TileSetGrid[i, j] != TileSet.Empty && !CellConnectedToCell(i, j, NumBuildingBlocksAcross / 2, NumBuildingBlocksUp / 2, ref knownExistingConnections))
                {
                    TileSetGrid[i, j] = TileSet.Empty;
                }
            }
        }
    }

    TileSet GetDeadEndCapType(int locX, int locZ)
    {
        if (locZ < NumBuildingBlocksUp - 1 && CellLocationHasSouth(new GridLocation(locX, locZ + 1)))
        {
            return TileSet.EndNorth;
        }
        else if (locX < NumBuildingBlocksAcross - 1 && CellLocationHasWest(new GridLocation(locX + 1, locZ)))
        {
            return TileSet.EndEast;
        }
        else if (locZ > 0 && CellLocationHasNorth(new GridLocation(locX, locZ - 1)))
        {
            return TileSet.EndSouth;
        }
        else if (locX > 0 && CellLocationHasEast(new GridLocation(locX - 1, locZ)))
        {
            return TileSet.EndWest;
        }

        return TileSet.Empty;
    }

    void CapOffDeadEnds()
    {
        for (int i = 0; i < NumBuildingBlocksAcross; ++i)
        {
            for (int j = 0; j < NumBuildingBlocksUp; ++j)
            {
                if (TileSetGrid[i, j] == TileSet.Empty)
                {
                    TileSetGrid[i, j] = GetDeadEndCapType(i, j);
                }
            }
        }
    }

    void FillDungeonBuildingBlockGrid()
    {
        for (int i = 0; i < NumBuildingBlocksAcross; ++i)
        {
            for (int j = 0; j < NumBuildingBlocksUp; ++j)
            {
                TileSetGrid[i, j] = TileSet.Empty;
            }
        }

        FillOutGrid();
        FixUpIslands();
        CapOffDeadEnds();
    }

    public void GoToNextFloor()
    {

    }

    public bool IsOnStairsDown()
    {
        return false;
    }

    void SetAdventurerLocation(GridLocation newLocation)
    {
        adventurerLocation = newLocation;

        prefabBuildingBlockWidth = PrefabBuildingBlock_All.localScale.x;
        prefabBuildingBlockHeight = PrefabBuildingBlock_All.localScale.z;

        float heroPositionX = transform.position.x + (newLocation.x * prefabBuildingBlockWidth);
        float heroPositionZ = transform.position.z + (newLocation.z * prefabBuildingBlockHeight);

        Adventurer.transform.position = new Vector3(heroPositionX, 1.0f, heroPositionZ);
    }

    public bool MoveAdventurer(Direction direction)
    {
        if (direction == Direction.North && CellLocationHasNorth(adventurerLocation))
        {
            SetAdventurerLocation(new GridLocation(adventurerLocation.x, adventurerLocation.z + 1));
            return true;
        }
        else if (direction == Direction.West && CellLocationHasWest(adventurerLocation))
        {
            SetAdventurerLocation(new GridLocation(adventurerLocation.x - 1, adventurerLocation.z));
            return true;
        }
        else if (direction == Direction.South && CellLocationHasSouth(adventurerLocation))
        {
            SetAdventurerLocation(new GridLocation(adventurerLocation.x, adventurerLocation.z - 1));
            return true;
        }
        else if (direction == Direction.East && CellLocationHasEast(adventurerLocation))
        {
            SetAdventurerLocation(new GridLocation(adventurerLocation.x + 1, adventurerLocation.z));
            return true;
        }
        return false;
    }
}