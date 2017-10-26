using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonScreen : MonoBehaviour
{
    public ScreenManager ScreenManager;
    public DungeonSpawner DungeonSpawner;
    public Adventurer Adventurer;
    public bool ChangeFloor = false;

    void Update()
    {
        if (ScreenManager.GetScreen() != ScreenState.DungeonScreen)
        {
            return;
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            ScreenManager.SetScreen(ScreenState.PauseScreen);
        }
        else if (Input.GetKeyUp(KeyCode.I))
        {
            ScreenManager.SetScreen(ScreenState.InventoryScreen);
        }
        else if (Input.GetKeyUp(KeyCode.Space) && !ChangeFloor && Adventurer.IsOnStairs())
        {
            ScreenManager.SetScreen(ScreenState.TransitionScreen);
        }

        Adventurer.HandleMovementInput();
    }

    void OnGUI()
    {
        if (ScreenManager.GetScreen() != ScreenState.DungeonScreen)
        {
            return;
        }

        GUI.Box(new Rect(25, 25, 150, 25), "Dungeon Floor: " + DungeonSpawner.DungeonFloorNumber);
        GUI.Label(new Rect(25, 55, 250, 25), "WASD = move");
        GUI.Label(new Rect(25, 75, 250, 25), "Z = zoom in/out");
        GUI.Label(new Rect(25, 95, 250, 25), "P = back to pause");
        GUI.Label(new Rect(25, 115, 250, 25), "I = open inventory");

        if (!ChangeFloor)
        {
            GUI.Label(new Rect(25, 135, 250, 25), "Spacebar on red = next floor");
        }
    }
}
