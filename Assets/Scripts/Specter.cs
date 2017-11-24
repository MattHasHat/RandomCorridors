using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Specter : MonoBehaviour
{
    private float SpecterSpeed;

    public LevelGenerator LevelGenerator;
    public DungeonScreen DungeonScreen;

    public float GetSpecterSpeed()
    {
        return SpecterSpeed;
    }

    public void SetSpecterSpeed(float specterSpeed)
    {
        SpecterSpeed = specterSpeed;
    }

    ArrayList FollowAdventurer(ArrayList possibleMoves)
    {
        if (LevelGenerator.IsNorthValid(LevelGenerator.SpecterLocation) && LevelGenerator.SpecterLocation.GetZ() < LevelGenerator.AdventurerLocation.GetZ())
        {
            possibleMoves.Add("North");
        }

        if (LevelGenerator.IsEastValid(LevelGenerator.SpecterLocation) && LevelGenerator.SpecterLocation.GetX() < LevelGenerator.AdventurerLocation.GetX())
        {
            possibleMoves.Add("East");
        }

        if (LevelGenerator.IsSouthValid(LevelGenerator.SpecterLocation) && LevelGenerator.SpecterLocation.GetZ() > LevelGenerator.AdventurerLocation.GetZ())
        {
            possibleMoves.Add("South");
        }

        if (LevelGenerator.IsWestValid(LevelGenerator.SpecterLocation) && LevelGenerator.SpecterLocation.GetX() > LevelGenerator.AdventurerLocation.GetX())
        {
            possibleMoves.Add("West");
        }

        return possibleMoves;
    }

    ArrayList CheckDirections(ArrayList possibleMoves)
    {
        if (LevelGenerator.IsNorthValid(LevelGenerator.SpecterLocation))
        {
            possibleMoves.Add("North");
        }

        if (LevelGenerator.IsEastValid(LevelGenerator.SpecterLocation))
        {
            possibleMoves.Add("East");
        }

        if (LevelGenerator.IsSouthValid(LevelGenerator.SpecterLocation))
        {
            possibleMoves.Add("South");
        }

        if (LevelGenerator.IsWestValid(LevelGenerator.SpecterLocation))
        {
            possibleMoves.Add("West");
        }

        return possibleMoves;
    }

    void MoveSpecter()
    {
        ArrayList possibleMoves = new ArrayList();
        possibleMoves = FollowAdventurer(possibleMoves);

        if (possibleMoves.Count == 0)
        {
            possibleMoves = CheckDirections(possibleMoves);
        }

        string move = (string)possibleMoves[Random.Range(0, possibleMoves.Count)];

        if (move == "North")
        {
            LevelGenerator.SetSpecterLocation(new GridLocation(LevelGenerator.SpecterLocation.GetX(), LevelGenerator.SpecterLocation.GetZ() + 1));
        }

        if (move == "East")
        {
            LevelGenerator.SetSpecterLocation(new GridLocation(LevelGenerator.SpecterLocation.GetX() + 1, LevelGenerator.SpecterLocation.GetZ()));
        }

        if (move == "South")
        {
            LevelGenerator.SetSpecterLocation(new GridLocation(LevelGenerator.SpecterLocation.GetX(), LevelGenerator.SpecterLocation.GetZ() - 1));
        }

        if (move == "West")
        {
            LevelGenerator.SetSpecterLocation(new GridLocation(LevelGenerator.SpecterLocation.GetX() - 1, LevelGenerator.SpecterLocation.GetZ()));
        }

        DungeonScreen.SetHasPlayed(false);
    }

    public void StartWandering()
    {
        InvokeRepeating("MoveSpecter", GetSpecterSpeed(), GetSpecterSpeed());
    }

    public void StopWandering()
    {
        CancelInvoke("MoveSpecter");
    }

    public bool HaveFoundAdventurer()
    {
        return LevelGenerator.SpecterLocation.GetX() == LevelGenerator.AdventurerLocation.GetX() && LevelGenerator.SpecterLocation.GetZ() == LevelGenerator.AdventurerLocation.GetZ();
    }
}
