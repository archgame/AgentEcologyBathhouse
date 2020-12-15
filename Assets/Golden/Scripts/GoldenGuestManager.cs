using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenGuestManager : MonoBehaviour
{
    [HideInInspector]
    public static GoldenGuestManager Instance { get; private set; } //for singleton

    public GameObject GuestPrefab; //{get;set;}guest gameobject to be instantiated
    public GameObject EmployeePrefab;

    public float EntranceRate = 0.5f; //the rate at which guests will enter

    private List<Guest> _guest = new List<Guest>(); //list of guests
    private List<Guest> _employee = new List<Guest>(); //list of guests
    [HideInInspector]
    public List<Destination> _destinations = new List<Destination>(); //list of destinations
    [HideInInspector]
    public List<Destination> _vipDestinations = new List<Destination>(); //list of destinations
    private List<Guest> _exitedGuests = new List<Guest>(); //guests that will exit
    private GuestEntrance[] _guestEntrances;
    private EmployeeEntrance[] _employeeEntrances;

    private float _lastEntrance = 0; //time since last entrant
    private int _occupancyLimit = 0; //occupancy limit maximum
    private int _occupancyLimitv = 0; //occupancy limit maximum

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
        GameObject[] destinations = GameObject.FindGameObjectsWithTag("Bath");
        //Debug.Log("101");
        destinations = Shuffle(destinations);
        //Debug.Log("102");
        foreach (GameObject go in destinations)
        {
            Destination destination = go.GetComponent<Destination>(); //getting the destination script from game object
            _destinations.Add(destination); //adding the destination script to the list
            _occupancyLimit += destination.OccupancyLimit; //increasing the occupancy limit maximum
        }

       // Debug.Log("102.5");
       GameObject[] destinationsv = GameObject.FindGameObjectsWithTag("BathV");
        //Debug.Log("101.2");
       destinationsv = Shuffle(destinationsv);
       // Debug.Log("102.2");
      foreach (GameObject gov in destinationsv)
       {
           Destination destinationv = gov.GetComponent<Destination>(); //getting the destination script from game object
            _vipDestinations.Add(destinationv); //adding the destination script to the list
        //    Debug.Log(_destinations.Count);
        //    _occupancyLimitv += destinationv.OccupancyLimit; //increasing the occupancy limit maximum
       }
        //Debug.Log("vipe " + _vipDestinations.Count);

        _guestEntrances = GameObject.FindObjectsOfType<GuestEntrance>();
        //Debug.Log("103 " + _guestEntrances.Length);
        _employeeEntrances = GameObject.FindObjectsOfType<EmployeeEntrance>();
        //Debug.Log("104 " + _employeeEntrances.Length);

        AdmitGuest();
        //Debug.Log("105");
        //AdmitEmployee();
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
        //Debug.Log("106.1");
        //guard statement, if bath house is full
        //if (_occupancyLimit <= _guest.Count) return;
        if (_guest.Count >= _occupancyLimit - 1) return;
        //Debug.Log("106");

        //instantiate guest
        int randomIndex = Random.Range(0, _guestEntrances.Length);
        Vector3 position = _guestEntrances[randomIndex].transform.position;
        GameObject guest = Instantiate(GuestPrefab, position, Quaternion.identity); //adding our gameobject to scene
        _guest.Add(guest.GetComponent<Guest>()); //adding our gameobject guest script to the guest list
        Guest guestScript = guest.GetComponent<Guest>();

        //Debug.Log("107");
        //List<Destination> visitedBaths = guestScript.VisitedBaths();
        AssignOpenBath(guestScript, _destinations);
        //Debug.Log("108");
    }

    private void AdmitEmployee()
    {
        //Debug.Log("109.0");
        if (_guest.Count % 6 != 0) return;
        //Debug.Log("110");
        //if (_guest.Count/6 <= _employee.Count) return;
        //Debug.Log("111.0");

       // Debug.Log("109");
       // if (_employee.Count >= _occupancyLimitv - 1) return;
       // Debug.Log("110");

        //instantiate guest
        int randomIndex = Random.Range(0, _employeeEntrances.Length);
        Vector3 position = _employeeEntrances[randomIndex].transform.position;
        GameObject guest = Instantiate(EmployeePrefab, position, Quaternion.identity); //adding our gameobject to scene
        _employee.Add(guest.GetComponent<VIPGuest>()); //adding our gameobject guest script to the guest list
        VIPGuest guestScript = guest.GetComponent<VIPGuest>();
        //List<Destination> visitedBaths = guestScript.VisitedBaths();
        AssignOpenBath(guestScript, _vipDestinations);
        //Debug.Log("112");
        //Debug.Break();
    }

    public virtual void AssignOpenBath(Guest guest, List<Destination> destinations, List<Destination> visited = null)
    {
        //Debug.Log("113");
        
        foreach (Destination bath in destinations)
        {
           // Debug.Log(bath.name);
            //Debug.Log("114 " + destinations.Count);
            //if bath is full guard statement
            if (bath.IsFull()) continue; //continue goes to the next line
            //Debug.Log("115");

            //make sure bath hasn't already been visited
            if (visited != null)
            {
                if (visited.Contains(bath))
                {
                    continue;
                }
            }
           // Debug.Log("117 " + bath.name);

            //assign destination;
            guest.Destination = bath;
            //Debug.Log("118");
            bath.AddGuest(guest);
            //Debug.Log("119");
            break;
        }
        //Debug.Break();
    }
    

    // Update is called once per frame
    private void Update()
    {
        //call guest update on each guest, the manager controls the guests
        foreach (Guest guest in _guest)
        {
            guest.GuestUpdate();
        }
        foreach (Guest guest in _employee)
        {
            guest.GuestUpdate();
        }

        if (_exitedGuests.Count >= 0) { ExitGuests(); }

        //admit guests after entrance rate
        if (EntranceRate <= _lastEntrance)
        {
            AdmitEmployee();
            AdmitGuest();
            _lastEntrance = 0;
            return;
        }
        else //if(EntranceRate > _lastEntrance)
        {
            _lastEntrance += Time.deltaTime;
        }
    }

    public virtual void ExitGuests()
    {
        //foreach(Guest guest in _exitedGuests)
        for (int i = 0; i < _exitedGuests.Count; i++)
        {
            Guest guest = _exitedGuests[i];
            if (_guest.Contains(guest))
            {
                _guest.Remove(guest);
            }
            if (_employee.Contains(guest))
            {
                _employee.Remove(guest);
            }

            Destroy(guest.gameObject);
        }
        _exitedGuests.Clear();
    }

    public virtual void GuestExit(Guest guest)
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

    public virtual Destination RandomEntrance(Guest guest)
    {
        string name = guest.name.Replace("(Clone)", "");
        int randomIndex = 0;
        if (name == GuestPrefab.name)
        {
            randomIndex = Random.Range(0, _guestEntrances.Length);
            return _guestEntrances[randomIndex];
        }

        randomIndex = Random.Range(0, _employeeEntrances.Length);
        return _employeeEntrances[randomIndex];


    }

    //public void AssignOpenReception(VIPGuest guest)
    //{

     //   GameObject[] receptions = GameObject.FindGameObjectsWithTag("Reception");
     //   foreach (GameObject re in receptions)
      //  {
      //      Destination redestination = re.GetComponent<Destination>(); //getting the destination script from game object
      //      _receptions.Add(redestination); //adding the destination script to the list                             
      //  }
//
      //  foreach (Destination redestination in _receptions)
      //  {
      //      //if bath is full guard statement
      //      if (redestination.IsFull()) continue; //continue goes to the next line
      //
       //     //make sure bath hasn't already been visited
//
//
        //    //assign destination;
        //    guest.Destination = redestination;
        //    redestination.AddGuest(guest);
       //     break;
       // }
    
}