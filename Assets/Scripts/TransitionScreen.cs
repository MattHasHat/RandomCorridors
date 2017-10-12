﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScreen : MonoBehaviour
{
    public ScreenManager ScreenManager;
    public DungeonSpawner DungeonSpawner;
    public Texture2D Background;

    void Update()
    {
        if (ScreenManager.GetScreen() != ScreenState.TransitionScreen)
        {
            return;
        }

        if (Input.GetKeyUp(KeyCode.Return))
        {
            DungeonSpawner.GoToNextFloor();
            ScreenManager.SetScreen(ScreenState.DungeonScreen);
        }
    }

    void OnGUI()
    {
        if (ScreenManager.GetScreen() != ScreenState.TransitionScreen)
        {
            return;
        }

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Background);

        GUI.Box(new Rect(25, 100, 700, 25), "You Have Descended further into the depths.");
        GUI.Box(new Rect(25, 150, 200, 25), "Press enter to continue.");
    }
}
