using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    public ScreenChanger ScreenChanger;
    public LevelGenerator LevelGenerator;
    public Texture2D Background;

    void OnGUI()
    {
        if (ScreenChanger.GetScreen() != ScreenState.PauseScreen)
        {
            return;
        }

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Background);

        if (GUI.Button(new Rect(Screen.width / 2 - 165, Screen.height / 2 - 10, 150, 40), "Continue the Adventure"))
        {
            ScreenChanger.SetScreen(ScreenState.DungeonScreen);
        }

        if (GUI.Button(new Rect(Screen.width / 2 + 15, Screen.height / 2 - 10, 150, 40), "Back to Main Menu"))
        {
            LevelGenerator.DeleteLevel();
            ScreenChanger.SetScreen(ScreenState.MainMenuScreen);
        }
    }
}
