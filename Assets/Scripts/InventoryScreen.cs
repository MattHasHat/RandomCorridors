using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Scripts/InventoryScreen")]
public class InventoryScreen : MonoBehaviour
{
    public ScreenManager ScreenManager;
    public Texture2D Background;
    public Adventurer Adventurer;

    void Update()
    {
        if (ScreenManager.GetScreen() != ScreenState.InventoryScreen)
        {
            return;
        }
    }

    void OnGUI()
    {
        if (ScreenManager.GetScreen() != ScreenState.InventoryScreen)
        {
            return;
        }

        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Background);

        if (GUI.Button(new Rect(200, 200, 300, 25), "Click to Use Healing Potion"))
        {
            Adventurer.HealDamage();
        }

        if (GUI.Button(new Rect(200, 230, 300, 25), "Close Inventory"))
        {
            ScreenManager.SetScreen(ScreenState.DungeonScreen);
        }
    }
}
