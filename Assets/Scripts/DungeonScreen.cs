using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonScreen : MonoBehaviour
{
    public ScreenChanger ScreenChanger;
    public LevelGenerator LevelGenerator;
    public Adventurer Adventurer;

    void Update()
    {
        if (ScreenChanger.GetScreen() != ScreenState.DungeonScreen)
        {
            return;
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            ScreenChanger.SetScreen(ScreenState.PauseScreen);
        }

        if (Input.GetKeyUp(KeyCode.Space) && Adventurer.IsOnStairs() && Adventurer.GetKeyFound() && LevelGenerator.GetFloorNumber() < 13)
        {
            ScreenChanger.SetScreen(ScreenState.TransitionScreen);
        }

        if (Adventurer.GetLight() == 0)
        {
            ScreenChanger.SetScreen(ScreenState.DeathScreen);
        }

        if (Input.GetKeyUp(KeyCode.Space) && Adventurer.IsOnStairs() && Adventurer.GetKeyFound() && LevelGenerator.GetFloorNumber() == 13)
        {
            ScreenChanger.SetScreen(ScreenState.VictoryScreen);
        }

        Adventurer.MoveAdventurer();
        Adventurer.HaveFoundOilCan();
        Adventurer.HaveFoundKey();
    }

    void OnGUI()
    {
        if (ScreenChanger.GetScreen() != ScreenState.DungeonScreen)
        {
            return;
        }

        GUI.Box(new Rect(Screen.width / 2 - 325, 25, 200, 25), "Dungeon Level: " + LevelGenerator.GetFloorNumber());
        GUI.Box(new Rect(Screen.width / 2 - 100, 25, 200, 25), "Light Remaining: " + Adventurer.GetLight());

        if (Adventurer.IsOnStairs() && Adventurer.GetKeyFound())
        {
            GUI.Box(new Rect(Screen.width / 2 + 125, 25, 200, 25), "Go to Next Floor");
        }
        else if (Adventurer.GetKeyFound())
        {
            GUI.Box(new Rect(Screen.width / 2 + 125, 25, 200, 25), "You Have the Key");
        }
        else
        {
            GUI.Box(new Rect(Screen.width / 2 + 125, 25, 200, 25), "Find the Key");
        }
    }
}
