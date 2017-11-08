using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsScreen : MonoBehaviour
{
    public ScreenChanger ScreenChanger;
    public LevelGenerator LevelGenerator;
    public Texture2D Background;

    void OnGUI()
    {
        if (ScreenChanger.GetScreen() != ScreenState.InstructionsScreen)
        {
            return;
        }

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Background);

        GUI.Label(new Rect(Screen.width / 2 - 350, Screen.height / 2 + 10, 350, 100),
            " Use W, A, S, and D to Move" +
            "\r\n Press P to Pause the Game" +
            "\r\n Press Spacebar to Go to the Next Floor" +
            "\r\n Press Enter Between Floors to Continue");

        GUI.Label(new Rect(Screen.width / 2, Screen.height / 2 + 10, 350, 100),
            " Find the Key to Go to the Next Floor" +
            "\r\n Use the Key on the Door to Advance through the Dungeon" +
            "\r\n Collect Oil Cans to Extend Your Torch's Light" +
            "\r\n Avoid the Roaming Spectres or Face Certain Doom");

        if (GUI.Button(new Rect(Screen.width / 2 - 75, Screen.height / 2 + 110, 150, 40), "Back to Main Menu"))
        {
            ScreenChanger.SetScreen(ScreenState.MainMenuScreen);
        }
    }
}

