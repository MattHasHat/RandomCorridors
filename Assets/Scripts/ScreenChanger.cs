using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScreenState { None, MainMenu, DungeonView, PauseScreen, Transition, DeathScreen, VictoryScreen }

public class ScreenChanger : MonoBehaviour
{
    private ScreenState current;
    private ScreenState next;

    public ScreenState GetScreen()
    {
        return current;
    }

    public void SetScreen(ScreenState screenState)
    {
        next = screenState;
    }

    void Start()
    {
        next = ScreenState.None;
        current = ScreenState.MainMenu;
    }

    void Update()
    {
        if (next != ScreenState.None)
        {
            current = next;
            next = ScreenState.None;
        }
    }
}
