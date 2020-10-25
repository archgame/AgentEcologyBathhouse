using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestManager : MonoBehaviour
{
    [HideInInspector]

    public static GuestManager Instance { get; private set; }

    public GameObject GuestPrefab; //guest gameobject to be instantiated

    public float EntranceRate = 0.5f; //the rate at which guests will enter

    private List<Guest> _guest = new List<Guest>(); //list of guests
    private List<Destination> _destinations = new List<Destination>(); //list of destinations
    private List<Guest> _exitedGuests = new List<Guest>(); //guests that will exit

    private float _lastEntrance = 0; //time since last entrant
    private int _occupancyLimit = 0; //occupancy limit maximum

    public GameObject fpCamObject;
    public Fpcam fpCamScript;

    private void Awake()
    {
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

        GameObject[] destinations = GameObject.FindGameObjectsWithTag("Bath");

        foreach (GameObject go in destinations)
        {
            Destination destination = go.GetComponent<Destination>(); //getting the destination script from game object
            _destinations.Add(destination); //adding the destination script to the list
            _occupancyLimit += destination.OccupancyLimit; //increasing the occupancy limit maximum
        }
        AdmitGuest();    
    }

    private void AdmitGuest()
    {
        //guard statement, if bath house is full
        //if (_occupancyLimit <= _guest.Count) return;
        if (_guest.Count >= _occupancyLimit) return;



        foreach (Destination bath in _destinations)
        {
            //if bath is full guard statement
            if (bath.IsFull()) continue; //continue goes to the next line

            //instantiate guest
            GameObject guest = Instantiate(GuestPrefab, transform.position, Quaternion.identity); //adding our gameobject to scene
            _guest.Add(guest.GetComponent<Guest>()); //adding our gameobject guest script to the guest list
            Guest guestScript = guest.GetComponent<Guest>();
            fpCamScript.FollowMe(guestScript);
            AssignOpenBath(guestScript);
        }
    }

    public void AssignOpenBath(Guest guest)
    {
        foreach (Destination bath in _destinations)
        {
            if (bath.IsFull()) continue;

            Guest guestScript = guest.GetComponent<Guest>();
            if (guestScript._visited != null)
            {
                if (guestScript._visited.Contains(bath))
                {
                    continue;
                }
            }

            guest.Destination = bath;
            bath.AddGuest(guest);
            guestScript._visited.Add(bath);
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

        fpCamScript.CamUpdate();
    }

    public void ExitGuests()
    {
        //foreach(Guest guest in _exitedGuests)
        for (int i = 0; i < _exitedGuests.Count; i++)
        {
            Guest guest = _exitedGuests[i];
            _guest.Remove(guest);
            fpCamScript.EndCamFollow(guest);
            Destroy(guest.gameObject);
        }
        _exitedGuests.Clear();
    }

    public void GuestExit(Guest guest)
    {
        _exitedGuests.Add(guest);
    }
}