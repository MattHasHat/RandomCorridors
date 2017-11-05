using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    public ScreenChanger ScreenChanger;
    public LevelGenerator LevelGenerator;
    public Texture2D Background;

    void OnGUI()
    {
        if (ScreenChanger.GetScreen() != ScreenState.DeathScreen)
        {
            return;
        }

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Background);

        GUI.Label(new Rect(Screen.width / 2 - 120, Screen.height / 2 + 70, 250, 40), "You Have Lost Yourself to the Darkness");

        if (GUI.Button(new Rect(Screen.width / 2 - 75, Screen.height / 2 + 100, 150, 40), "Back to Main Menu"))
        {
            LevelGenerator.DeleteDungeonFloor();
            ScreenChanger.SetScreen(ScreenState.MainMenu);
        }
    }
}
