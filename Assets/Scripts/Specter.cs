using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Specter : MonoBehaviour
{
    public LevelGenerator LevelGenerator;

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

    public void MoveSpecter()
    {
        ArrayList possibleMoves = new ArrayList();
        possibleMoves = CheckDirections(possibleMoves);

        string move = (string)possibleMoves[Random.Range(0, possibleMoves.Count)];

        if (move == "North")
        {
            LevelGenerator.SetSpecterLocation(new GridLocation(LevelGenerator.SpecterLocation.GetX(), LevelGenerator.SpecterLocation.GetZ() + 1), Quaternion.identity);
        }

        if (move == "East")
        {
            LevelGenerator.SetSpecterLocation(new GridLocation(LevelGenerator.SpecterLocation.GetX() + 1, LevelGenerator.SpecterLocation.GetZ()), Quaternion.identity);
        }

        if (move == "South")
        {
            LevelGenerator.SetSpecterLocation(new GridLocation(LevelGenerator.SpecterLocation.GetX(), LevelGenerator.SpecterLocation.GetZ() - 1), Quaternion.identity);
        }

        if (move == "West")
        {
            LevelGenerator.SetSpecterLocation(new GridLocation(LevelGenerator.SpecterLocation.GetX() - 1, LevelGenerator.SpecterLocation.GetZ()), Quaternion.identity);
        }
    }

    public void Wander()
    {
        InvokeRepeating("MoveSpecter", 1.5f, 1.5f);
    }

    public bool HaveFoundAdventurer()
    {
        return LevelGenerator.SpecterLocation.GetX() == LevelGenerator.AdventurerLocation.GetX() && LevelGenerator.SpecterLocation.GetZ() == LevelGenerator.AdventurerLocation.GetZ();
    }
}
