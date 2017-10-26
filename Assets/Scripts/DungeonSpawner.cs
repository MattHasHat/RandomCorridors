using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSpawner : MonoBehaviour
{
    public ScreenManager ScreenManager;
    public Adventurer Adventurer;
    public Tile Tile;

    public Transform PrefabStairsUp;
    public Transform PrefabStairsDown;

    public Material MaterialOverrideStairsUp;
    public Material MaterialOverrideStairsDown;

    public Transform PrefabBuildingBlockParent;
    public Transform OtherStuffParent;
    public int DungeonWidth;
    public int DungeonHeight;

    public float TileWidth;
    public float TileHeight;

    public GridLocation AdventurerLocation;
    public GridLocation StairsUpLocation;
    public GridLocation StairsDownLocation;

    [HideInInspector]
    public int DungeonFloorNumber = 1;

    public TileSet[,] TileSetGrid;

    void InitializeDungeonTileFromGrid()
    {
        TileWidth = Tile.Tile_Cross.localScale.x;
        TileHeight = Tile.Tile_Cross.localScale.z;

        for (int i = 0; i < DungeonWidth; i++)
        {
            for (int j = 0; j < DungeonHeight; j++)
            {
                if (TileSetGrid[i, j] != TileSet.None)
                {
                    float locationX = transform.position.x + (i * TileWidth);
                    float locationY = transform.position.z + (j * TileHeight);
                    Transform prefabToMake = Tile.GetPrefabFromTileSet(TileSetGrid[i, j]);
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
        TileWidth = Tile.Tile_Cross.localScale.x;
        TileHeight = Tile.Tile_Cross.localScale.z;

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

    public void SetAdventurerLocation(GridLocation location)
    {
        AdventurerLocation = location;

        TileWidth = Tile.Tile_Cross.localScale.x;
        TileHeight = Tile.Tile_Cross.localScale.z;

        float positionX = transform.position.x + (location.x * TileWidth);
        float positionZ = transform.position.z + (location.z * TileHeight);

        Adventurer.transform.position = new Vector3(positionX, 1.0f, positionZ);
    }
}
