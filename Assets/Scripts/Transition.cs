using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoBehaviour
{
    public ScreenChanger ScreenChanger;
    public LevelGenerator LevelGenerator;
    public Texture2D Background;

    void Update()
    {
        if (ScreenChanger.GetScreen() != ScreenState.Transition)
        {
            return;
        }

        if (Input.GetKeyUp(KeyCode.Return))
        {
            LevelGenerator.GoToNextFloor();
            ScreenChanger.SetScreen(ScreenState.DungeonView);
        }
    }

    void OnGUI()
    {
        if (ScreenChanger.GetScreen() != ScreenState.Transition)
        {
            return;
        }

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Background);

        GUI.Label(new Rect(25, 100, 500, 25), "You Have Descended Further into the Depths.");
        GUI.Label(new Rect(25, 130, 200, 25), "Press Enter to Continue.");
    }
}
