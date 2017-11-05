using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonView : MonoBehaviour
{
    public ScreenChanger ScreenChanger;
    public LevelGenerator LevelGenerator;
    public Adventurer Adventurer;

    void Update()
    {
        if (ScreenChanger.GetScreen() != ScreenState.DungeonView)
        {
            return;
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            ScreenChanger.SetScreen(ScreenState.PauseScreen);
        }
        else if (Input.GetKeyUp(KeyCode.Space) && Adventurer.IsOnStairs() && Adventurer.GetKeyFound() && LevelGenerator.GetFloorNumber() < 5)
        {
            ScreenChanger.SetScreen(ScreenState.Transition);
        }
        else if (Adventurer.GetStamina() == 0)
        {
            ScreenChanger.SetScreen(ScreenState.DeathScreen);
        }
        else if (Input.GetKeyUp(KeyCode.Space) && Adventurer.IsOnStairs() && Adventurer.GetKeyFound() && LevelGenerator.GetFloorNumber() == 5)
        {
            ScreenChanger.SetScreen(ScreenState.VictoryScreen);
        }

        Adventurer.HaveFoundKey();
        Adventurer.MoveAdventurer();
    }

    void OnGUI()
    {
        if (ScreenChanger.GetScreen() != ScreenState.DungeonView)
        {
            return;
        }

        GUI.Box(new Rect(25, 25, 200, 25), "Dungeon Level: " + LevelGenerator.GetFloorNumber());
        GUI.Box(new Rect(25, 55, 200, 25), "Stamina Remaining: " + Adventurer.GetStamina());
        GUI.Label(new Rect(25, 85, 250, 25), "WASD for Movement");
        GUI.Label(new Rect(25, 115, 250, 25), "P to Pause");

        if (Adventurer.IsOnStairs())
        {
            GUI.Label(new Rect(25, 145, 250, 25), "Spacebar on Panel and Go to Next Floor");
        }
    }
}
