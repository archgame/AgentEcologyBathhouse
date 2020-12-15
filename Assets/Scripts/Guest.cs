using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Guest : MonoBehaviour
{
    [Header("UI")]
    public Text Text;

    public Slider Slider;

    public List<Guest> guestEncounters = new List<Guest>();
    public List<Guest> totalGuests = new List<Guest>();

    public enum Action { BATHING, WALKING, RIDING, DANCING }
    public enum Feeling { Healthy, Sick, Contaminated }

    [Header("Destination")]
    //public global variables
    public Destination Destination;             //where the agent is going

    public int Baths = 15;                      //the number of baths our guest will take
    public float BathTime = 2.0f;               //how long the agent stays in
    public Action Status;                       //our agent's current status
    public Feeling Health;                      //the current state of guest's health
    public bool iamsad = false;

    public GameObject[] glasses;
    private Renderer[] glassglow;
    public float SocialVal = 100.0f;
    private float SocialSub = 0.1f;
    public float _socialtimer = 0;
    public float socialtime = 20f;
    private Vector3 dancebase;
    
    //private global variables
    private float _bathTime = 0;                //how long the agent has been in the bath

    [HideInInspector]
    private NavMeshAgent _agent;                //our Nav Mesh Agent Component

    [HideInInspector]
    private Conveyance _currentConveyance = null;

    public List<Destination> _destinations = new List<Destination>();   //list of destinations to go to
    public Destination _tempDestination;                                //logs old destination for use later
    private List<Destination> _visitedBaths = new List<Destination>();  //list of baths already gone to
    public float destdist = 0;
    //public Vector2 WanderTimer = new Vector2(2, 5);
    //private float _wanderTimer = 2;
    //internal Vector3 velocity;
    private Renderer GuestCol;
    //variables for assigning susceptibility and then interaction count before sick
    private int _susceptibility = 0;
    private int _interactioncounter = 0;
    public Glasses[] glassesscript;

    private void Start()
    {

        int sickchance = Random.Range(0, 100);
        if (sickchance < GuestManager.Instance.PercentSick)
        {
            Health = Feeling.Sick;
            GuestManager.Instance.sickcount += 1;
        }
        else
        {
            Health = Feeling.Healthy;
            GuestManager.Instance.healthycount += 1;
            //if healthy, assign susceptibility variable
            _susceptibility = Random.Range(25, 100);
        }

        glassesscript = GetComponentsInChildren<Glasses>();

        _agent = GetComponent<NavMeshAgent>();                  //adds navmesh component
        //Status = Action.RANDOM;
        //Vector3 newPos = RandomNavSphere(transform.position, 100, -1);
        //UpdateDestination(newPos);
        GuestCol = GetComponent<Renderer>();                    //controls the renderer of the guest

        Status = Action.WALKING;                                //sets start status to walking
        UpdateDestination();                                    //updates the destination at start
        FindPath(ref _currentConveyance, ref _destinations);    //finds path to said destination
    }

    // Update is called once per frame
    public void GuestUpdate()
    {
        if (Destination == null)
        {
            if (Baths == 0)
            {
                Destination = GuestManager.Instance.RandomEntrance();
            }
            else
            {
                GuestManager.Instance.AssignOpenBath(this, _visitedBaths); //Destination is assigned inside method
            }
        }

        if (GuestManager.Instance._sadguest.Contains(this))
        { iamsad = true; }
        else
        { iamsad = false; }

        if(SocialVal > 100)
        {
            SocialVal = 100;
        }
        else if (SocialVal > 0)
        {
            SocialVal -= SocialSub;
        }
        else if (Status != Action.DANCING)
        {
            SocialVal = 0;
            if (!GuestManager.Instance._sadguest.Contains(this))
            {
                GuestManager.Instance._sadguest.Add(this);
                //Debug.Log(":( SAAAAD");
                //Debug.Log(GuestManager.Instance._sadguest.Count);
            }
        }

        //updates guest color
        if (Health == Feeling.Healthy)
        {
            GuestCol.material.color = Color.green;
        }
        if (Health == Feeling.Sick)
        {
            GuestCol.material.color = Color.red;
        }
        if (Health == Feeling.Contaminated)
        {
            GuestCol.material.color = Color.yellow;
        }

        foreach (Glasses g in glassesscript)
        {
            g.socialcol = SocialVal;
        }

        //checks guets for contamination contact and updates
        for (int i = 0; i < GuestManager.Instance._guest.Count; i++)
        {
            Guest guesti = GuestManager.Instance._guest[i];

            if (Vector3.Distance(transform.position, guesti.transform.position) < 2.0f)
            {
                //Debug.Log("TooClose");

                if (true) //!guestEncounters.Contains(guesti))
                {
                    //if (guesti.transform.position != transform.position)
                    if(guesti != this)
                    {
                        if (guesti.Health != Feeling.Healthy)
                        {
                            if (Health == Feeling.Healthy)
                            {
                                _interactioncounter += 1;
                                guestEncounters.Add(guesti);
                                
                                if (_interactioncounter >= _susceptibility)
                                {
                                    Health = Feeling.Contaminated;
                                    GuestManager.Instance.risk += 1;
                                    //this.SetSlider(1);
                                    GuestManager.Instance.healthycount -= 1;
                                    GuestManager.Instance.contamcount += 1;
                                }
                                else
                                {
                                    return;
                                }
                            }

                        }

                    }

                }
            }

        }

        if (Status == Action.DANCING)
        {
            _socialtimer += Time.deltaTime;
            if (_socialtimer > socialtime)
            {
                SocialVal = 100f;
                _tempDestination = Destination;
                Destination = null;
                transform.position = dancebase;

                if (Baths == 0)
                {
                    Destination = GuestManager.Instance.RandomEntrance();
                }
                else
                {
                    GuestManager.Instance.AssignOpenBath(this, _visitedBaths); //Destination is assigned inside method
                }

                if (Destination == null)
                {
                    Debug.Log("NO DESTINATION");
                    return;
                }

                _agent.enabled = true;
                _agent.isStopped = false;

                SetText("Walking");
                                
                _destinations[0].RemoveGuest(this); //remove guest from current bath
                _destinations.RemoveAt(0); //remove current bath from destination list
                _socialtimer = 0; //reseting social time
                Status = Action.WALKING;  //start walking
                UpdateDestination(); //update new destination
                FindPath(ref _currentConveyance, ref _destinations); //finding best path
                return;
            }
            else
            {
                Vector3 dancechange = GuestManager.Instance.DanceVector();
                transform.position = dancebase + dancechange;
                return;
            }
        }

        if (Status == Action.RIDING)
        {
            _currentConveyance.ConveyanceUpdate(this);
        }

        if (Status == Action.BATHING)
        {
            _bathTime += Time.deltaTime;
            if (_bathTime > BathTime)
            {
                _tempDestination = Destination;
                Destination = null;

                /*/if (SocialVal <= 0)
                {
                    if (GuestManager.Instance._partydests != null)
                    {
                        Debug.Log("wherestheparty");
                        GuestManager.Instance.AssignParty(this);
                        return;
                    }
                }/*/
                if (Baths == 0)
                {
                    Destination = GuestManager.Instance.RandomEntrance();
                }
                else
                {
                    GuestManager.Instance.AssignOpenBath(this, _visitedBaths); //Destination is assigned inside method
                }
                if (Destination == null) { Debug.Log("error"); return; }

                SetText("Walking");
                //_tempDestination.RemoveGuest(this); //remove guest from current bath
                _destinations[0].RemoveGuest(this); //remove guest from current bath
                _destinations.RemoveAt(0); //remove current bath from destination list
                _bathTime = 0; //reseting bath time
                Status = Action.WALKING;  //start walking
                UpdateDestination(); //update new destination
                FindPath(ref _currentConveyance, ref _destinations); //finding best path
            }

            return;
        }

        //guard statement
        if (Destination == null) return; //return stops the update here until next frame

        //orient gameobject direction
        if (_agent.enabled && _agent.velocity != Vector3.zero)
        {
            Vector3 forward = _agent.velocity;
            forward.y = 0;
            transform.forward = forward;
        }
        DestinationDistance(); //++++

        /*/
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GuestCol.material.color = Color.green;
        }
        /*/
    }

    public virtual void GuestWalkDestination()
    {
        Status = Action.WALKING;
        UpdateDestination();
        FindPath(ref _currentConveyance, ref _destinations);
    }

    private void DestinationDistance()      //tests destination proximity for arrival
    {
        if (Vector3.Distance(transform.position, Destination.transform.position) < 4.0f)
        {
            if (Destination.tag == "Party")
            {
                PartyTime();
                return;
            }
        }
        
        //test agent distance from destination
        if (Vector3.Distance(transform.position, Destination.transform.position) < 1.1f)
        {
            if (Destination.GetComponentInParent<Conveyance>())
            {
                Status = Action.RIDING;
                _agent.enabled = false;
                _currentConveyance = Destination.GetComponentInParent<Conveyance>();
                return;
            }
            else if (Destination.tag == "Bath")
            {
                StartBath();
                return;
            }
            else if (Destination.tag == "Entrance")
            {
                GuestManager.Instance.GuestExit(this);
                //GuestManager manager = Destination.gameObject.GetComponent<GuestManager>();
                //manager.GuestExit(this);
                return;
            }
        }
    }

    public void UpdateDestination()
    {
        _agent.SetDestination(Destination.transform.position);
        _agent.isStopped = false;
    }

    public virtual void NextDestination()
    {
        _agent.enabled = true;                                  //turns on the agents navmesh
        _destinations.RemoveAt(0);                              //removes current destination
        Destination = _destinations[0];                         //sets destination to next in line
        Status = Action.WALKING;                                //starts walking to that destination
        FindPath(ref _currentConveyance, ref _destinations);    //this allows multiple conveyances
    }

    public static float AgentWalkDistance(NavMeshAgent agent, Transform trans, Vector3 start, Vector3 end, Color color)
    {
        //in case they are the same position
        if (Vector3.Distance(start, end) < 0.01f) { Debug.Log("SmallDistanceError"); return 0; }
        Guest gst = agent.GetComponent<Guest>();

        //move agent to the start position
        Vector3 initialPosition = trans.position; //remembers where he was
        bool startenabled = agent.enabled;
        agent.enabled = false;
        trans.position = start;//_agent.Move(start - initialPosition);
        agent.enabled = true;

        //agent.SetDestination(end);
        //agent.isStopped = false;

        //test to see if agent has path or not

        float distance = Mathf.Infinity;
        NavMeshPath navMeshPath = agent.path;
        if (!agent.CalculatePath(end, navMeshPath))
        {
            //Debug.Log("error1");
            //reset agent to original position
            agent.enabled = false;
            trans.position = initialPosition;//_agent.Move(initialPosition - start);
            agent.enabled = startenabled;

            //Debug.Log("Infinity1: " + distance);
            return distance;
        }

        //check to see if there is a path
        Vector3[] path = navMeshPath.corners;
        if (path.Length < 2 || Vector3.Distance(path[path.Length - 1], end) > 2) //2
        {
            //Debug.Log("error2");
            //reset agent to original position
            agent.enabled = false;
            trans.position = initialPosition;//_agent.Move(initialPosition - start);
            agent.enabled = startenabled;
            //Debug.Log("Infinity2: " + distance);
            return distance;
        }

        //get walking path distance
        distance = 0;
        for (int i = 1; i < path.Length; i++)
        {
            distance += Vector3.Distance(path[i - 1], path[i]);
            if (color != Color.black) { /*/Debug.DrawLine(path[i - 1], path[i], color);/*/ } //visualizing the path, not necessary to return
            //Debug.Log("calculated");
        }

        //reset agent to original position
        agent.enabled = false;
        trans.position = initialPosition;//_agent.Move(initialPosition - start);
        agent.enabled = startenabled;

        return distance;
    }

    public virtual void FindPath(ref Conveyance currentConveyance, ref List<Destination> destinations)
    {
        //Debug.Break();

        //get walking path distance
        Vector3 guestPosition = transform.position;
        Vector3 destinationPosition = Destination.transform.position;
        float distance = AgentWalkDistance(_agent, transform, guestPosition, destinationPosition, Color.yellow);
        //Debug.Log("WalkingDistance: " + distance);

        //test all conveyances
        currentConveyance = null;
        Conveyance[] conveyances = GameObject.FindObjectsOfType<Conveyance>();
        foreach (Conveyance c in conveyances)
        {
            //guard statement, how many people are on the conveyance

            if (c.IsFull()) continue;

            float distToC = AgentWalkDistance(_agent, transform, guestPosition, c.StartPosition(_agent, guestPosition), Color.green);
            float distC = c.WeightedTravelDistance(_agent, guestPosition, destinationPosition);
            float distFromC = AgentWalkDistance(_agent, transform, c.EndPosition(_agent, destinationPosition), destinationPosition, Color.red);
            //Debug.Log("TripStart: " + c.GetDestination(_agent, guestPosition));
            //Debug.Log("TripEnd: " + c.GetDestination(_agent, destinationPosition));
            //Debug.Log("Bath: " + Destination);
            //Debug.Log("distToC: " + distToC);
            //Debug.Log("distC: " + distC);
            //Debug.Log("distFromC: " + distFromC);

            //Debug.DrawLine(guestPosition, c.StartPosition(_agent, guestPosition), Color.black);
            //Debug.DrawLine(c.StartPosition(_agent, guestPosition), c.EndPosition(_agent, destinationPosition), Color.cyan);
            //Debug.DrawLine(c.EndPosition(), destinationPosition, Color.white);
            //Debug.Log("ConveyanceDistance: " + (distToC + distC + distFromC));
            //Debug.Break();

            if (distance > distToC + distC + distFromC)
            {
                currentConveyance = c;
                distance = distToC + distC + distFromC;
            }
        }

        //if there are no conveyances, we update the destination list with current destination
        if (_currentConveyance == null)
        {
            destinations.Clear();
            destinations.Add(Destination);
            UpdateDestination();
            return;
        }

        if (_currentConveyance.GetType() == typeof(Scooter))
        {
            Scooter scooter = _currentConveyance as Scooter;
            scooter.SetWaiting(this);
        }
        destinations.Clear();
        destinations.Add(currentConveyance.GetDestination(_agent, guestPosition));
        destinations.Add(Destination);
        Destination = destinations[0];
        UpdateDestination();

        destdist = distance;

        //Debug.Break();


    }

    /// <summary>
    /// Start bath by changing agent status and stopping the agent
    /// </summary>
    private void StartBath()
    {
        Baths--;
        _visitedBaths.Add(Destination);
        Status = Action.BATHING;
        _agent.isStopped = true;
        SetText("Bathing");
    }

    public void PartyTime()
    {
        dancebase = transform.position;
        Status = Action.DANCING;
        GuestManager.Instance._sadguest.Remove(this);
        if (_agent.enabled == true)
        {
            _agent.isStopped = true;
            _agent.enabled = false;
        }
        SetText("Dancing");
    }

    public virtual Destination GetUltimateDestination()
    {
        if (_destinations.Count == 0) return null;
        return _destinations[_destinations.Count - 1];
    }

    public virtual void SetText(string text)
    {
        if (Text == null) return;
        Text.text = text;
    }

    public virtual void SetSlider(float i)
    {
        if (Slider == null) return;
        Slider.value = i;
    }

    public virtual string GetText()
    {
        if (Slider == null) return string.Empty;
        return Text.text;
    }

    public virtual float GetSliderValue()
    {
        if (Slider == null) return Mathf.Infinity;
        return Slider.value;
    }

    public virtual List<Destination> VisitedBaths()
    {
        return _visitedBaths;
    }

    public void EndConveyance()
    {
        if (_currentConveyance != null)
        {
            _currentConveyance.EjectGuest(this);
        }
    }

    /*/public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        //Debug.Break();
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        //Debug.DrawLine(origin, randDirection, Color.blue);
        //Debug.DrawRay(navHit.position, Vector3.up * 3, Color.cyan);
        return navHit.position;
    }/*/
}