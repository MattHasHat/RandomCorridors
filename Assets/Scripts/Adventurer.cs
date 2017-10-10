using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adventurer : MonoBehaviour {

    public ScreenManager ScreenManager;
    private string adventurerName;
    private int totalExperience;
    private int maxHealth;
    private int currentHealth;

    void Start()
    {
        adventurerName = "The Adventurer";
        totalExperience = 0;
        maxHealth = 100;
        currentHealth = maxHealth;
    }

    private void OnGUI()
    {
        if (ScreenManager.GetScreen() != ScreenState.MainMenuScreen && ScreenManager.GetScreen() != ScreenState.DungeonScreen)
        {
            return;
        }
        GUI.depth = 0;
        GUI.Box(new Rect(Screen.width - 150, 25, 125, 200), "Name: " + adventurerName);
        GUI.Label(new Rect(Screen.width - 150, 75, 125, 25), "XP: " + totalExperience);
        GUI.Label(new Rect(Screen.width - 150, 100, 125, 25), "HP: [" + currentHealth + " / " + maxHealth + "]");
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
