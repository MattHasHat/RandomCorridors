﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSpawner : MonoBehaviour
{
    public ScreenManager ScreenManager;
    public Adventurer Adventurer;
    public Tile Tile;

    public Transform StairsUp;
    public Transform StairsDown;
    public Transform Key;
    public Transform TileParent;
    public Transform ObjectParent;
    public Transform Camera;

    public int DungeonWidth;
    public int DungeonHeight;
    public int DungeonFloorNumber = 1;

    public GridLocation AdventurerLocation;
    public GridLocation StairsUpLocation;
    public GridLocation StairsDownLocation;
    public GridLocation KeyLocation;

    public TileSet[,] TileSetGrid;

    void InitializeDungeonTileFromGrid()
    {
        float TileWidth = Tile.Tile_Cross.localScale.x;
        float TileHeight = Tile.Tile_Cross.localScale.z;

        for (int i = 0; i < DungeonWidth; i++)
        {
            for (int j = 0; j < DungeonHeight; j++)
            {
                if (TileSetGrid[i, j] != TileSet.None)
                {
                    float locationX = transform.position.x + (i * TileWidth);
                    float locationY = transform.position.z + (j * TileHeight);
                    Transform selectedTile = Tile.GetPrefabFromTileSet(TileSetGrid[i, j]);
                    Transform createTile = (Transform)Instantiate(selectedTile, new Vector3(locationX, 0.0f, locationY), Quaternion.identity);
                    createTile.parent = TileParent;
                }
            }
        }
    }

    TileSet GetPossibleTile(int locationX, int locationZ, bool westValid, bool northValid, bool eastValid, bool southValid, bool westNotValid, bool northNotValid, bool eastNotValid, bool southNotValid, bool useDeadEnds)
    {
        if (useDeadEnds)
        {
            return TileSet.Empty;
        }

        ArrayList possibleTiles = Tile.GetListOfAllTiles();

        possibleTiles.Remove(TileSet.Empty);
        possibleTiles.Remove(TileSet.None);
        possibleTiles.Remove(TileSet.EndNorth);
        possibleTiles.Remove(TileSet.EndEast);
        possibleTiles.Remove(TileSet.EndSouth);
        possibleTiles.Remove(TileSet.EndWest);

        if (westValid == true)
        {
            possibleTiles.Remove(TileSet.NorthEast);
            possibleTiles.Remove(TileSet.NorthSouth);
            possibleTiles.Remove(TileSet.EastSouth);
            possibleTiles.Remove(TileSet.NoWest);
        }

        if (northValid == true)
        {
            possibleTiles.Remove(TileSet.EastWest);
            possibleTiles.Remove(TileSet.SouthWest);
            possibleTiles.Remove(TileSet.EastSouth);
            possibleTiles.Remove(TileSet.NoNorth);
        }

        if (eastValid == true)
        {
            possibleTiles.Remove(TileSet.NorthSouth);
            possibleTiles.Remove(TileSet.NorthWest);
            possibleTiles.Remove(TileSet.SouthWest);
            possibleTiles.Remove(TileSet.NoEast);
        }

        if (southValid == true)
        {
            possibleTiles.Remove(TileSet.NorthEast);
            possibleTiles.Remove(TileSet.NorthWest);
            possibleTiles.Remove(TileSet.EastWest);
            possibleTiles.Remove(TileSet.NoSouth);
        }

        if (locationX == 0 || westNotValid == true)
        {
            possibleTiles.Remove(TileSet.NorthWest);
            possibleTiles.Remove(TileSet.EastWest);
            possibleTiles.Remove(TileSet.SouthWest);
            possibleTiles.Remove(TileSet.NoNorth);
            possibleTiles.Remove(TileSet.NoEast);
            possibleTiles.Remove(TileSet.NoSouth);
            possibleTiles.Remove(TileSet.Cross);
        }

        if (locationX == DungeonWidth - 1 || eastNotValid == true)
        {
            possibleTiles.Remove(TileSet.NorthEast);
            possibleTiles.Remove(TileSet.EastWest);
            possibleTiles.Remove(TileSet.EastSouth);
            possibleTiles.Remove(TileSet.NoNorth);
            possibleTiles.Remove(TileSet.NoSouth);
            possibleTiles.Remove(TileSet.NoWest);
            possibleTiles.Remove(TileSet.Cross);
        }

        if (locationZ == 0 || southNotValid == true)
        {
            possibleTiles.Remove(TileSet.NorthSouth);
            possibleTiles.Remove(TileSet.EastSouth);
            possibleTiles.Remove(TileSet.SouthWest);
            possibleTiles.Remove(TileSet.NoNorth);
            possibleTiles.Remove(TileSet.NoEast);
            possibleTiles.Remove(TileSet.NoWest);
            possibleTiles.Remove(TileSet.Cross);
        }

        if (locationZ == DungeonHeight - 1 || northNotValid == true)
        {
            possibleTiles.Remove(TileSet.NorthEast);
            possibleTiles.Remove(TileSet.NorthSouth);
            possibleTiles.Remove(TileSet.NorthWest);
            possibleTiles.Remove(TileSet.NoEast);
            possibleTiles.Remove(TileSet.NoSouth);
            possibleTiles.Remove(TileSet.NoWest);
            possibleTiles.Remove(TileSet.Cross);
        }

        if (possibleTiles.Count == 0)
        {
            return TileSet.Empty;
        }
        else
        {
            return (TileSet)possibleTiles[Random.Range(0, possibleTiles.Count)];
        }
    }

    public bool GridLocationValidEast(GridLocation location)
    {
        if (location.x == DungeonWidth - 1)
        {
            return false;
        }

        TileSet tile = TileSetGrid[location.x, location.z];
        switch (tile)
        {
            case TileSet.EndEast:
            case TileSet.NorthEast:
            case TileSet.EastSouth:
            case TileSet.EastWest:
            case TileSet.NoNorth:
            case TileSet.NoSouth:
            case TileSet.NoWest:
            case TileSet.Cross:
                return true;
            default:
                return false;
        }
    }

    public bool GridLocationValidWest(GridLocation location)
    {
        if (location.x == 0)
        {
            return false;
        }

        TileSet tile = TileSetGrid[location.x, location.z];
        switch (tile)
        {
            case TileSet.EndWest:
            case TileSet.NorthWest:
            case TileSet.EastWest:
            case TileSet.SouthWest:
            case TileSet.NoNorth:
            case TileSet.NoEast:
            case TileSet.NoSouth:
            case TileSet.Cross:
                return true;
            default:
                return false;
        }
    }

    public bool GridLocationValidNorth(GridLocation location)
    {
        if (location.z == DungeonHeight - 1)
        {
            return false;
        }

        TileSet tile = TileSetGrid[location.x, location.z];
        switch (tile)
        {
            case TileSet.EndNorth:
            case TileSet.NorthEast:
            case TileSet.NorthSouth:
            case TileSet.NorthWest:
            case TileSet.NoEast:
            case TileSet.NoSouth:
            case TileSet.NoWest:
            case TileSet.Cross:
                return true;
            default:
                return false;
        }
    }

    public bool GridLocationValidSouth(GridLocation location)
    {
        if (location.z == 0)
        {
            return false;
        }

        TileSet tile = TileSetGrid[location.x, location.z];
        switch (tile)
        {
            case TileSet.EndSouth:
            case TileSet.NorthSouth:
            case TileSet.EastSouth:
            case TileSet.SouthWest:
            case TileSet.NoNorth:
            case TileSet.NoEast:
            case TileSet.NoWest:
            case TileSet.Cross:
                return true;
            default:
                return false;
        }
    }

    TileSet SelectValidTile(GridLocation location)
    {
        bool westValid = false;
        bool northValid = false;
        bool eastValid = false;
        bool southValid = false;
        bool westNotValid = false;
        bool northNotValid = false;
        bool eastNotValid = false;
        bool southNotValid = false;

        if (location.x > 0 && TileSetGrid[location.x - 1, location.z] != TileSet.Empty)
        {
            if (GridLocationValidEast(new GridLocation(location.x - 1, location.z)))
            {
                westValid = true;
            }
            else
            {
                westNotValid = true;
            }
        }

        if (location.x < DungeonWidth - 1 && TileSetGrid[location.x + 1, location.z] != TileSet.Empty)
        {
            if (GridLocationValidWest(new GridLocation(location.x + 1, location.z)))
            {
                eastValid = true;
            }
            else
            {
                eastNotValid = true;
            }
        }

        if (location.z > 0 && TileSetGrid[location.x, location.z - 1] != TileSet.Empty)
        {
            if (GridLocationValidNorth(new GridLocation(location.x, location.z - 1)))
            {
                southValid = true;
            }
            else
            {
                southNotValid = true;
            }
        }

        if (location.z < DungeonHeight - 1 && TileSetGrid[location.x, location.z + 1] != TileSet.Empty)
        {
            if (GridLocationValidSouth(new GridLocation(location.x, location.z + 1)))
            {
                northValid = true;
            }
            else
            {
                northNotValid = true;
            }
        }

        return GetPossibleTile(location.x, location.z, westValid, northValid, eastValid, southValid, westNotValid, northNotValid, eastNotValid, southNotValid, false);
    }

    void FillRemainingGrid()
    {
        for (int i = 0; i < DungeonWidth; i++)
        {
            for (int j = 0; j < DungeonHeight; j++)
            {
                if (TileSetGrid[i, j] == TileSet.Empty)
                {
                    TileSetGrid[i, j] = SelectValidTile(new GridLocation(i, j));
                }
            }
        }
    }

    bool CellConnectedToCell(int startLocationX, int startLocationZ, int endLocationX, int endLocationZ, ref ArrayList knownExistingConnections)
    {
        ArrayList searchedList = new ArrayList();
        ArrayList toSearchList = new ArrayList();

        bool pathFound = false;
        bool searchCompleted = false;

        toSearchList.Add(new GridLocation(startLocationX, startLocationZ));

        while (!searchCompleted)
        {
            if (toSearchList.Count == 0)
            {
                searchCompleted = true;
                break;
            }

            GridLocation toSearch = (GridLocation)toSearchList[0];
            toSearchList.RemoveAt(0);

            if (searchedList.Contains(toSearch) == false)
            {
                searchedList.Add(toSearch);
            }

            if ((toSearch.x == endLocationX && toSearch.z == endLocationZ) || knownExistingConnections.Contains(toSearch))
            {
                searchCompleted = true;
                pathFound = true;

                foreach (GridLocation pos in searchedList)
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
                if (GridLocationValidEast(new GridLocation(toSearch.x, toSearch.z)))
                {
                    GridLocation newLocation = new GridLocation(toSearch.x + 1, toSearch.z);

                    if (toSearchList.Contains(newLocation) == false && searchedList.Contains(newLocation) == false)
                    {
                        toSearchList.Add(newLocation);
                    }

                }

                if (GridLocationValidWest(new GridLocation(toSearch.x, toSearch.z)))
                {
                    GridLocation newLocation = new GridLocation(toSearch.x - 1, toSearch.z);

                    if (toSearchList.Contains(newLocation) == false && searchedList.Contains(newLocation) == false)
                    {
                        toSearchList.Add(newLocation);
                    }
                }

                if (GridLocationValidNorth(new GridLocation(toSearch.x, toSearch.z)))
                {
                    GridLocation newLocation = new GridLocation(toSearch.x, toSearch.z + 1);

                    if (toSearchList.Contains(newLocation) == false && searchedList.Contains(newLocation) == false)
                    {
                        toSearchList.Add(newLocation);
                    }
                }

                if (GridLocationValidSouth(new GridLocation(toSearch.x, toSearch.z)))
                {
                    GridLocation newLocation = new GridLocation(toSearch.x, toSearch.z - 1);
                    if (toSearchList.Contains(newLocation) == false && searchedList.Contains(newLocation) == false)
                    {
                        toSearchList.Add(newLocation);
                    }
                }
            }
        }
        return pathFound;
    }

    void ConnectIslands()
    {
        ArrayList knownExistingConnections = new ArrayList();

        for (int i = 0; i < DungeonWidth; i++)
        {
            for (int j = 0; j < DungeonHeight; j++)
            {
                if (TileSetGrid[i, j] != TileSet.Empty && !CellConnectedToCell(i, j, DungeonWidth / 2, DungeonHeight / 2, ref knownExistingConnections))
                {
                    TileSetGrid[i, j] = TileSet.Empty;
                }
            }
        }
    }

    TileSet GetValidEndTile(int locationX, int locationZ)
    {
        if (locationZ < DungeonHeight - 1 && GridLocationValidSouth(new GridLocation(locationX, locationZ + 1)))
        {
            return TileSet.EndNorth;
        }
        else if (locationX < DungeonWidth - 1 && GridLocationValidWest(new GridLocation(locationX + 1, locationZ)))
        {
            return TileSet.EndEast;
        }
        else if (locationZ > 0 && GridLocationValidNorth(new GridLocation(locationX, locationZ - 1)))
        {
            return TileSet.EndSouth;
        }
        else if (locationX > 0 && GridLocationValidEast(new GridLocation(locationX - 1, locationZ)))
        {
            return TileSet.EndWest;
        }
        return TileSet.Empty;
    }

    void CapOffEnds()
    {
        for (int i = 0; i < DungeonWidth; i++)
        {
            for (int j = 0; j < DungeonHeight; j++)
            {
                if (TileSetGrid[i, j] == TileSet.Empty)
                {
                    TileSetGrid[i, j] = GetValidEndTile(i, j);
                }
            }
        }
    }

    void FillDungeonTileGrid()
    {
        for (int i = 0; i < DungeonWidth; i++)
        {
            for (int j = 0; j < DungeonHeight; j++)
            {
                TileSetGrid[i, j] = TileSet.Empty;
            }
        }
        FillRemainingGrid();
        ConnectIslands();
        CapOffEnds();
    }

    ArrayList GetPotentialObjectLocations()
    {
        ArrayList returnList = new ArrayList();

        for (int i = 0; i < DungeonWidth; i++)
        {
            for (int j = 0; j < DungeonHeight; j++)
            {
                if (TileSetGrid[i, j] != TileSet.Empty)
                {
                    returnList.Add(new GridLocation(i, j));
                }
            }
        }
        return returnList;
    }

    void InitializeObjects(GridLocation chosenUp, GridLocation chosenDown, GridLocation chosenKey)
    {
        float TileWidth = Tile.Tile_Cross.localScale.x;
        float TileHeight = Tile.Tile_Cross.localScale.z;

        int stairsUpGridX = chosenUp.x;
        int stairsUpGridZ = chosenUp.z;
        float stairsUpLocationX = transform.position.x + (stairsUpGridX * TileWidth);
        float stairsUpLocationZ = transform.position.z + (stairsUpGridZ * TileHeight);
        Transform createStairsUp = (Transform)Instantiate(StairsUp, new Vector3(stairsUpLocationX, 1.0f, stairsUpLocationZ), Quaternion.identity);

        int stairsDownGridX = chosenDown.x;
        int stairsDownGridZ = chosenDown.z;
        float stairsDownLocationX = transform.position.x + (stairsDownGridX * TileWidth);
        float stairsDownLocationZ = transform.position.z + (stairsDownGridZ * TileHeight);
        Transform createStairsDown = (Transform)Instantiate(StairsDown, new Vector3(stairsDownLocationX, 1.0f, stairsDownLocationZ), Quaternion.identity);

        int keyGridX = chosenKey.x;
        int keyGridZ = chosenKey.z;
        float keyLocationX = transform.position.x + (keyGridX * TileWidth);
        float keyLocationZ = transform.position.z + (keyGridZ * TileHeight);
        Transform createKey = (Transform)Instantiate(Key, new Vector3(keyLocationX, 1.05f, keyLocationZ), Quaternion.Euler(0, 135, -90));

        StairsUpLocation = new GridLocation(stairsUpGridX, stairsUpGridZ);
        StairsDownLocation = new GridLocation(stairsDownGridX, stairsDownGridZ);
        KeyLocation = new GridLocation(keyGridX, keyGridZ);

        createStairsUp.parent = ObjectParent;
        createStairsDown.parent = ObjectParent;
        createKey.parent = ObjectParent;
    }

    void PlaceObjects()
    {
        ArrayList potentialObjectPositions = GetPotentialObjectLocations();

        int chosenUpIndex = Random.Range(0, potentialObjectPositions.Count);
        GridLocation chosenUp = (GridLocation)potentialObjectPositions[chosenUpIndex];
        potentialObjectPositions.RemoveAt(chosenUpIndex);

        int chosenDownIndex = Random.Range(0, potentialObjectPositions.Count);
        GridLocation chosenDown = (GridLocation)potentialObjectPositions[chosenDownIndex];
        potentialObjectPositions.RemoveAt(chosenDownIndex);

        int chosenKeyIndex = Random.Range(0, potentialObjectPositions.Count);
        GridLocation chosenKey = (GridLocation)potentialObjectPositions[chosenKeyIndex];

        InitializeObjects(chosenUp, chosenDown, chosenKey);
    }

    void CreateDungeonFloor()
    {
        FillDungeonTileGrid();
        PlaceObjects();
        InitializeDungeonTileFromGrid();
        SetAdventurerLocation(new GridLocation(StairsUpLocation.x, StairsUpLocation.z), Quaternion.identity);
    }

    void Start()
    {
        TileSetGrid = new TileSet[DungeonWidth, DungeonHeight];
        CreateDungeonFloor();
    }

    void DeleteDungeonFloor()
    {
        for (int i = 0; i < TileParent.childCount; i++)
        {
            Destroy(TileParent.GetChild(i).gameObject);
        }

        for (int i = 0; i < ObjectParent.childCount; i++)
        {
            Destroy(ObjectParent.GetChild(i).gameObject);
        }
    }

    public void GoToNextFloor()
    {
        DeleteDungeonFloor();
        CreateDungeonFloor();
        DungeonFloorNumber++;
    }

    public void SetAdventurerLocation(GridLocation location, Quaternion direction)
    {
        AdventurerLocation = location;

        float TileWidth = Tile.Tile_Cross.localScale.x;
        float TileHeight = Tile.Tile_Cross.localScale.z;

        float positionX = transform.position.x + (location.x * TileWidth);
        float positionZ = transform.position.z + (location.z * TileHeight);

        Adventurer.transform.position = new Vector3(positionX, 1.0f, positionZ);
        Adventurer.transform.rotation = direction;
        Camera.transform.position = new Vector3(positionX, 11.0f, positionZ);
    }
}
