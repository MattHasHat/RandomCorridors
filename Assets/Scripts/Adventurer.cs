using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adventurer : MonoBehaviour
{
    private int Light;
    private bool KeyFound;

    public LevelGenerator LevelGenerator;

    public int GetLight()
    {
        return Light;
    }

    public bool GetKeyFound()
    {
        return KeyFound;
    }

    public void SetLight(int light)
    {
        Light = light;
    }

    public void SetKeyFound(bool keyFound)
    {
        KeyFound = keyFound;
    }

    void MoveNorth()
    {
        if (Input.GetKeyUp(KeyCode.W) && LevelGenerator.GetIfNorthValid(LevelGenerator.AdventurerLocation))
        {
            LevelGenerator.SetAdventurerLocation(new GridLocation(LevelGenerator.AdventurerLocation.GetX(), LevelGenerator.AdventurerLocation.GetZ() + 1), Quaternion.Euler(0, 0, 0));
            SetLight(GetLight() - 1);
        }
    }

    void MoveEast()
    {
        if (Input.GetKeyUp(KeyCode.D) && LevelGenerator.GetIfEastValid(LevelGenerator.AdventurerLocation))
        {
            LevelGenerator.SetAdventurerLocation(new GridLocation(LevelGenerator.AdventurerLocation.GetX() + 1, LevelGenerator.AdventurerLocation.GetZ()), Quaternion.Euler(0, 90, 0));
            SetLight(GetLight() - 1);
        }
    }

    void MoveSouth()
    {
        if (Input.GetKeyUp(KeyCode.S) && LevelGenerator.GetIfSouthValid(LevelGenerator.AdventurerLocation))
        {
            LevelGenerator.SetAdventurerLocation(new GridLocation(LevelGenerator.AdventurerLocation.GetX(), LevelGenerator.AdventurerLocation.GetZ() - 1), Quaternion.Euler(0, 180, 0));
            SetLight(GetLight() - 1);
        }
    }

    void MoveWest()
    {
        if (Input.GetKeyUp(KeyCode.A) && LevelGenerator.GetIfWestValid(LevelGenerator.AdventurerLocation))
        {
            LevelGenerator.SetAdventurerLocation(new GridLocation(LevelGenerator.AdventurerLocation.GetX() - 1, LevelGenerator.AdventurerLocation.GetZ()), Quaternion.Euler(0, 270, 0));
            SetLight(GetLight() - 1);
        }
    }

    public void MoveAdventurer()
    {
        MoveNorth();
        MoveEast();
        MoveSouth();
        MoveWest();
    }

    public void HaveFoundOilCan()
    {
        if (LevelGenerator.AdventurerLocation.GetX() == LevelGenerator.OilCanLocation.GetX() && LevelGenerator.AdventurerLocation.GetZ() == LevelGenerator.OilCanLocation.GetZ())
        {
            SetLight(GetLight() + 30);
            LevelGenerator.OilCanLocation.SetX(LevelGenerator.GetDungeonSize() + 1);

            for (int i = 0; i < LevelGenerator.OilCanParent.childCount; i++)
            {
                Destroy(LevelGenerator.OilCanParent.GetChild(i).gameObject);
            }
        }
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
