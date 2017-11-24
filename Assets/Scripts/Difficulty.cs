using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DifficultyLevel { Easy, Normal, Hard }

public class Difficulty : MonoBehaviour
{
    private int DungeonSize;
    private int Light;
    private float SpecterSpeed;

    private DifficultyLevel CurrentDifficulty;

    public DifficultyLevel GetDifficultyLevel()
    {
        return CurrentDifficulty;
    }

    public void SetDifficultyLevel(DifficultyLevel difficultyLevel)
    {
        CurrentDifficulty = difficultyLevel;
    }

    public Difficulty(int dungeonSize, int light, float specterSpeed)
    {
        DungeonSize = dungeonSize;
        Light = light;
        SpecterSpeed = specterSpeed;
    }

    public int GetDungeonSize()
    {
        return DungeonSize;
    }

    public int GetLight()
    {
        return Light;
    }

    public float GetSpecterSpeed()
    {
        return SpecterSpeed;
    }

    public void SetDungeonSize(int dungeonSize)
    {
        DungeonSize = dungeonSize;
    }

    public void SetLight(int light)
    {
        Light = light;
    }

    public void SetSpecterSpeed(float specterSpeed)
    {
        SpecterSpeed = specterSpeed;
    }

    void Start()
    {
        SetDifficultyLevel(DifficultyLevel.Normal);
    }
}
