using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adventurer : MonoBehaviour
{
    public DungeonSpawner DungeonSpawner;
    public int Stamina = 500;
    public bool KeyFound = false;
    
    public void HandleMovementInput()
    {
        if (Input.GetKeyUp(KeyCode.W) && DungeonSpawner.GridLocationValidNorth(DungeonSpawner.AdventurerLocation))
        {
            DungeonSpawner.SetAdventurerLocation(new GridLocation(DungeonSpawner.AdventurerLocation.x, DungeonSpawner.AdventurerLocation.z + 1), Quaternion.Euler(0, 0, 0));
            Stamina--;
        }
        else if (Input.GetKeyUp(KeyCode.D) && DungeonSpawner.GridLocationValidEast(DungeonSpawner.AdventurerLocation))
        {
            DungeonSpawner.SetAdventurerLocation(new GridLocation(DungeonSpawner.AdventurerLocation.x + 1, DungeonSpawner.AdventurerLocation.z), Quaternion.Euler(0, 90, 0));
            Stamina--;
        }
        else if (Input.GetKeyUp(KeyCode.S) && DungeonSpawner.GridLocationValidSouth(DungeonSpawner.AdventurerLocation))
        {
            DungeonSpawner.SetAdventurerLocation(new GridLocation(DungeonSpawner.AdventurerLocation.x, DungeonSpawner.AdventurerLocation.z - 1), Quaternion.Euler(0, 180, 0));
            Stamina--;
        }
        else if (Input.GetKeyUp(KeyCode.A) && DungeonSpawner.GridLocationValidWest(DungeonSpawner.AdventurerLocation))
        {
            DungeonSpawner.SetAdventurerLocation(new GridLocation(DungeonSpawner.AdventurerLocation.x - 1, DungeonSpawner.AdventurerLocation.z), Quaternion.Euler(0, 270, 0));
            Stamina--;
        }

    }

    public bool HaveFoundKey()
    {
        if (DungeonSpawner.AdventurerLocation.x == DungeonSpawner.KeyLocation.x && DungeonSpawner.AdventurerLocation.z == DungeonSpawner.KeyLocation.z)
        {
            KeyFound = true;
        }
        return KeyFound;
    }

    public bool IsOnStairs()
    {
        return DungeonSpawner.AdventurerLocation.x == DungeonSpawner.StairsDownLocation.x && DungeonSpawner.AdventurerLocation.z == DungeonSpawner.StairsDownLocation.z;
    }
}
