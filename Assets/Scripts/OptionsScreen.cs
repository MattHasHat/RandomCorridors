using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsScreen : MonoBehaviour
{
    private string Difficulty;

    public ScreenChanger ScreenChanger;
    public LevelGenerator LevelGenerator;
    public Texture2D Background;

    public string GetDifficulty()
    {
        return Difficulty;
    }

    public void SetDifficulty(string difficulty)
    {
        Difficulty = difficulty;
    }

    void Start()
    {
        SetDifficulty("Normal");
        LevelGenerator.InitializeGame(10, 300);
    }

    void OnGUI()
    {
        if (ScreenChanger.GetScreen() != ScreenState.OptionsScreen)
        {
            return;
        }

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Background);

        GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 10, 200, 25), "Difficulty: " + GetDifficulty());

        if (GUI.Button(new Rect(Screen.width / 2 - 250, Screen.height / 2 + 50, 150, 40), "Easy"))
        {
            SetDifficulty("Easy");
            LevelGenerator.InitializeGame(5, 200);
        }

        if (GUI.Button(new Rect(Screen.width / 2 - 75, Screen.height / 2 + 50, 150, 40), "Normal"))
        {
            SetDifficulty("Normal");
            LevelGenerator.InitializeGame(10, 300);
        }

        if (GUI.Button(new Rect(Screen.width / 2 + 100, Screen.height / 2 + 50, 150, 40), "Hard"))
        {
            SetDifficulty("Hard");
            LevelGenerator.InitializeGame(15, 400);
        }

        if (GUI.Button(new Rect(Screen.width / 2 - 75, Screen.height / 2 + 110, 150, 40), "Back to Main Menu"))
        {
            ScreenChanger.SetScreen(ScreenState.MainMenuScreen);
        }
    }
}