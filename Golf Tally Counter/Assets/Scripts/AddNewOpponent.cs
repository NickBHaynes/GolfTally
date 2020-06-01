using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddNewOpponent
{
    public Person AddOpponent(string opponentName, string opponentShots)
    {
        Person opponent = new Person();
        opponent.Name = opponentName;
        opponent.StartingShots = float.Parse(opponentShots);
        opponent.pastRounds = new List<float>();
        opponent.pastRounds.Add(float.Parse(opponentShots));
        return opponent;
    }
}
