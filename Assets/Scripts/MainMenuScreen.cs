using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScreen : MonoBehaviour
{
    public ScreenManager ScreenManager;
    public Texture2D Background;

    void OnGUI()
    {
        if (ScreenManager.GetScreen() != ScreenState.MainMenuScreen)
        {
            return;
        }

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Background);

        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 50, 100, 40), "Start"))
        {
            ScreenManager.SetScreen(ScreenState.PauseScreen);
        }

        if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2, 100, 40), "Quit"))
        {
            Application.Quit();
        }
    }
}