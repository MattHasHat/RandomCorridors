using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adventurer : MonoBehaviour
{
    public LevelGenerator LevelGenerator;
    public int Stamina;
    public bool KeyFound;
    
    public void HandleMovementInput()
    {
        if (Input.GetKeyUp(KeyCode.W) && LevelGenerator.GridLocationValidNorth(LevelGenerator.AdventurerLocation))
        {
            LevelGenerator.SetAdventurerLocation(new GridLocation(LevelGenerator.AdventurerLocation.x, LevelGenerator.AdventurerLocation.z + 1), Quaternion.Euler(0, 0, 0));
            Stamina--;
        }
        else if (Input.GetKeyUp(KeyCode.D) && LevelGenerator.GridLocationValidEast(LevelGenerator.AdventurerLocation))
        {
            LevelGenerator.SetAdventurerLocation(new GridLocation(LevelGenerator.AdventurerLocation.x + 1, LevelGenerator.AdventurerLocation.z), Quaternion.Euler(0, 90, 0));
            Stamina--;
        }
        else if (Input.GetKeyUp(KeyCode.S) && LevelGenerator.GridLocationValidSouth(LevelGenerator.AdventurerLocation))
        {
            LevelGenerator.SetAdventurerLocation(new GridLocation(LevelGenerator.AdventurerLocation.x, LevelGenerator.AdventurerLocation.z - 1), Quaternion.Euler(0, 180, 0));
            Stamina--;
        }
        else if (Input.GetKeyUp(KeyCode.A) && LevelGenerator.GridLocationValidWest(LevelGenerator.AdventurerLocation))
        {
            LevelGenerator.SetAdventurerLocation(new GridLocation(LevelGenerator.AdventurerLocation.x - 1, LevelGenerator.AdventurerLocation.z), Quaternion.Euler(0, 270, 0));
            Stamina--;
        }
    }

    public bool HaveFoundKey()
    {
        if (LevelGenerator.AdventurerLocation.x == LevelGenerator.KeyLocation.x && LevelGenerator.AdventurerLocation.z == LevelGenerator.KeyLocation.z)
        {
            KeyFound = true;
        }
        return KeyFound;
    }

    public bool IsOnStairs()
    {
        return LevelGenerator.AdventurerLocation.x == LevelGenerator.StairsDownLocation.x && LevelGenerator.AdventurerLocation.z == LevelGenerator.StairsDownLocation.z;
    }
}
