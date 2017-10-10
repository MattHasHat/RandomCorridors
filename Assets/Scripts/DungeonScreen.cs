using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonScreen : MonoBehaviour
{
    public ScreenManager ScreenManager;
    public DungeonSpawner DungeonSpawner;
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


        else if (Input.GetKeyUp(KeyCode.Space) && !ChangeFloor && DungeonSpawner.IsOnStairsDown())
        {
            ScreenManager.SetScreen(ScreenState.TransitionScreen);
        }

    }

    void HandleMovementInput()
    {
        bool moveSuccess = false;

        if (Input.GetKeyUp(KeyCode.W))
        {
            DungeonSpawner.MoveAdventurer(DungeonSpawner.Direction.North);
            moveSuccess = true;

        }

        else if (Input.GetKeyUp(KeyCode.A))
        {
            DungeonSpawner.MoveAdventurer(DungeonSpawner.Direction.West);
            moveSuccess = true;
        }

        else if (Input.GetKeyUp(KeyCode.S))
        {
            DungeonSpawner.MoveAdventurer(DungeonSpawner.Direction.South);
            moveSuccess = true;
        }

        else if (Input.GetKeyUp(KeyCode.D))
        {
            DungeonSpawner.MoveAdventurer(DungeonSpawner.Direction.East);
            moveSuccess = true;
        }

        if (moveSuccess && ChangeFloor && DungeonSpawner.IsOnStairsDown())
        {
            ScreenManager.SetScreen(ScreenState.TransitionScreen);
            return;
        }
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
        GUI.Label(new Rect(25, 95, 250, 25), "H = back to home base");
        GUI.Label(new Rect(25, 115, 250, 25), "I = open inventory");

        if (!ChangeFloor)
        {
            GUI.Label(new Rect(25, 135, 250, 25), "Spacebar on red = next floor");
        }
    }
}
