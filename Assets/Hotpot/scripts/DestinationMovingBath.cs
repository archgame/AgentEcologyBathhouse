using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationMovingBath : Destination
{

    private List<Guest> _occupants; //= new List<Guest>(); // list is private to protect from accidental methods

    //Awake happens before Start
    private void Awake()
    {
        _occupants = new List<Guest>();
    }

    public override void AddGuest(Guest guest)
    {
        _occupants.Add(guest);
    }

    public override void RemoveGuest(Guest guest)
    {
        _occupants.Remove(guest);
    }

    public override bool IsFull()
    {
        if (OccupancyLimit == 0) return false; //if there is no occupancy limit, it is never full
        if (_occupants.Count >= OccupancyLimit) { return true; } //if the number of guests equals occupants, it is full
        return false;
    }

    public override bool IsEmpty()
    {
        if (_occupants.Count == 0) { return true; }
        return false;
    }
}