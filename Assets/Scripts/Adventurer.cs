using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adventurer : MonoBehaviour
{
    public enum Direction { North, East, South, West }
    public ScreenManager ScreenManager;
    public DungeonSpawner DungeonSpawner;
    private string adventurerName;
    private int totalExperience;
    private int maxHealth;
    private int currentHealth;

    private bool ChangeFloor;

    void Start()
    {
        adventurerName = "Adventurer";
        totalExperience = 0;
        maxHealth = 100;
        currentHealth = maxHealth;
    }

    void OnGUI()
    {
        if (ScreenManager.GetScreen() != ScreenState.DungeonScreen && ScreenManager.GetScreen() != ScreenState.InventoryScreen)
        {
            return;
        }

        GUI.depth = 0;
        GUI.Box(new Rect(Screen.width - 150, 25, 125, 200), "Name: " + adventurerName);
        GUI.Label(new Rect(Screen.width - 150, 75, 125, 25), "XP: " + totalExperience);
        GUI.Label(new Rect(Screen.width - 150, 100, 125, 25), "HP: [" + currentHealth + " / " + maxHealth + "]");
    }

    public void HandleMovementInput()
    {
        bool moveSuccess = false;

        if (Input.GetKeyUp(KeyCode.W))
        {
            MoveAdventurer(Direction.North);
            moveSuccess = true;

        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            MoveAdventurer(Direction.West);
            moveSuccess = true;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            MoveAdventurer(Direction.South);
            moveSuccess = true;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            MoveAdventurer(Direction.East);
            moveSuccess = true;
        }

        if (moveSuccess && ChangeFloor && IsOnStairs())
        {
            ScreenManager.SetScreen(ScreenState.TransitionScreen);
            return;
        }
    }

    public bool MoveAdventurer(Direction direction)
    {
        if (direction == Direction.North && DungeonSpawner.GridLocationValidNorth(DungeonSpawner.AdventurerLocation))
        {
            DungeonSpawner.SetAdventurerLocation(new GridLocation(DungeonSpawner.AdventurerLocation.x, DungeonSpawner.AdventurerLocation.z + 1));
            return true;
        }
        else if (direction == Direction.West && DungeonSpawner.GridLocationValidWest(DungeonSpawner.AdventurerLocation))
        {
            DungeonSpawner.SetAdventurerLocation(new GridLocation(DungeonSpawner.AdventurerLocation.x - 1, DungeonSpawner.AdventurerLocation.z));
            return true;
        }
        else if (direction == Direction.South && DungeonSpawner.GridLocationValidSouth(DungeonSpawner.AdventurerLocation))
        {
            DungeonSpawner.SetAdventurerLocation(new GridLocation(DungeonSpawner.AdventurerLocation.x, DungeonSpawner.AdventurerLocation.z - 1));
            return true;
        }
        else if (direction == Direction.East && DungeonSpawner.GridLocationValidEast(DungeonSpawner.AdventurerLocation))
        {
            DungeonSpawner.SetAdventurerLocation(new GridLocation(DungeonSpawner.AdventurerLocation.x + 1, DungeonSpawner.AdventurerLocation.z));
            return true;
        }
        return false;
    }

    public bool IsOnStairs()
    {
        return DungeonSpawner.AdventurerLocation.x == DungeonSpawner.StairsDownLocation.x && DungeonSpawner.AdventurerLocation.z == DungeonSpawner.StairsDownLocation.z;
    }

    public void GainExperience(int experience)
    {
        totalExperience += experience;
    }

    public void AccrueDamage(int damage)
    {
        currentHealth -= damage;
    }

    public void HealDamage()
    {
        if (currentHealth < 80)
        {
            currentHealth += 20;
        }
        else
        {
            currentHealth = maxHealth;
        }
    }
}
