using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    public ScreenChanger ScreenManager;
    public LevelGenerator LevelGenerator;
    public Texture2D Background;

    void OnGUI()
    {
        if (ScreenManager.GetScreen() != ScreenState.PauseScreen)
        {
            return;
        }

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Background);

        if (GUI.Button(new Rect(Screen.width / 2 - 75, Screen.height / 2 - 50, 150, 40), "Continue the Adventure"))
        {
            ScreenManager.SetScreen(ScreenState.DungeonView);
        }

        if (GUI.Button(new Rect(Screen.width / 2 - 75, Screen.height / 2, 150, 40), "Back to Main Menu"))
        {
            LevelGenerator.DeleteDungeonFloor();
            ScreenManager.SetScreen(ScreenState.MainMenu);
        }
    }
}
