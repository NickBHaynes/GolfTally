using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddNewOpponent : MonoBehaviour
{
    public Person AddOpponent(string opponentName, float opponentShots)
    {
        Person opponent = new Person();
        opponent.Name = opponentName;
        opponent.StartingShots = opponentShots;
        return opponent;
    }
}
