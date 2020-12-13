﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestManager : MonoBehaviour
{
    [HideInInspector]
    public static GuestManager Instance { get; private set; }
    public int risk = 0;
    public float PercentSick = 0;

    public int totalcount;
    public int healthycount = 0;
    public int sickcount = 0;
    public int contamcount = 0;
    public int safeExit = 0;
    public int exitCount = 0;
    public int neutralExit = 0;

    public GameObject GuestPrefab; //guest gameobject to be instantiated
    public GameObject EmployeePrefab;

    public float EntranceRate = 0.5f; //the rate at which guests will enter

    public List<Guest> _guest = new List<Guest>(); //list of guests
    private List<Destination> _destinations = new List<Destination>(); //list of destinations
    private List<Destination> _partydests = new List<Destination>();
    private List<Guest> _exitedGuests = new List<Guest>(); //guests that will exit
    private GuestEntrance[] _guestEntrances;

    private float _lastEntrance = 0; //time since last entrant
    public int _occupancyLimit = 0; //occupancy limit maximum

    private GameObject fpCamObject;
    private Fpcam fpCamScript;

    private void Awake()
    {
        //Singleton Pattern
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        fpCamObject = GameObject.FindGameObjectWithTag("First Person Camera");
        fpCamScript = fpCamObject.GetComponent<Fpcam>();

        GameObject[] partygos = GameObject.FindGameObjectsWithTag("Party");
        GameObject[] destinations = GameObject.FindGameObjectsWithTag("Bath");
        destinations = Shuffle(destinations);

        foreach (GameObject go in destinations)
        {
            Destination destination = go.GetComponent<Destination>(); //getting the destination script from game object
            _destinations.Add(destination); //adding the destination script to the list
            _occupancyLimit += destination.OccupancyLimit; //increasing the occupancy limit maximum
        }

        foreach (GameObject go in partygos)
        {
            Destination partydest = go.GetComponent<Destination>();
            _partydests.Add(partydest);
        }

        _guestEntrances = GameObject.FindObjectsOfType<GuestEntrance>();

        AdmitGuest();
    }

        private GameObject[] Shuffle(GameObject[] objects)
    {
        GameObject tempGO;
        for (int i = 0; i < objects.Length; i++)
        {
            //Debug.Log("i: " + i);
            int rnd = Random.Range(0, objects.Length);
            tempGO = objects[rnd];
            objects[rnd] = objects[i];
            objects[i] = tempGO;
        }
        return objects;
    }

    private void AdmitGuest()
    {
        //guard statement, if bath house is full
        //if (_occupancyLimit <= _guest.Count) return;
        if (_guest.Count >= _occupancyLimit - 1) return;

        //instantiate guest
        int randomIndex = Random.Range(0, _guestEntrances.Length);
        Vector3 position = _guestEntrances[randomIndex].transform.position;
        GameObject guest = Instantiate(GuestPrefab, position, Quaternion.identity); //adding our gameobject to scene
        _guest.Add(guest.GetComponent<Guest>()); //adding our gameobject guest script to the guest list
        Guest guestScript = guest.GetComponent<Guest>();
        fpCamScript.FollowMe(guestScript);
        //List<Destination> visitedBaths = guestScript.VisitedBaths();
        AssignOpenBath(guestScript);
    }

    public virtual void AssignOpenBath(Guest guest, List<Destination> visited = null)
    {
        foreach (Destination bath in _destinations)
        {
            //if bath is full guard statement
            if (bath.IsFull()) continue; //continue goes to the next line

            //make sure bath hasn't already been visited
            if (visited != null)
            {
                if (visited.Contains(bath))
                {
                    continue;
                }
            }

            //assign destination;
            guest.Destination = bath;
            bath.AddGuest(guest);
            break;
        }
    }
    public virtual void AssignParty(Guest guest, List<Destination> visited = null)
    {
        foreach (Destination party in _partydests)
        {
            //if bath is full guard statement
            

            //make sure bath hasn't already been visited
            if (visited != null)
            {
                if (visited.Contains(party))
                {
                    continue;
                }
            }

            //assign destination;
            guest.Destination = party;
            party.AddGuest(guest);
            break;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        

        //call guest update on each guest, the manager controls the guests
        foreach (Guest guest in _guest)
        {
            guest.GuestUpdate();
        }

        fpCamScript.CamUpdate();

        if (_exitedGuests.Count >= 0) { ExitGuests(); }

        //admit guests after entrance rate
        if (EntranceRate <= _lastEntrance)
        {
            AdmitGuest();
            _lastEntrance = 0;
            return;
        }
        else //if(EntranceRate > _lastEntrance)
        {
            _lastEntrance += Time.deltaTime;
        }

        totalcount = _guest.Count;
    }

    public void ExitGuests()
    {
        //foreach(Guest guest in _exitedGuests)
        for (int i = 0; i < _exitedGuests.Count; i++)
        {
            Guest guest = _exitedGuests[i];
            _guest.Remove(guest);
            fpCamScript.EndCamFollow(guest);
            if (guest.Health == Guest.Feeling.Healthy)
            {
                healthycount -= 1;
                safeExit += 1;
            }
            if (guest.Health == Guest.Feeling.Sick)
            {
                sickcount -= 1;
                neutralExit += 1;
            }
            if (guest.Health == Guest.Feeling.Contaminated)
            {
                contamcount -= 1;
            }
            exitCount += 1;
            Destroy(guest.gameObject);
        }
        _exitedGuests.Clear();
    }

    public void GuestExit(Guest guest)
    {
        _exitedGuests.Add(guest);
    }

    public virtual List<Guest> GuestList()
    {
        return _guest;
    }

    public virtual List<Destination> DestinationList()
    {
        return _destinations;
    }

    public virtual Destination RandomEntrance()
    {
        int randomIndex = Random.Range(0, _guestEntrances.Length);
        return _guestEntrances[randomIndex];
    }
}