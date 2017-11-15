﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScreen : MonoBehaviour
{
    public ScreenChanger ScreenChanger;
    public LevelGenerator LevelGenerator;
    public Texture2D Background;

    void OnGUI()
    {
        if (ScreenChanger.GetScreen() != ScreenState.MainMenuScreen)
        {
            return;
        }

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Background);

        if (GUI.Button(new Rect(Screen.width / 2 - 330, Screen.height / 3 * 2, 200, 60), "Start Game"))
        {
            LevelGenerator.StartGame();
            ScreenChanger.SetScreen(ScreenState.DungeonScreen);
        }

        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 3 * 2, 200, 60), "Options"))
        {
            ScreenChanger.SetScreen(ScreenState.OptionsScreen);
        }

        if (GUI.Button(new Rect(Screen.width / 2 + 130, Screen.height / 3 * 2, 200, 60), "Instructions"))
        {
            ScreenChanger.SetScreen(ScreenState.InstructionsScreen);
        }

        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 3 * 2 + 90, 200, 60), "Quit Game"))
        {
            Application.Quit();
        }
    }
}
