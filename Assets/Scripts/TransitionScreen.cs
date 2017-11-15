using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionScreen : MonoBehaviour
{
    public ScreenChanger ScreenChanger;
    public LevelGenerator LevelGenerator;
    public Texture2D Background;

    void Update()
    {
        if (ScreenChanger.GetScreen() == ScreenState.TransitionScreen && Input.GetKeyUp(KeyCode.Return))
        {
            LevelGenerator.GoToNextFloor();
            ScreenChanger.SetScreen(ScreenState.DungeonScreen);
        }
    }

    void OnGUI()
    {
        if (ScreenChanger.GetScreen() != ScreenState.TransitionScreen)
        {
            return;
        }

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Background);

        if (LevelGenerator.GetFloorNumber() == 1)
        {
            GUI.Label(new Rect(Screen.width / 2 - 600, Screen.height / 3 - 50, 900, 30),
                "I Have Descended Further Into The Depths Of The Dungeon And Will Continue Onward.");
        }

        if (LevelGenerator.GetFloorNumber() == 2)
        {
            GUI.Label(new Rect(Screen.width / 2 - 600, Screen.height / 3 - 50, 900, 30),
                "These Floors Are Quite Labyrinthian. Who? Or Should I Say What Could Have Built This Maze?");
        }

        if (LevelGenerator.GetFloorNumber() == 3)
        {
            GUI.Label(new Rect(Screen.width / 2 - 600, Screen.height / 3 - 50, 900, 30),
                "The Specters Seem Aimless In Their Wandering. While They Do Not Seek Me Out, I Shall Not Dare Come Face To Face With One.");
        }

        if (LevelGenerator.GetFloorNumber() == 4)
        {
            GUI.Label(new Rect(Screen.width / 2 - 600, Screen.height / 3 - 50, 900, 30),
                "As I Proceed Further Down, I Have Come To Realize That My Torch Is My Lifeline. Now I Fear For When It Might Go Out.");
        }

        if (LevelGenerator.GetFloorNumber() == 5)
        {
            GUI.Label(new Rect(Screen.width / 2 - 600, Screen.height / 3 - 50, 900, 30),
                "I Feel As If I Have Been Here Before In My Dreams. Or Perhaps It Was A Nightmare?");
        }

        if (LevelGenerator.GetFloorNumber() == 6)
        {
            GUI.Label(new Rect(Screen.width / 2 - 600, Screen.height / 3 - 50, 900, 30),
                "Though I Should Know Better To Turn Around, It Feels As If Something Is Beckoning Me On, Something... Primordial.");
        }

        if (LevelGenerator.GetFloorNumber() == 7)
        {
            GUI.Label(new Rect(Screen.width / 2 - 600, Screen.height / 3 - 50, 900, 30),
                "The Walls Of This Place Seem Cyclopean Yet Otherworldly In their Design. I Wonder, How Old Could These Walls Be?");
        }
        if (LevelGenerator.GetFloorNumber() == 8)
        {
            GUI.Label(new Rect(Screen.width / 2 - 600, Screen.height / 3 - 50, 900, 30),
                "As I Journey, I Find Myself Mimicking The Specters. Their Movements. Their Glazed-over Stare. It's As If I Am Metamorphosizing Into One.");
        }

        if (LevelGenerator.GetFloorNumber() == 9)
        {
            GUI.Label(new Rect(Screen.width / 2 - 600, Screen.height / 3 - 50, 900, 30),
                "I Hear Whispers Now, Though No One Is There To Whisper Them. Dread Has Become Me.");
        }

        if (LevelGenerator.GetFloorNumber() == 10)
        {
            GUI.Label(new Rect(Screen.width / 2 - 600, Screen.height / 3 - 50, 900, 30),
                "I Fear I Have Made A Terrible Mistake In Coming Here.");
        }

        if (LevelGenerator.GetFloorNumber() == 11)
        {
            GUI.Label(new Rect(Screen.width / 2 - 600, Screen.height / 3 - 50, 900, 30),
                "I Can Feel My Mind Slipping...");
        }

        if (LevelGenerator.GetFloorNumber() == 12)
        {
            GUI.Label(new Rect(Screen.width / 2 - 600, Screen.height / 3 - 50, 900, 30),
                "...");
        }
    }
}
