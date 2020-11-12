using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Funicular : Conveyance
{
    public GameObject Car;
    public GameObject Positions;
    public GameObject[] Stops;
    


    public enum State { MOVING, WAITING };

    public State CurrentState = State.WAITING;


    private Dictionary<Guest, Vector3> _guests = new Dictionary<Guest, Vector3>(); //all guests
    private Dictionary<GameObject, Guest> _positions = new Dictionary<GameObject, Guest>();
    public Destination[] _destinations;
    private Dictionary<Guest, GameObject> _riders = new Dictionary<Guest, GameObject>();
    

    private float _maxWait = 3.0f;
    public float _waitTime = 0.0f;


    public override void SetDestination()
    {
        _waitTime = _maxWait;

        _destinations = GetComponentsInChildren<Destination>();
        

        //create the positions dictionary
        for (int i = 0; i < Positions.transform.childCount; i++)
        {
            _positions.Add(Positions.transform.GetChild(i).gameObject, null);
        }

        //set the occupnacy limit for each waiting lobby based on the number of positions in the elevator
        foreach (Destination destination in _destinations)
        {
            destination.OccupancyLimit = _positions.Count;
        }
    }


    public void Update()
    {
        foreach (KeyValuePair<Guest, GameObject> kvp in _riders)
        {
            kvp.Key.transform.parent = Car.transform;
            //_riders[kvp.Key] = kvp.Value;
            //kvp.Value = GameObject
        }

        
        Vector3 CarPosition = Car.transform.position;
        Vector3 NextPosition = Stops[0].transform.position;
                
        if (Vector3.Distance(CarPosition, NextPosition) < 0.01f)
        {
            
            CurrentState = State.WAITING;
            
            if (_waitTime <= 0)
            {
                CurrentState = State.MOVING;
            }
            else
            {
                _waitTime -= Time.deltaTime;
                return;
            }
            Array.Reverse(Stops);
            
            _waitTime = _maxWait;
        }
        
        Car.transform.position = Vector3.MoveTowards(CarPosition, NextPosition, Time.deltaTime * Speed);
        CurrentState = State.MOVING;
        
    }


    public override void ConveyanceUpdate(Guest guest)
    {
                //add guest to dictionary and their desired destination
        if (!_guests.ContainsKey(guest))
        {
            
            Destination destination = GetDestination(guest.transform.position); //converting into elevator stop floor
            _guests.Add(guest, destination.transform.position);

            
        }

        //guard statement if the elevator is moving
        if (CurrentState == State.MOVING) { return; }

        
        //call if the car if it isn't on the guest level
        if (Mathf.Abs(_destinations[1].transform.position.y-guest.transform.position.y) > 2.9f) 
        //If car is not in the destination floor, Note:At least more than 0.2f than the 'potitions' Y
        {
            if (Mathf.Abs(Car.transform.position.y - guest.transform.position.y) < 7f)// and if car is next to guest, then load
            {
                if (!LoadingGuest(guest))
                {
                    //if the guest isn't done loading
                }
            }
        }
       
        else //else unload
        {
            
            if (!UnloadingGuest(guest))
            {
                 //if the guest isn't done loading
            }
        }

       
        
    }

        //once we reach this point, we can assume the guest is either loading or unloading
        //we are assuming the guests that are unloading are children of the Car GameObject
        

    public bool UnloadingGuest(Guest guest)
    {
        //at this point we assume the guest is unloading

        //switch out the point when begin the unloading process
        if (guest.transform.position == _riders[guest].transform.position)
        {
            Destination destination = GetDestination(Car.transform.position);
            Vector3 offset = destination.transform.position - Car.transform.position;
            _guests[guest] = guest.transform.position + offset;
        }

        //unload the guest (animate the guest exiting
        guest.transform.position = Vector3.MoveTowards(guest.transform.position,
            _guests[guest],
            Time.deltaTime * Speed);

        //if the guest hasn't reached the disembark position, return false
        if (Vector3.Distance(guest.transform.position, _guests[guest]) > 0.01f) return false;

        //assume the guest has made it to the disembark position
        GameObject position = _riders[guest];
        _positions[position] = null;//this position is now open
        _riders.Remove(guest);
        _guests.Remove(guest);
        guest.transform.parent = null; //unparenting the guest from the car
        guest.NextDestination();
        return true;
    }

    public bool LoadingGuest(Guest guest)
    {
        if (!_riders.ContainsKey(guest))
        {
            //if the car is full, we can't add the new rider
            if (_riders.Count >= _positions.Count) return true;

            List<GameObject> gos = _positions.Keys.ToList();
            foreach (GameObject go in gos)
            {
                if (_positions[go] != null) continue;

                _positions[go] = guest;
                _riders.Add(guest, go);
                break;
            }
        }

        //guard statement if car is full
        if (!_riders.ContainsKey(guest)) return true;

        //load the guest (animate guest getting on the Car)
        guest.transform.position = Vector3.MoveTowards(guest.transform.position,
            _riders[guest].transform.position,
            Time.deltaTime * Speed);

        //if the guest hasn't reached the Car position, we indicate the loading is not finished
        if (Vector3.Distance(guest.transform.position, _riders[guest].transform.position) > 0.01f) return false;

        if (guest.Destination != null) { guest.Destination.RemoveGuest(guest); }
        return true;
    }




    public override Destination GetDestination(Vector3 vec)
    {
        Destination[] tempDestinations = _destinations;
        tempDestinations = tempDestinations.OrderBy(go => Mathf.Abs(go.transform.position.y - vec.y)).ToArray();
        //tempDestinations = tempDestinations.OrderBy(x => x.name).ToArray();
        //tempDestinations = tempDestinations.OrderBy(x => Vector3.Distance(x.transform.position, Vector3.zero)).ToArray();
        return tempDestinations[0];
    }

       

    public override Vector3 StartPosition(Vector3 vec)
    {
        if (_destinations.Length == 0) { return Vector3.zero; }
        Destination destination = GetDestination(vec);
        return destination.transform.position;
    }

    public override Vector3 EndPosition(Vector3 vec)
    {
        if (_destinations.Length == 0) { return Vector3.zero; }
        Destination destination = GetDestination(vec);
        return destination.transform.position;
    }





}
