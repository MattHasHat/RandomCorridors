using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScreenState { None, MainMenuScreen, DungeonScreen, PauseScreen, }

public class ScreenManager : MonoBehaviour
{
    private ScreenState current;
    private ScreenState next;

    public ScreenState GetScreen()
    {
        return current;
    }

    public void SetScreen(ScreenState newScreenState)
    {
        next = newScreenState;
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
