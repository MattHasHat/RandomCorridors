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

        GUI.Label(new Rect(Screen.width / 2 - 360, Screen.height / 3 * 2 - 90, 350, 30), "- Use 'W', 'A', 'S', And 'D' To Move");
        GUI.Label(new Rect(Screen.width / 2 - 360, Screen.height / 3 * 2 - 45, 350, 30), "- Press 'P' To Pause The Game");
        GUI.Label(new Rect(Screen.width / 2 - 360, Screen.height / 3 * 2, 350, 30), "- Press 'Spacebar' To Go To The Next Floor");
        GUI.Label(new Rect(Screen.width / 2 - 360, Screen.height / 3 * 2 + 45, 350, 30), "- Press 'Enter' Between Floors To Continue");

        GUI.Label(new Rect(Screen.width / 2 + 60, Screen.height / 3 * 2 - 90, 400, 30), "- Find The Key To Go To The Next Floor");
        GUI.Label(new Rect(Screen.width / 2 + 60, Screen.height / 3 * 2 - 45, 400, 30), "- Use The Key On The Door To Advance Through The Dungeon");
        GUI.Label(new Rect(Screen.width / 2 + 60, Screen.height / 3 * 2, 400, 30), "- Collect Oil Cans To Extend Your Torch's Life");
        GUI.Label(new Rect(Screen.width / 2 + 60, Screen.height / 3 * 2 + 45, 400, 30), "- Avoid The Roaming Spectres Or You Will Flee To Level's Start");

        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 3 * 2 + 90, 200, 60), "Back To Main Menu"))
        {
            ScreenChanger.SetScreen(ScreenState.MainMenuScreen);
        }
    }
}
