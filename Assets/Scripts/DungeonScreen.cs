using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonScreen : MonoBehaviour
{
    private bool HasPlayed;

    public ScreenChanger ScreenChanger;
    public LevelGenerator LevelGenerator;
    public Adventurer Adventurer;
    public Specter Specter;

    private AudioSource AudioSource;
    public AudioClip PauseGame;
    public AudioClip OpenDoor;
    public AudioClip Died;
    public AudioClip Fanfare;
    public AudioClip Scream;

    public bool GetHasPlayed()
    {
        return HasPlayed;
    }

    public void SetHasPlayed(bool hasPlayed)
    {
        HasPlayed = hasPlayed;
    }

    void Update()
    {
        if (ScreenChanger.GetScreen() != ScreenState.DungeonScreen)
        {
            return;
        }

        if (Input.GetKey(KeyCode.RightAlt))
        {
            LevelGenerator.Camera.transform.position = new Vector3(22.5f, 50.0f, 22.5f);
        }

        if (Input.GetKeyUp(KeyCode.P))
        {
            AudioSource.clip = PauseGame;
            AudioSource.Play();
            ScreenChanger.SetScreen(ScreenState.PauseScreen);
        }

        if (Input.GetKeyUp(KeyCode.Space) && Adventurer.IsOnStairs() && Adventurer.GetKeyFound() && LevelGenerator.GetFloorNumber() < 13)
        {
            AudioSource.clip = OpenDoor;
            AudioSource.Play();
            ScreenChanger.SetScreen(ScreenState.TransitionScreen);
        }

        if (Adventurer.GetLight() == 0)
        {
            if (ScreenChanger.GetScreen() == ScreenState.DungeonScreen)
            {
                AudioSource.clip = Died;
                AudioSource.Play();
            }

            ScreenChanger.SetScreen(ScreenState.DeathScreen);
        }

        if (Input.GetKeyUp(KeyCode.Space) && Adventurer.IsOnStairs() && Adventurer.GetKeyFound() && LevelGenerator.GetFloorNumber() == 13)
        {
            if (ScreenChanger.GetScreen() == ScreenState.DungeonScreen)
            {
                AudioSource.clip = Fanfare;
                AudioSource.Play();
            }

            ScreenChanger.SetScreen(ScreenState.VictoryScreen);
        }

        Adventurer.MoveAdventurer();
        Adventurer.HaveFoundOilCan();
        Adventurer.HaveFoundKey();

        if (Specter.HaveFoundAdventurer() == true)
        {
            if (!GetHasPlayed())
            {
                AudioSource.clip = Scream;
                AudioSource.Play();
                SetHasPlayed(true);
            }
            LevelGenerator.SetAdventurerLocation(new GridLocation(LevelGenerator.StairsUpLocation.GetX(), LevelGenerator.StairsUpLocation.GetZ()), Quaternion.identity);
        }
    }

    void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    void OnGUI()
    {
        if (ScreenChanger.GetScreen() != ScreenState.DungeonScreen)
        {
            return;
        }

        GUI.Box(new Rect(Screen.width / 2 - 360, 25, 200, 25), "Dungeon Level: " + LevelGenerator.GetFloorNumber());
        GUI.Box(new Rect(Screen.width / 2 - 100, 25, 200, 25), "Light Remaining: " + Adventurer.GetLight());

        if (Adventurer.IsOnStairs() && Adventurer.GetKeyFound())
        {
            GUI.Box(new Rect(Screen.width / 2 + 160, 30, 200, 25), "Go To Next Floor");
        }
        else if (Adventurer.GetKeyFound())
        {
            GUI.Box(new Rect(Screen.width / 2 + 160, 30, 200, 25), "You Have The Key");
        }
        else
        {
            GUI.Box(new Rect(Screen.width / 2 + 160, 30, 200, 25), "Find The Key");
        }
    }
}
