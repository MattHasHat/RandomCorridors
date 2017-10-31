using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adventurer : MonoBehaviour
{
    public enum Direction { North, East, South, West }

    public ScreenManager ScreenManager;
    public DungeonSpawner DungeonSpawner;

    bool ChangeFloor = false;

    void OnGUI()
    {
        if (ScreenManager.GetScreen() != ScreenState.DungeonScreen)
        {
            return;
        }
    }

    public void HandleMovementInput()
    {
        bool moveSuccess = false;

        if (Input.GetKeyUp(KeyCode.W))
        {
            MoveAdventurer(Direction.North);
            moveSuccess = true;

        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            MoveAdventurer(Direction.West);
            moveSuccess = true;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            MoveAdventurer(Direction.South);
            moveSuccess = true;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            MoveAdventurer(Direction.East);
            moveSuccess = true;
        }

        if (moveSuccess && ChangeFloor && IsOnStairs())
        {
            ScreenManager.SetScreen(ScreenState.TransitionScreen);
            return;
        }
    }

    public bool MoveAdventurer(Direction direction)
    {
        if (direction == Direction.North && DungeonSpawner.GridLocationValidNorth(DungeonSpawner.AdventurerLocation))
        {
            DungeonSpawner.SetAdventurerLocation(new GridLocation(DungeonSpawner.AdventurerLocation.x, DungeonSpawner.AdventurerLocation.z + 1), Quaternion.Euler(0, 0, 0));
            return true;
        }
        else if (direction == Direction.West && DungeonSpawner.GridLocationValidWest(DungeonSpawner.AdventurerLocation))
        {
            DungeonSpawner.SetAdventurerLocation(new GridLocation(DungeonSpawner.AdventurerLocation.x - 1, DungeonSpawner.AdventurerLocation.z), Quaternion.Euler(0, 270, 0));
            return true;
        }
        else if (direction == Direction.South && DungeonSpawner.GridLocationValidSouth(DungeonSpawner.AdventurerLocation))
        {
            DungeonSpawner.SetAdventurerLocation(new GridLocation(DungeonSpawner.AdventurerLocation.x, DungeonSpawner.AdventurerLocation.z - 1), Quaternion.Euler(0, 180, 0));
            return true;
        }
        else if (direction == Direction.East && DungeonSpawner.GridLocationValidEast(DungeonSpawner.AdventurerLocation))
        {
            DungeonSpawner.SetAdventurerLocation(new GridLocation(DungeonSpawner.AdventurerLocation.x + 1, DungeonSpawner.AdventurerLocation.z), Quaternion.Euler(0, 90, 0));
            return true;
        }
        return false;
    }

    public bool IsOnStairs()
    {
        return DungeonSpawner.AdventurerLocation.x == DungeonSpawner.StairsDownLocation.x && DungeonSpawner.AdventurerLocation.z == DungeonSpawner.StairsDownLocation.z;
    }
}
