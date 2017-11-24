using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    public ScreenChanger ScreenChanger;
    public LevelGenerator LevelGenerator;
    public Texture2D Background;

    private AudioSource AudioSource;
    public AudioClip UnpauseGame;
    public AudioClip Exit;

    void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    void OnGUI()
    {
        if (ScreenChanger.GetScreen() != ScreenState.PauseScreen)
        {
            return;
        }

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Background);

        if (GUI.Button(new Rect(Screen.width / 2 - 215, Screen.height / 2 - 10, 200, 60), "Continue the Adventure"))
        {
            AudioSource.clip = UnpauseGame;
            AudioSource.Play();
            ScreenChanger.SetScreen(ScreenState.DungeonScreen);
        }

        if (GUI.Button(new Rect(Screen.width / 2 + 15, Screen.height / 2 - 10, 200, 60), "Back To Main Menu"))
        {
            AudioSource.clip = Exit;
            AudioSource.Play();
            LevelGenerator.DeleteLevel();
            ScreenChanger.SetScreen(ScreenState.MainMenuScreen);
        }
    }
}
