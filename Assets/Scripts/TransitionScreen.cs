using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScreen : MonoBehaviour
{
    public ScreenChanger ScreenChanger;
    public LevelGenerator LevelGenerator;
    public Texture2D Background;

    void Update()
    {
        if (ScreenChanger.GetScreen() == ScreenState.TransitionScreen && Input.GetKeyUp(KeyCode.Return))
        {
            LevelGenerator.GoToNextFloor();
            ScreenChanger.SetScreen(ScreenState.DungeonScreen);
        }
    }

    void OnGUI()
    {
        if (ScreenChanger.GetScreen() != ScreenState.TransitionScreen)
        {
            return;
        }

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Background);

        GUI.Label(new Rect(25, 100, 500, 25), "You Have Descended Further into the Depths.");
    }
}
