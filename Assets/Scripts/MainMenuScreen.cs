using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScreen : MonoBehaviour
{
    public ScreenChanger ScreenChanger;
    public LevelGenerator LevelGenerator;
    public Texture2D Background;

    private AudioSource AudioSource;
    public AudioClip SelectLow;
    public AudioClip SelectMedium;
    public AudioClip SelectHigh;
    public AudioClip Exit;

    void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    void OnGUI()
    {
        if (ScreenChanger.GetScreen() != ScreenState.MainMenuScreen)
        {
            return;
        }

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Background);

        if (GUI.Button(new Rect(Screen.width / 2 - 330, Screen.height / 3 * 2, 200, 60), "Start Game"))
        {
            AudioSource.clip = SelectLow;
            AudioSource.Play();
            LevelGenerator.StartGame();
            ScreenChanger.SetScreen(ScreenState.DungeonScreen);
        }

        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 3 * 2, 200, 60), "Options"))
        {
            AudioSource.clip = SelectMedium;
            AudioSource.Play();
            ScreenChanger.SetScreen(ScreenState.OptionsScreen);
        }

        if (GUI.Button(new Rect(Screen.width / 2 + 130, Screen.height / 3 * 2, 200, 60), "Instructions"))
        {
            AudioSource.clip = SelectHigh;
            AudioSource.Play();
            ScreenChanger.SetScreen(ScreenState.InstructionsScreen);
        }

        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 3 * 2 + 90, 200, 60), "Quit Game"))
        {
            AudioSource.clip = Exit;
            AudioSource.Play();
            Application.Quit();
        }
    }
}
