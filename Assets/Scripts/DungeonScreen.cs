using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonScreen : MonoBehaviour
{
    public ScreenManager ScreenManager;
    public DungeonSpawner DungeonSpawner;
    public Adventurer Adventurer;

    void Update()
    {
        if (Adventurer.KeyFound == true)
        {
            for (int i = 0; i < DungeonSpawner.KeyParent.childCount; i++)
            {
                Destroy(DungeonSpawner.KeyParent.GetChild(i).gameObject);
            }
        }

        if (ScreenManager.GetScreen() != ScreenState.DungeonScreen)
        {
            return;
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            ScreenManager.SetScreen(ScreenState.PauseScreen);
        }
        else if (Input.GetKeyUp(KeyCode.Space) && Adventurer.IsOnStairs() && Adventurer.HaveFoundKey())
        {
            DungeonSpawner.GoToNextFloor();
        }

        Adventurer.HaveFoundKey();
        Adventurer.HandleMovementInput();
    }

    void OnGUI()
    {
        if (ScreenManager.GetScreen() != ScreenState.DungeonScreen)
        {
            return;
        }

        GUI.Box(new Rect(25, 25, 200, 25), "Dungeon Floor: " + DungeonSpawner.DungeonFloorNumber);
        GUI.Box(new Rect(25, 55, 200, 25), "Stamina Remaining: " + Adventurer.Stamina);
        GUI.Label(new Rect(25, 85, 250, 25), "WASD for Movement");
        GUI.Label(new Rect(25, 115, 250, 25), "P to Pause");

        if (Adventurer.IsOnStairs())
        {
            GUI.Label(new Rect(25, 145, 250, 25), "Spacebar on Panel and Go to Next Floor");
        }
    }
}
