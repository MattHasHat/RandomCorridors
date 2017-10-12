using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSpawner : MonoBehaviour
{
    private enum TileSet { None, Empty, EndNorth, EndEast, EndSouth, EndWest, NorthEast, NorthSouth, NorthWest, EastSouth, EastWest, SouthWest, NoNorth, NoEast, NoSouth, NoWest, Cross }
    public enum Direction { North, East, South, West }

    public ScreenManager ScreenManager;
    public Adventurer Adventurer;

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

    public Transform PrefabStairsUp;
    public Transform PrefabStairsDown;

    public Material MaterialOverrideStairsUp;
    public Material MaterialOverrideStairsDown;

    public Transform PrefabBuildingBlockParent;
    public Transform OtherStuffParent;
    public int DungeonWidth;
    public int DungeonHeight;

    private float TileWidth;
    private float TileHeight;

    private GridLocation AdventurerLocation;
    private GridLocation StairsUpLocation;
    private GridLocation StairsDownLocation;

    [HideInInspector]
    public int DungeonFloorNumber = 1;

    private TileSet[,] TileSetGrid;

    Transform GetPrefabFromTileSet(TileSet tile)
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

    TileSet GetRandomTile()
    {
        ArrayList allTiles = GetListOfAllTiles();
        return (TileSet)allTiles[Random.Range(0, allTiles.Count)];
    }

    void InitializeDungeonTileFromGrid()
    {
        TileWidth = Tile_Cross.localScale.x;
        TileHeight = Tile_Cross.localScale.z;

        for (int i = 0; i < DungeonWidth; i++)
        {
            for (int j = 0; j < DungeonHeight; j++)
            {
                if (TileSetGrid[i, j] != TileSet.None)
                {
                    float locationX = transform.position.x + (i * TileWidth);
                    float locationY = transform.position.z + (j * TileHeight);
                    Transform prefabToMake = GetPrefabFromTileSet(TileSetGrid[i, j]);
                    Transform createBlock = (Transform)Instantiate(prefabToMake, new Vector3(locationX, 0.0f, locationY), Quaternion.identity);

                    if (MaterialOverrideStairsUp != null && i == StairsUpLocation.x && j == StairsUpLocation.z)
                    {
                        for (int childIndex = 0; childIndex < createBlock.childCount; childIndex++)
                        {
                            Transform child = createBlock.GetChild(childIndex);
                            child.GetComponent<Renderer>().material = MaterialOverrideStairsUp;
                        }
                    }
                    else if (MaterialOverrideStairsDown != null && i == StairsDownLocation.x && j == StairsDownLocation.z)
                    {
                        for (int childIndex = 0; childIndex < createBlock.childCount; ++childIndex)
                        {
                            Transform child = createBlock.GetChild(childIndex);
                            child.GetComponent<Renderer>().material = MaterialOverrideStairsDown;
                        }
                    }

                    createBlock.parent = PrefabBuildingBlockParent;
                }
            }
        }
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

    TileSet GetPossibleTile(int locationX, int locationZ, bool westValid, bool northValid, bool eastValid, bool southValid, bool westNotValid, bool northNotValid, bool eastNotValid, bool southNotValid, bool useDeadEnds)
    {
        if (useDeadEnds)
        {
            return TileSet.Empty;
        }

        ArrayList possibleTiles = GetListOfAllTiles();

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

    bool GridLocationValidEast(GridLocation location)
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

    bool GridLocationValidWest(GridLocation location)
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

    bool GridLocationValidNorth(GridLocation location)
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

    bool GridLocationValidSouth(GridLocation location)
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

    ArrayList GetPotentialStairPositions()
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

    void InitializeStairs(GridLocation chosenUp, GridLocation chosenDown)
    {
        TileWidth = Tile_Cross.localScale.x;
        TileHeight = Tile_Cross.localScale.z;

        int stairsUpGridX = chosenUp.x;
        int stairsUpGridZ = chosenUp.z;
        float stairsUpLocX = transform.position.x + (stairsUpGridX * TileWidth);
        float stairsUpLocZ = transform.position.z + (stairsUpGridZ * TileHeight);
        Transform createStairsUp = (Transform)Instantiate(PrefabStairsUp, new Vector3(stairsUpLocX, 0.0f, stairsUpLocZ), Quaternion.identity);

        int stairsDownGridX = chosenDown.x;
        int stairsDownGridZ = chosenDown.z;
        float stairsDownLocX = transform.position.x + (stairsDownGridX * TileWidth);
        float stairsDownLocZ = transform.position.z + (stairsDownGridZ * TileHeight);
        Transform createStairsDown = (Transform)Instantiate(PrefabStairsDown, new Vector3(stairsDownLocX, 0.0f, stairsDownLocZ), Quaternion.identity);

        StairsUpLocation = new GridLocation(stairsUpGridX, stairsUpGridZ);
        StairsDownLocation = new GridLocation(stairsDownGridX, stairsDownGridZ);

        createStairsUp.parent = OtherStuffParent;
        createStairsDown.parent = OtherStuffParent;
    }

    void PlaceStairs()
    {
        ArrayList potentialStairPositions = GetPotentialStairPositions();

        int chosenUpIndex = Random.Range(0, potentialStairPositions.Count);
        GridLocation chosenUp = (GridLocation)potentialStairPositions[chosenUpIndex];
        potentialStairPositions.RemoveAt(chosenUpIndex);
        int chosenDownIndex = Random.Range(0, potentialStairPositions.Count);
        GridLocation chosenDown = (GridLocation)potentialStairPositions[chosenDownIndex];

        InitializeStairs(chosenUp, chosenDown);
    }

    void CreateDungeonFloor()
    {
        FillDungeonTileGrid();
        PlaceStairs();
        InitializeDungeonTileFromGrid();
        SetAdventurerLocation(new GridLocation(StairsUpLocation.x, StairsUpLocation.z));
    }

    void Start()
    {
        TileSetGrid = new TileSet[DungeonWidth, DungeonHeight];
        CreateDungeonFloor();
    }

    void DeleteDungeonFloor()
    {
        for (int i = 0; i < PrefabBuildingBlockParent.childCount; i++)
        {
            Destroy(PrefabBuildingBlockParent.GetChild(i).gameObject);
        }

        for (int i = 0; i < OtherStuffParent.childCount; i++)
        {
            Destroy(OtherStuffParent.GetChild(i).gameObject);
        }
    }

    public void GoToNextFloor()
    {
        DeleteDungeonFloor();
        CreateDungeonFloor();
        DungeonFloorNumber++;
    }

    public bool IsOnStairs()
    {
        return AdventurerLocation.x == StairsDownLocation.x && AdventurerLocation.z == StairsDownLocation.z;
    }

    void SetAdventurerLocation(GridLocation location)
    {
        AdventurerLocation = location;

        TileWidth = Tile_Cross.localScale.x;
        TileHeight = Tile_Cross.localScale.z;

        float heroPositionX = transform.position.x + (location.x * TileWidth);
        float heroPositionZ = transform.position.z + (location.z * TileHeight);

        Adventurer.transform.position = new Vector3(heroPositionX, 1.0f, heroPositionZ);
    }

    public bool MoveAdventurer(Direction direction)
    {
        if (direction == Direction.North && GridLocationValidNorth(AdventurerLocation))
        {
            SetAdventurerLocation(new GridLocation(AdventurerLocation.x, AdventurerLocation.z + 1));
            return true;
        }
        else if (direction == Direction.West && GridLocationValidWest(AdventurerLocation))
        {
            SetAdventurerLocation(new GridLocation(AdventurerLocation.x - 1, AdventurerLocation.z));
            return true;
        }
        else if (direction == Direction.South && GridLocationValidSouth(AdventurerLocation))
        {
            SetAdventurerLocation(new GridLocation(AdventurerLocation.x, AdventurerLocation.z - 1));
            return true;
        }
        else if (direction == Direction.East && GridLocationValidEast(AdventurerLocation))
        {
            SetAdventurerLocation(new GridLocation(AdventurerLocation.x + 1, AdventurerLocation.z));
            return true;
        }
        return false;
    }
}
