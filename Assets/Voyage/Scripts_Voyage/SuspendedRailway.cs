using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SuspendedRailway : MonoBehaviour
{
    public GameObject Car;
    public GameObject Positions;
    public List<float> _buttonPressed = new List<float>();

    public enum State { MOVING, WAITING };

    public State CurrentState = State.WAITING;

    private Destination[] _destinations;

    //info of all guests
    private Dictionary<Guest, Vector3> _guests = new Dictionary<Guest, Vector3>();

    //info of guests in the car
    private Dictionary<GameObject, Guest> _positions = new Dictionary<GameObject, Guest>();
    
    private Dictionary<Guest, GameObject> _riders = new Dictionary<Guest, GameObject>();

    private float _maxWait = 1.0f;
    private float _waitTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
