using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsScreen : MonoBehaviour
{
    public ScreenChanger ScreenChanger;
    public LevelGenerator LevelGenerator;
    public Difficulty Difficulty;
    public Texture2D Background;

    void OnGUI()
    {
        if (ScreenChanger.GetScreen() != ScreenState.OptionsScreen)
        {
            return;
        }

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Background);

        GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 3 * 2 - 45, 200, 30), "Difficulty: " + Difficulty.GetDifficultyLevel());

        if (GUI.Button(new Rect(Screen.width / 2 - 330, Screen.height / 3 * 2, 200, 60), "Easy"))
        {
            Difficulty.SetDifficultyLevel(DifficultyLevel.Easy);
        }

        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 3 * 2, 200, 60), "Normal"))
        {
            Difficulty.SetDifficultyLevel(DifficultyLevel.Normal);
        }

        if (GUI.Button(new Rect(Screen.width / 2 + 130, Screen.height / 3 * 2, 200, 60), "Hard"))
        {
            Difficulty.SetDifficultyLevel(DifficultyLevel.Hard);
        }

        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 3 * 2 + 90, 200, 60), "Back To Main Menu"))
        {
            ScreenChanger.SetScreen(ScreenState.MainMenuScreen);
        }
    }
}
