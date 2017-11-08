using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private int FloorNumber;
    private int DungeonSize;
    private float TileSize;
    private TileSet[,] TileGrid;

    public Transform StairsUp;
    public Transform StairsDown;
    public Transform Key;
    public Transform OilCan;
    public Transform TileParent;
    public Transform StairsParent;
    public Transform KeyParent;
    public Transform OilCanParent;
    public Transform Camera;

    public GridLocation AdventurerLocation;
    public GridLocation StairsUpLocation;
    public GridLocation StairsDownLocation;
    public GridLocation KeyLocation;
    public GridLocation OilCanLocation;

    public ScreenChanger ScreenChanger;
    public Adventurer Adventurer;
    public Tile Tile;

    public int GetFloorNumber()
    {
        return FloorNumber;
    }

    public void SetFloorNumber(int floorNumber)
    {
        FloorNumber = floorNumber;
    }

    public int GetDungeonSize()
    {
        return DungeonSize;
    }

    public void SetDungeonSize(int dungeonSize)
    {
        DungeonSize = dungeonSize;
    }

    public float GetTileSize()
    {
        return TileSize;
    }

    public void SetTileSize(float tileSize)
    {
        TileSize = tileSize;
    }

    public bool GetIfNorthValid(GridLocation location)
    {
        if (location.GetZ() == GetDungeonSize() - 1)
        {
            return false;
        }

        TileSet tile = TileGrid[location.GetX(), location.GetZ()];

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

    public bool GetIfEastValid(GridLocation location)
    {
        if (location.GetX() == GetDungeonSize() - 1)
        {
            return false;
        }

        TileSet tile = TileGrid[location.GetX(), location.GetZ()];

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

    public bool GetIfSouthValid(GridLocation location)
    {
        if (location.GetZ() == 0)
        {
            return false;
        }

        TileSet tile = TileGrid[location.GetX(), location.GetZ()];

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

    public bool GetIfWestValid(GridLocation location)
    {
        if (location.GetX() == 0)
        {
            return false;
        }

        TileSet tile = TileGrid[location.GetX(), location.GetZ()];

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

    ArrayList GetListOfTiles()
    {
        ArrayList list = new ArrayList();

        foreach (TileSet tile in TileSet.GetValues(typeof(TileSet)))
        {
            list.Add(tile);
        }
        return list;
    }

    ArrayList CheckIfAtGridEdge(GridLocation location, ArrayList possibleTiles)
    {
        if (location.GetZ() == GetDungeonSize() - 1)
        {
            possibleTiles.Remove(TileSet.NorthEast);
            possibleTiles.Remove(TileSet.NorthSouth);
            possibleTiles.Remove(TileSet.NorthWest);
            possibleTiles.Remove(TileSet.NoEast);
            possibleTiles.Remove(TileSet.NoSouth);
            possibleTiles.Remove(TileSet.NoWest);
            possibleTiles.Remove(TileSet.Cross);
        }

        if (location.GetX() == GetDungeonSize() - 1)
        {
            possibleTiles.Remove(TileSet.NorthEast);
            possibleTiles.Remove(TileSet.EastSouth);
            possibleTiles.Remove(TileSet.EastWest);
            possibleTiles.Remove(TileSet.NoNorth);
            possibleTiles.Remove(TileSet.NoSouth);
            possibleTiles.Remove(TileSet.NoWest);
            possibleTiles.Remove(TileSet.Cross);
        }

        if (location.GetZ() == 0)
        {
            possibleTiles.Remove(TileSet.NorthSouth);
            possibleTiles.Remove(TileSet.EastSouth);
            possibleTiles.Remove(TileSet.SouthWest);
            possibleTiles.Remove(TileSet.NoNorth);
            possibleTiles.Remove(TileSet.NoEast);
            possibleTiles.Remove(TileSet.NoWest);
            possibleTiles.Remove(TileSet.Cross);
        }

        if (location.GetX() == 0)
        {
            possibleTiles.Remove(TileSet.NorthWest);
            possibleTiles.Remove(TileSet.EastWest);
            possibleTiles.Remove(TileSet.SouthWest);
            possibleTiles.Remove(TileSet.NoNorth);
            possibleTiles.Remove(TileSet.NoEast);
            possibleTiles.Remove(TileSet.NoSouth);
            possibleTiles.Remove(TileSet.Cross);
        }

        return possibleTiles;
    }

    ArrayList CheckIfNorthPossible(GridLocation location, ArrayList possibleTiles)
    {
        if (location.GetZ() < GetDungeonSize() - 1 && TileGrid[location.GetX(), location.GetZ() + 1] != TileSet.Empty)
        {
            if (GetIfSouthValid(new GridLocation(location.GetX(), location.GetZ() + 1)))
            {
                possibleTiles.Remove(TileSet.EastSouth);
                possibleTiles.Remove(TileSet.EastWest);
                possibleTiles.Remove(TileSet.SouthWest);
                possibleTiles.Remove(TileSet.NoNorth);
            }
            else
            {
                possibleTiles.Remove(TileSet.NorthEast);
                possibleTiles.Remove(TileSet.NorthSouth);
                possibleTiles.Remove(TileSet.NorthWest);
                possibleTiles.Remove(TileSet.NoEast);
                possibleTiles.Remove(TileSet.NoSouth);
                possibleTiles.Remove(TileSet.NoWest);
                possibleTiles.Remove(TileSet.Cross);
            }
        }
        return possibleTiles;
    }

    ArrayList CheckIfEastPossible(GridLocation location, ArrayList possibleTiles)
    {
        if (location.GetX() < GetDungeonSize() - 1 && TileGrid[location.GetX() + 1, location.GetZ()] != TileSet.Empty)
        {
            if (GetIfWestValid(new GridLocation(location.GetX() + 1, location.GetZ())))
            {
                possibleTiles.Remove(TileSet.NorthSouth);
                possibleTiles.Remove(TileSet.NorthWest);
                possibleTiles.Remove(TileSet.SouthWest);
                possibleTiles.Remove(TileSet.NoEast);
            }
            else
            {
                possibleTiles.Remove(TileSet.NorthEast);
                possibleTiles.Remove(TileSet.EastSouth);
                possibleTiles.Remove(TileSet.EastWest);
                possibleTiles.Remove(TileSet.NoNorth);
                possibleTiles.Remove(TileSet.NoSouth);
                possibleTiles.Remove(TileSet.NoWest);
                possibleTiles.Remove(TileSet.Cross);
            }
        }
        return possibleTiles;
    }

    ArrayList CheckIfSouthPossible(GridLocation location, ArrayList possibleTiles)
    {
        if (location.GetZ() > 0 && TileGrid[location.GetX(), location.GetZ() - 1] != TileSet.Empty)
        {
            if (GetIfNorthValid(new GridLocation(location.GetX(), location.GetZ() - 1)))
            {
                possibleTiles.Remove(TileSet.NorthEast);
                possibleTiles.Remove(TileSet.NorthWest);
                possibleTiles.Remove(TileSet.EastWest);
                possibleTiles.Remove(TileSet.NoSouth);
            }
            else
            {
                possibleTiles.Remove(TileSet.NorthSouth);
                possibleTiles.Remove(TileSet.EastSouth);
                possibleTiles.Remove(TileSet.SouthWest);
                possibleTiles.Remove(TileSet.NoNorth);
                possibleTiles.Remove(TileSet.NoEast);
                possibleTiles.Remove(TileSet.NoWest);
                possibleTiles.Remove(TileSet.Cross);
            }
        }
        return possibleTiles;
    }

    ArrayList CheckIfWestPossible(GridLocation location, ArrayList possibleTiles)
    {
        if (location.GetX() > 0 && TileGrid[location.GetX() - 1, location.GetZ()] != TileSet.Empty)
        {
            if (GetIfEastValid(new GridLocation(location.GetX() - 1, location.GetZ())))
            {
                possibleTiles.Remove(TileSet.NorthEast);
                possibleTiles.Remove(TileSet.NorthSouth);
                possibleTiles.Remove(TileSet.EastSouth);
                possibleTiles.Remove(TileSet.NoWest);
            }
            else
            {
                possibleTiles.Remove(TileSet.NorthWest);
                possibleTiles.Remove(TileSet.EastWest);
                possibleTiles.Remove(TileSet.SouthWest);
                possibleTiles.Remove(TileSet.NoNorth);
                possibleTiles.Remove(TileSet.NoEast);
                possibleTiles.Remove(TileSet.NoSouth);
                possibleTiles.Remove(TileSet.Cross);
            }
        }
        return possibleTiles;
    }

    TileSet GetTile(GridLocation location)
    {
        ArrayList possibleTiles = GetListOfTiles();

        possibleTiles.Remove(TileSet.Empty);
        possibleTiles.Remove(TileSet.None);
        possibleTiles.Remove(TileSet.EndNorth);
        possibleTiles.Remove(TileSet.EndEast);
        possibleTiles.Remove(TileSet.EndSouth);
        possibleTiles.Remove(TileSet.EndWest);

        CheckIfAtGridEdge(location, possibleTiles);
        CheckIfNorthPossible(location, possibleTiles);
        CheckIfEastPossible(location, possibleTiles);
        CheckIfSouthPossible(location, possibleTiles);
        CheckIfWestPossible(location, possibleTiles);

        if (possibleTiles.Count == 0)
        {
            return TileSet.Empty;
        }
        else
        {
            return (TileSet)possibleTiles[Random.Range(0, possibleTiles.Count)];
        }
    }

    void PlaceTiles()
    {
        for (int i = 0; i < GetDungeonSize(); i++)
        {
            for (int j = 0; j < GetDungeonSize(); j++)
            {
                if (TileGrid[i, j] == TileSet.Empty)
                {
                    TileGrid[i, j] = GetTile(new GridLocation(i, j));
                }
            }
        }
    }

    bool GetIfTileIsConnected(int startLocationX, int startLocationZ, int endLocationX, int endLocationZ, ref ArrayList existingConnections)
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

            if ((toSearch.GetX() == endLocationX && toSearch.GetZ() == endLocationZ) || existingConnections.Contains(toSearch))
            {
                searchCompleted = true;
                pathFound = true;

                foreach (GridLocation position in searchedList)
                {
                    existingConnections.Add(position);
                }

                foreach (GridLocation position in toSearchList)
                {
                    existingConnections.Add(position);
                }

                break;
            }
            else
            {
                if (GetIfNorthValid(new GridLocation(toSearch.GetX(), toSearch.GetZ())))
                {
                    GridLocation newLocation = new GridLocation(toSearch.GetX(), toSearch.GetZ() + 1);

                    if (toSearchList.Contains(newLocation) == false && searchedList.Contains(newLocation) == false)
                    {
                        toSearchList.Add(newLocation);
                    }
                }

                if (GetIfEastValid(new GridLocation(toSearch.GetX(), toSearch.GetZ())))
                {
                    GridLocation newLocation = new GridLocation(toSearch.GetX() + 1, toSearch.GetZ());

                    if (toSearchList.Contains(newLocation) == false && searchedList.Contains(newLocation) == false)
                    {
                        toSearchList.Add(newLocation);
                    }

                }

                if (GetIfSouthValid(new GridLocation(toSearch.GetX(), toSearch.GetZ())))
                {
                    GridLocation newLocation = new GridLocation(toSearch.GetX(), toSearch.GetZ() - 1);

                    if (toSearchList.Contains(newLocation) == false && searchedList.Contains(newLocation) == false)
                    {
                        toSearchList.Add(newLocation);
                    }
                }

                if (GetIfWestValid(new GridLocation(toSearch.GetX(), toSearch.GetZ())))
                {
                    GridLocation newLocation = new GridLocation(toSearch.GetX() - 1, toSearch.GetZ());

                    if (toSearchList.Contains(newLocation) == false && searchedList.Contains(newLocation) == false)
                    {
                        toSearchList.Add(newLocation);
                    }
                }
            }
        }
        return pathFound;
    }

    void ReplaceOrphans()
    {
        ArrayList existingConnections = new ArrayList();

        for (int i = 0; i < GetDungeonSize(); i++)
        {
            for (int j = 0; j < GetDungeonSize(); j++)
            {
                if (TileGrid[i, j] != TileSet.Empty && !GetIfTileIsConnected(i, j, GetDungeonSize() / 2, GetDungeonSize() / 2, ref existingConnections))
                {
                    TileGrid[i, j] = TileSet.Empty;
                }
            }
        }
    }

    TileSet GetValidEndTile(int locationX, int locationZ)
    {
        if (locationZ < GetDungeonSize() - 1 && GetIfSouthValid(new GridLocation(locationX, locationZ + 1)))
        {
            return TileSet.EndNorth;
        }

        if (locationX < GetDungeonSize() - 1 && GetIfWestValid(new GridLocation(locationX + 1, locationZ)))
        {
            return TileSet.EndEast;
        }

        if (locationZ > 0 && GetIfNorthValid(new GridLocation(locationX, locationZ - 1)))
        {
            return TileSet.EndSouth;
        }

        if (locationX > 0 && GetIfEastValid(new GridLocation(locationX - 1, locationZ)))
        {
            return TileSet.EndWest;
        }

        return TileSet.Empty;
    }

    void CapEnds()
    {
        for (int i = 0; i < GetDungeonSize(); i++)
        {
            for (int j = 0; j < GetDungeonSize(); j++)
            {
                if (TileGrid[i, j] == TileSet.Empty)
                {
                    TileGrid[i, j] = GetValidEndTile(i, j);
                }
            }
        }
    }

    void FillGrid()
    {
        for (int i = 0; i < GetDungeonSize(); i++)
        {
            for (int j = 0; j < GetDungeonSize(); j++)
            {
                TileGrid[i, j] = TileSet.Empty;
            }
        }

        PlaceTiles();
        ReplaceOrphans();
        CapEnds();
    }

    ArrayList GetPotentialObjectLocations()
    {
        ArrayList returnList = new ArrayList();

        for (int i = 0; i < GetDungeonSize(); i++)
        {
            for (int j = 0; j < GetDungeonSize(); j++)
            {
                if (TileGrid[i, j] != TileSet.Empty)
                {
                    returnList.Add(new GridLocation(i, j));
                }
            }
        }
        return returnList;
    }

    void InitializeStairsUp(GridLocation location)
    {
        int stairsUpGridX = location.GetX();
        int stairsUpGridZ = location.GetZ();

        float stairsUpLocationX = transform.position.x + (stairsUpGridX * GetTileSize());
        float stairsUpLocationZ = transform.position.z + (stairsUpGridZ * GetTileSize());

        Transform createStairsUp = (Transform)Instantiate(StairsUp, new Vector3(stairsUpLocationX, 0.75f, stairsUpLocationZ), Quaternion.identity);
        StairsUpLocation = new GridLocation(stairsUpGridX, stairsUpGridZ);
        createStairsUp.parent = StairsParent;
    }

    void InitializeStairsDown(GridLocation location)
    {
        int stairsDownGridX = location.GetX();
        int stairsDownGridZ = location.GetZ();

        float stairsDownLocationX = transform.position.x + (stairsDownGridX * GetTileSize());
        float stairsDownLocationZ = transform.position.z + (stairsDownGridZ * GetTileSize());

        Transform createStairsDown = (Transform)Instantiate(StairsDown, new Vector3(stairsDownLocationX, 0.6f, stairsDownLocationZ), Quaternion.identity);
        StairsDownLocation = new GridLocation(stairsDownGridX, stairsDownGridZ);
        createStairsDown.parent = StairsParent;

    }

    void InitializeKey(GridLocation location)
    {
        int keyGridX = location.GetX();
        int keyGridZ = location.GetZ();

        float keyLocationX = transform.position.x + (keyGridX * GetTileSize());
        float keyLocationZ = transform.position.z + (keyGridZ * GetTileSize());

        Transform createKey = (Transform)Instantiate(Key, new Vector3(keyLocationX, 0.6f, keyLocationZ), Quaternion.Euler(0, 135, -90));
        KeyLocation = new GridLocation(keyGridX, keyGridZ);
        createKey.parent = KeyParent;

    }

    void InitializeOilCan(GridLocation location)
    {
        int OilCanGridX = location.GetX();
        int OilCanGridZ = location.GetZ();

        float OilCanLocationX = transform.position.x + (OilCanGridX * GetTileSize());
        float OilCanLocationZ = transform.position.z + (OilCanGridZ * GetTileSize());

        Transform createOilCan = (Transform)Instantiate(OilCan, new Vector3(OilCanLocationX, 0.9f, OilCanLocationZ), Quaternion.Euler(0, 180, 0));
        OilCanLocation = new GridLocation(OilCanGridX, OilCanGridZ);
        createOilCan.parent = OilCanParent;

    }

    void PlaceObjects()
    {
        ArrayList potentialObjectPositions = GetPotentialObjectLocations();

        int chosenUpIndex = Random.Range(0, potentialObjectPositions.Count);
        GridLocation locationStairsUp = (GridLocation)potentialObjectPositions[chosenUpIndex];
        potentialObjectPositions.RemoveAt(chosenUpIndex);

        int chosenDownIndex = Random.Range(0, potentialObjectPositions.Count);
        GridLocation locationStairsDown = (GridLocation)potentialObjectPositions[chosenDownIndex];
        potentialObjectPositions.RemoveAt(chosenDownIndex);

        int chosenKeyIndex = Random.Range(0, potentialObjectPositions.Count);
        GridLocation locationKey = (GridLocation)potentialObjectPositions[chosenKeyIndex];
        potentialObjectPositions.RemoveAt(chosenKeyIndex);

        int chosenOilCanIndex = Random.Range(0, potentialObjectPositions.Count);
        GridLocation locationOilCan = (GridLocation)potentialObjectPositions[chosenOilCanIndex];

        InitializeStairsUp(locationStairsUp);
        InitializeStairsDown(locationStairsDown);
        InitializeKey(locationKey);
        InitializeOilCan(locationOilCan);
    }

    void InitializeTiles()
    {
        for (int i = 0; i < GetDungeonSize(); i++)
        {
            for (int j = 0; j < GetDungeonSize(); j++)
            {
                if (TileGrid[i, j] != TileSet.None)
                {
                    float locationX = transform.position.x + (i * GetTileSize());
                    float locationZ = transform.position.z + (j * GetTileSize());

                    Transform selectedTile = Tile.GetPrefabFromTileSet(TileGrid[i, j]);
                    Transform createTile = (Transform)Instantiate(selectedTile, new Vector3(locationX, 0.0f, locationZ), Quaternion.identity);

                    createTile.parent = TileParent;
                }
            }
        }
    }

    public void SetAdventurerLocation(GridLocation location, Quaternion direction)
    {
        AdventurerLocation = location;

        float positionX = transform.position.x + (location.GetX() * GetTileSize());
        float positionZ = transform.position.z + (location.GetZ() * GetTileSize());

        Adventurer.transform.position = new Vector3(positionX, 1.0f, positionZ);
        Adventurer.transform.rotation = direction;
        Camera.transform.position = new Vector3(positionX, 12.0f, positionZ);
    }

    void CreateLevel()
    {
        FillGrid();
        PlaceObjects();
        InitializeTiles();
        SetAdventurerLocation(new GridLocation(StairsUpLocation.GetX(), StairsUpLocation.GetZ()), Quaternion.identity);
        Adventurer.SetKeyFound(false);
    }

    void DeleteFloor()
    {
        for (int i = 0; i < TileParent.childCount; i++)
        {
            Destroy(TileParent.GetChild(i).gameObject);
        }
    }

    void DeleteStairs()
    {
        for (int i = 0; i < StairsParent.childCount; i++)
        {
            Destroy(StairsParent.GetChild(i).gameObject);
        }
    }

    void DeleteKey()
    {
        for (int i = 0; i < KeyParent.childCount; i++)
        {
            Destroy(KeyParent.GetChild(i).gameObject);
        }
    }

    void DeleteOilCan()
    {
        for (int i = 0; i < OilCanParent.childCount; i++)
        {
            Destroy(OilCanParent.GetChild(i).gameObject);
        }
    }

    public void DeleteLevel()
    {
        DeleteFloor();
        DeleteStairs();
        DeleteKey();
        DeleteOilCan();
    }

    public void GoToNextFloor()
    {
        SetFloorNumber(GetFloorNumber() + 1);
        DeleteLevel();
        CreateLevel();
    }

    public void InitializeGame(int dungeonSize, int light)
    {
        SetFloorNumber(1);
        SetDungeonSize(dungeonSize);
        Adventurer.SetLight(light);
    }

    public void StartGame()
    {
        SetTileSize(Tile.Cross.localScale.x);
        TileGrid = new TileSet[DungeonSize, DungeonSize];
        CreateLevel();
    }
}
