using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    public ScreenChanger ScreenChanger;
    public LevelGenerator LevelGenerator;
    public Texture2D Background;

    private AudioSource AudioSource;
    public AudioClip Exit;

    void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    void OnGUI()
    {
        if (ScreenChanger.GetScreen() != ScreenState.DeathScreen)
        {
            return;
        }

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Background);

        GUI.Label(new Rect(Screen.width / 2 - 170, Screen.height / 3 * 2 + 45, 340, 30), " You Have Lost Yourself In The Darkness, Never To Leave");

        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 3 * 2 + 90, 200, 60), "Back To Main Menu"))
        {
            AudioSource.clip = Exit;
            AudioSource.Play();
            LevelGenerator.DeleteLevel();
            ScreenChanger.SetScreen(ScreenState.MainMenuScreen);
        }
    }
}
