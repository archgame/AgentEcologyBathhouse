using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GuestManager : MonoBehaviour
{
    [HideInInspector]
    public static GuestManager Instance { get; private set; }

    public GameObject GuestPrefab; //guest gameobject to be instantiated
    public GameObject PartyPrefab;

    public int risk = 0;
    public float PercentSick = 0;

    public int totalcount;
    public int healthycount = 0;
    public int sickcount = 0;
    public int contamcount = 0;
    public int safeExit = 0;
    public int exitCount = 0;
    public int neutralExit = 0;
     

    public float EntranceRate = 0.5f; //the rate at which guests will enter

    public List<Guest> _guest = new List<Guest>(); //list of guests
    public List<Guest> _sadguest = new List<Guest>();
    private List<Destination> _destinations = new List<Destination>(); //list of destinations
    public List<Destination> _partydests = new List<Destination>();
    private List<Guest> _exitedGuests = new List<Guest>(); //guests that will exit
    private GuestEntrance[] _guestEntrances;

    private float _lastEntrance = 0; //time since last entrant
    public int _occupancyLimit = 0; //occupancy limit maximum
    public int totalparties = 0;

    public GameObject fpCamObject;
    private Fpcam fpCamScript;

    public float dancetimer = 0.0f;
    public float dancespeed = 0.1f;

    private void Awake()
    {
        //Singleton Pattern
        Debug.Log("awake1");
        
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

        //Instance = this;

        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }

        //Debug.Log("awake2");
        //fpCamObject = GameObject.FindGameObjectWithTag("First Person Camera");
        fpCamScript = fpCamObject.GetComponent<Fpcam>();

        //fpCamScript = Fpcam.Instance;

        GameObject[] partygos = GameObject.FindGameObjectsWithTag("Party");
        GameObject[] destinations = GameObject.FindGameObjectsWithTag("Bath");
        destinations = Shuffle(destinations);

        foreach (GameObject go in destinations)
        {
            Destination destination = go.GetComponent<Destination>(); //getting the destination script from game object
            _destinations.Add(destination); //adding the destination script to the list
            _occupancyLimit += destination.OccupancyLimit; //increasing the occupancy limit maximum
        }

        _occupancyLimit /= 2;

        //Debug.Log("Occupancy Limit" + _occupancyLimit);

        foreach (GameObject go in partygos)
        {
            Destination partydest = go.GetComponent<Destination>();
            _partydests.Add(partydest);
        }

        _guestEntrances = GameObject.FindObjectsOfType<GuestEntrance>();

        //AdmitGuest();
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
        if (_guest.Count >= _occupancyLimit - 1) { Debug.Log("TooFull"); return; }

        //instantiate guest
        int randomIndex = Random.Range(0, _guestEntrances.Length);
        Vector3 position = _guestEntrances[randomIndex].transform.position;
        GameObject guest = Instantiate(GuestPrefab, position, Quaternion.identity); //adding our gameobject to scene
        Debug.Log("Instantiation" + guest.name);
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
        GuestCluster();

        dancetimer += Time.deltaTime;

        if (dancetimer >= 100f)
        {
            dancetimer = 0.0f;
        }

        //call guest update on each guest, the manager controls the guests
        foreach (Guest guest in _guest)
        {
            guest.GuestUpdate();
        }



        if (_exitedGuests.Count >= 0) { ExitGuests(); }

        fpCamScript.CamUpdate();

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

    public Vector3 DanceVector()
    {
        float x = 0; // 0.25f * (Mathf.Sin((360.00f / 100) * dancetimer));
        float y = 0.25f * (Mathf.Sin(400 * (Mathf.PI / 100) * dancetimer));
        float z = 0; // 0.25f * (Mathf.Sin((360.00f / 100) * dancetimer));
        Vector3 relpos = new Vector3(x, y, z);
        return relpos;
    }

    public bool GlassesToggle()
    {
        float y = Mathf.Sin((400 * (Mathf.PI / 200) * dancetimer) - (Mathf.PI / 2));
        if (y >= 0)
        {
            return true;
        }
        else
        { 
            return false;
        }
    }

    public void CreateCluster(List<Guest> cluster)
    {
        for (int i = 0; i < cluster.Count; i++)
        {
            if (!_sadguest.Contains(cluster[i]))
            {
                Debug.Log("partyignored");
                return;
            }

            cluster[i].EndConveyance();
        }

        float x = 0f;
        float y = 0f;
        float z = 0f;

        for (int i = 0; i < cluster.Count; i++)
        {
            x += cluster[i].transform.position.x;
            y += cluster[i].transform.position.y;
            z += cluster[i].transform.position.z;
        }
        //Debug.Log("partycreated");
        Vector3 center = new Vector3(x / cluster.Count, y / cluster.Count, z / cluster.Count);
        GameObject partycluster = Instantiate(PartyPrefab, center, Quaternion.identity);
        Destination partydest = partycluster.GetComponent<Destination>();
        x = 0;
        y = 0;
        z = 0;
        for (int i = 0; i < cluster.Count; i++)
        {

            partydest.AddGuest(cluster[i]);
            cluster[i].Destination = partydest;
            Vector3 spot = RandomNavSphere(center, 6f, -1);
            x += spot.x;
            y += spot.y;
            z += spot.z;
            Debug.DrawLine(cluster[i].transform.position, spot, Color.red);
            //Debug.Break();
            cluster[i].transform.position = (spot + Vector3.up);
            cluster[i].PartyTime();
        }
        partycluster.transform.position = new Vector3(x / cluster.Count, (y / cluster.Count) + 2, z / cluster.Count);
    }

    public void GuestCluster()
    {
        List<Guest> templist = _sadguest;
        List<Guest> clusterlist = new List<Guest>();
        int count = templist.Count;
        for (int i = 0; i < templist.Count; i++)
        {
            if (templist[i] == null) { continue; }
            clusterlist.Add(templist[i]);
            for (int j = i + 1; j < templist.Count; j++)
            {
                if (templist[j] == null) { continue; }
                float d = Vector3.Distance(templist[i].transform.position, templist[j].transform.position);
                if (d <= 4)
                {
                    clusterlist.Add(templist[j]);
                }
            }

            if (clusterlist.Count >= 3)
            {
                //Debug.Log("partyidentified");
                CreateCluster(clusterlist);
                clusterlist.Clear();
                return;
            }
            else
            {
                clusterlist.Clear();
            }
        }
        return;
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        //Debug.Break();
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, 2 * dist, layermask);
        //Debug.DrawLine(origin, randDirection, Color.blue);
        //Debug.DrawRay(navHit.position, Vector3.up * 3, Color.cyan);
        return navHit.position;
    }
}