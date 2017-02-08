using UnityEngine;
using System.Collections;
using System;

public class Player : IComparable<Player>
{
    public string name;
    public int point;

    public Player(string newName,int newPoint)
    {
        name = newName;
        point = newPoint;
    }
    public int CompareTo(Player other)
    {
        if (other == null)
            return 1;

        return other.point-point;
    }
}
