using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adventurer : MonoBehaviour
{
    private int Stamina;
    private bool KeyFound;

    public LevelGenerator LevelGenerator;

    public int GetStamina()
    {
        return Stamina;
    }

    public bool GetKeyFound()
    {
        return KeyFound;
    }

    public void SetStamina(int stamina)
    {
        Stamina = stamina;
    }

    public void SetKeyFound(bool keyFound)
    {
        KeyFound = keyFound;
    }

    public void MoveNorth()
    {
        if (Input.GetKeyUp(KeyCode.W) && LevelGenerator.GridLocationValidNorth(LevelGenerator.AdventurerLocation))
        {

            LevelGenerator.SetAdventurerLocation(new GridLocation(LevelGenerator.AdventurerLocation.GetX(), LevelGenerator.AdventurerLocation.GetZ() + 1), Quaternion.Euler(0, 0, 0));
            SetStamina(GetStamina() - 1);
        }
    }

    public void MoveEast()
    {
        if (Input.GetKeyUp(KeyCode.D) && LevelGenerator.GridLocationValidEast(LevelGenerator.AdventurerLocation))
        {
            LevelGenerator.SetAdventurerLocation(new GridLocation(LevelGenerator.AdventurerLocation.GetX() + 1, LevelGenerator.AdventurerLocation.GetZ()), Quaternion.Euler(0, 90, 0));
            SetStamina(GetStamina() - 1);
        }
    }

    public void MoveSouth()
    {
        if (Input.GetKeyUp(KeyCode.S) && LevelGenerator.GridLocationValidSouth(LevelGenerator.AdventurerLocation))
        {
            LevelGenerator.SetAdventurerLocation(new GridLocation(LevelGenerator.AdventurerLocation.GetX(), LevelGenerator.AdventurerLocation.GetZ() - 1), Quaternion.Euler(0, 180, 0));
            SetStamina(GetStamina() - 1);
        }
    }

    public void MoveWest()
    {
        if (Input.GetKeyUp(KeyCode.A) && LevelGenerator.GridLocationValidWest(LevelGenerator.AdventurerLocation))
        {
            LevelGenerator.SetAdventurerLocation(new GridLocation(LevelGenerator.AdventurerLocation.GetX() - 1, LevelGenerator.AdventurerLocation.GetZ()), Quaternion.Euler(0, 270, 0));
            SetStamina(GetStamina() - 1);
        }
    }

    public void MoveAdventurer()
    {
        MoveNorth();
        MoveEast();
        MoveSouth();
        MoveWest();
    }

    public void HaveFoundKey()
    {
        if (LevelGenerator.AdventurerLocation.GetX() == LevelGenerator.KeyLocation.GetX() && LevelGenerator.AdventurerLocation.GetZ() == LevelGenerator.KeyLocation.GetZ())
        {
            SetKeyFound(true);

            for (int i = 0; i < LevelGenerator.KeyParent.childCount; i++)
            {
                Destroy(LevelGenerator.KeyParent.GetChild(i).gameObject);
            }
        }
    }

    public bool IsOnStairs()
    {
        return LevelGenerator.AdventurerLocation.GetX() == LevelGenerator.StairsDownLocation.GetX() && LevelGenerator.AdventurerLocation.GetZ() == LevelGenerator.StairsDownLocation.GetZ();
    }
}
