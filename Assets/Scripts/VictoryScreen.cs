using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    public ScreenChanger ScreenChanger;
    public LevelGenerator LevelGenerator;
    public Texture2D Background;

    void OnGUI()
    {
        if (ScreenChanger.GetScreen() != ScreenState.VictoryScreen)
        {
            return;
        }

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Background);

        GUI.Label(new Rect(Screen.width / 2 - 175, Screen.height / 2 + 60, 350, 40), "You Have Braved the Dungeon and Plundered its Treasures;");
        GUI.Label(new Rect(Screen.width / 2 - 60, Screen.height / 2 + 75, 120, 40), "But at What Cost?");

        if (GUI.Button(new Rect(Screen.width / 2 - 75, Screen.height / 2 + 100, 150, 40), "Back to Main Menu"))
        {
            LevelGenerator.DeleteDungeonFloor();
            ScreenChanger.SetScreen(ScreenState.MainMenu);
        }
    }
}
