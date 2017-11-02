using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public ScreenChanger ScreenChanger;
    public LevelGenerator LevelGenerator;
    public Texture2D Background;

    void OnGUI()
    {
        if (ScreenChanger.GetScreen() != ScreenState.MainMenu)
        {
            return;
        }

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Background);

        if (GUI.Button(new Rect(Screen.width / 2 - 75, Screen.height / 2 + 70, 150, 40), "Start"))
        {
            LevelGenerator.StartGame();
            ScreenChanger.SetScreen(ScreenState.DungeonView);
        }

        if (GUI.Button(new Rect(Screen.width / 2 - 75, Screen.height / 2 + 120, 150, 40), "Quit"))
        {
            Application.Quit();
        }
    }
}
