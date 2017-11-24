using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adventurer : MonoBehaviour
{
    private int Light;
    private bool KeyFound;

    public LevelGenerator LevelGenerator;

    private AudioSource AudioSource;
    public AudioClip Step;
    public AudioClip PickupOil;
    public AudioClip PickupKey;

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
        if (Input.GetKeyUp(KeyCode.W) && LevelGenerator.IsNorthValid(LevelGenerator.AdventurerLocation))
        {
            LevelGenerator.SetAdventurerLocation(new GridLocation(LevelGenerator.AdventurerLocation.GetX(), LevelGenerator.AdventurerLocation.GetZ() + 1), Quaternion.Euler(0, 0, 0));
            SetLight(GetLight() - 1);
            AudioSource.clip = Step;
            AudioSource.Play();
        }
    }

    void MoveEast()
    {
        if (Input.GetKeyUp(KeyCode.D) && LevelGenerator.IsEastValid(LevelGenerator.AdventurerLocation))
        {
            LevelGenerator.SetAdventurerLocation(new GridLocation(LevelGenerator.AdventurerLocation.GetX() + 1, LevelGenerator.AdventurerLocation.GetZ()), Quaternion.Euler(0, 90, 0));
            SetLight(GetLight() - 1);
            AudioSource.clip = Step;
            AudioSource.Play();
        }
    }

    void MoveSouth()
    {
        if (Input.GetKeyUp(KeyCode.S) && LevelGenerator.IsSouthValid(LevelGenerator.AdventurerLocation))
        {
            LevelGenerator.SetAdventurerLocation(new GridLocation(LevelGenerator.AdventurerLocation.GetX(), LevelGenerator.AdventurerLocation.GetZ() - 1), Quaternion.Euler(0, 180, 0));
            SetLight(GetLight() - 1);
            AudioSource.clip = Step;
            AudioSource.Play();
        }
    }

    void MoveWest()
    {
        if (Input.GetKeyUp(KeyCode.A) && LevelGenerator.IsWestValid(LevelGenerator.AdventurerLocation))
        {
            LevelGenerator.SetAdventurerLocation(new GridLocation(LevelGenerator.AdventurerLocation.GetX() - 1, LevelGenerator.AdventurerLocation.GetZ()), Quaternion.Euler(0, 270, 0));
            SetLight(GetLight() - 1);
            AudioSource.clip = Step;
            AudioSource.Play();
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
            SetLight(GetLight() + 25);
            AudioSource.clip = PickupOil;
            AudioSource.Play();
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
            if (!GetKeyFound())
            {
                AudioSource.clip = PickupKey;
                AudioSource.Play();
            }

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

    void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
    }
}
