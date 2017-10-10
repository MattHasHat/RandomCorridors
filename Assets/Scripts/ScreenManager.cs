using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScreenState { None, MainMenuScreen, DungeonScreen, PauseScreen, TransitionScreen }

public class ScreenManager : MonoBehaviour {

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

    private void Start()
    {
        next = ScreenState.None;
        current = ScreenState.MainMenuScreen;
    }

    private void Update()
    {
        if (next != ScreenState.None)
        {
            current = next;
            next = ScreenState.None;
        }
    }

}
