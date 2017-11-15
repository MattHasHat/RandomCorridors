using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DifficultyLevel { Easy, Normal, Hard }

public class Difficulty : MonoBehaviour
{
    private DifficultyLevel CurrentDifficulty;

    private int DungeonSize;
    private int Light;

    public DifficultyLevel GetDifficultyLevel()
    {
        return CurrentDifficulty;
    }

    public void SetDifficultyLevel(DifficultyLevel difficultyLevel)
    {
        CurrentDifficulty = difficultyLevel;
    }

    public Difficulty(int dungeonSize, int light)
    {
        DungeonSize = dungeonSize;
        Light = light;
    }

    public int GetDungeonSize()
    {
        return DungeonSize;
    }

    public int GetLight()
    {
        return Light;
    }

    public void SetDungeonSize(int dungeonSize)
    {
        DungeonSize = dungeonSize;
    }

    public void SetLight(int light)
    {
        Light = light;
    }

    void Start()
    {
        SetDifficultyLevel(DifficultyLevel.Normal);
    }
}
