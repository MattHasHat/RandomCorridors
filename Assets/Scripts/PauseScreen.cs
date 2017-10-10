using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Scripts/PauseScreen")]
public class PauseScreen : MonoBehaviour
{
    public ScreenManager ScreenManager;
    public Texture2D Background;

    void OnGUI()
    {
        if (ScreenManager.GetScreen() != ScreenState.PauseScreen)
        {
            return;
        }

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Background);

        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 200, 40), "Go on an Adventure"))
        {
            ScreenManager.SetScreen(ScreenState.DungeonScreen);
        }

        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2, 200, 40), "Back to Main Menu"))
        {
            ScreenManager.SetScreen(ScreenState.MainMenuScreen);
        }
    }
}
