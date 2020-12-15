using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    public int OccupancyLimit = 1;

    private List<Guest> _occupants; //= new List<Guest>(); // list is private to protect from accidental methods

    //Awake happens before Start
    private void Awake()
    {
        //Debug.Log("201");
        _occupants = new List<Guest>();
        //Debug.Log("202");
    }
    

    public virtual void AddGuest(Guest guest)
    {
        //Debug.Log("203");
        _occupants.Add(guest);
        //Debug.Log("204");
    }
    

    public virtual void RemoveGuest(Guest guest)
    {
        //Debug.Log("205");
        _occupants.Remove(guest);
        //Debug.Log("206");
    }


    public virtual bool IsFull()
    {
        //Debug.Log("207");
        if (OccupancyLimit == 0) return false; //if there is no occupancy limit, it is never full
        if (_occupants.Count >= OccupancyLimit) { return true; } //if the number of guests equals occupants, it is full
        return false;
        //Debug.Log("208");
    }

    public virtual bool IsEmpty()
    {
        //Debug.Log("209");
        if (_occupants.Count == 0) { return true; }
        return false;
        //Debug.Log("210");
    }
}