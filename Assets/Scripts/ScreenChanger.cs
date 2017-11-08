using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScreenState { None, MainMenuScreen, OptionsScreen, InstructionsScreen, DungeonScreen, PauseScreen, TransitionScreen, DeathScreen, VictoryScreen }

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
        current = ScreenState.MainMenuScreen;
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
