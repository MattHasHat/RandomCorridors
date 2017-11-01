using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScreen : MonoBehaviour
{
    public ScreenManager ScreenManager;
    public DungeonSpawner DungeonSpawner;
    public Texture2D Background;

    void OnGUI()
    {
        if (ScreenManager.GetScreen() != ScreenState.MainMenuScreen)
        {
            return;
        }

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Background);

        if (GUI.Button(new Rect(Screen.width / 2 - 75, Screen.height / 2 + 70, 150, 40), "Start"))
        {
            DungeonSpawner.StartGame();
            ScreenManager.SetScreen(ScreenState.DungeonScreen);
        }

        if (GUI.Button(new Rect(Screen.width / 2 - 75, Screen.height / 2 + 120, 150, 40), "Quit"))
        {
            Application.Quit();
        }
    }
}
