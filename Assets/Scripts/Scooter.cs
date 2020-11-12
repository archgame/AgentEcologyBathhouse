using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Destination))]
public class Scooter : Conveyance
{
    public enum Action { WALKING, RIDING, WAITING, SEARCHING }

    public Action Status;
    private float _timer = 0;
    public Vector2 WanderTimer = new Vector2(2, 5);
    private float _wanderTimer = 2;
    private int _currentPathIndex = 0;
    private Vector3 _guestDestination = Vector3.zero;
    private Guest _guest = null;
    public Boolean fallen = false;


    [HideInInspector]
    public NavMeshAgent _agent; //our Nav Mesh Agent Component

    private Destination _dest;


    public override void SetDestination()
    {
        _dest = GetComponent<Destination>();
        _agent = GetComponent<NavMeshAgent>();
        Status = Action.WAITING;
        Vector3 newPos = Guest.RandomNavSphere(transform.position, 100, -1);
        UpdateDestination(newPos);
        if (Path.Length == 0) return;

        UpdateDestination(Path[_currentPathIndex].transform.position);
    }

    // Update is called once per frame
    private void Update()
    {


        if (Status == Action.RIDING)
        {
           
            //make scooter face forward relative to velocity
            if (_agent.velocity.sqrMagnitude > Mathf.Epsilon)
            {
                transform.rotation = Quaternion.LookRotation(_agent.velocity.normalized);
                _guest.transform.rotation = transform.rotation; //and the guest faces that direction too
            }

            //if the vehicle is more than two vehicle widths away from destination return
            if (Vector3.Distance(transform.position, _guestDestination) > (transform.localScale.x * 2) + 2) return;

            //after this the vehicle has arrived at the destination

            //unload agent
            _guest.transform.parent = null;
            _guest.NextDestination();
            _guest = null;
            //_agent.enabled = false; //make scooters stop walking around
                                    
            //scooter falls over when WAITING
            if (!fallen)
            {
               Debug.Log("falling");
               //transform.Rotate(0.0f, 0.0f, -90.0f, Space.Self);
               fallen = true;
            }
            

            Status = Action.WAITING;

            //this has to do with the random walking mechanic
            /*/if (Path.Length <= 2)
            {
                Vector3 newPos = Guest.RandomNavSphere(transform.position, 100, -1);
                UpdateDestination(newPos);
                _timer = 0;
                _wanderTimer = Random.Range(0, 0);
            }
            else
            {
                UpdateDestination(Path[_currentPathIndex].transform.position);
            }/*/

        }
    }






    public override void ConveyanceUpdate(Guest guest)
    {
        if (Status != Action.WAITING) return;

        if (Vector3.Distance(transform.position, guest.transform.position)
            > transform.localScale.x + guest.transform.localScale.x + 1f) return;
        
        // scooter stands up when ready to be used
        if (fallen)
        {
            Debug.Log("pickup");
            //transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
            fallen = false;
        }
        

        Status = Action.RIDING;
        _agent.enabled = true;

        // position scooter and guest at ground level
        guest.transform.position = transform.position + Vector3.up;
        guest.transform.parent = transform;
        

        //guest.transform.rotation = Quaternion.Euler(0.0f, 0.0f, gameObject.transform.rotation.z * -1.0f);
        //guest.transform.position = transform.position + new Vector3 (1, 0, 0);
        _guestDestination = guest.GetUltimateDestination().transform.position;
        UpdateDestination(_guestDestination);
    }



    public void SetWaiting(Guest guest)
    {
        if (_guest == guest) return;
        _guest = guest;
        _agent.enabled = false;
        Status = Action.WAITING;
    }

    private void UpdateDestination(Vector3 position)
    {
        _agent.SetDestination(position);
        _agent.isStopped = false;
    }

    public override float WeightedTravelDistance(Vector3 start, Vector3 end)
    {
        Vector3 destination = _agent.destination;

        float toGuest = Guest.AgentWalkDistance(_agent, transform, transform.position, start, Color.green);
        float withGuest = Guest.AgentWalkDistance(_agent, transform, transform.position, end, Color.green);
        UpdateDestination(destination);

        if (toGuest == Mathf.Infinity || withGuest == Mathf.Infinity) return Mathf.Infinity;

        float distance = toGuest + withGuest;
        distance /= Weight;
        return distance;
    }

    public override Vector3 StartPosition(Vector3 vec)
    {
        return transform.position;
    }

    public override Vector3 EndPosition(Vector3 vec)
    {
        return vec; //assuming vehicle will take guest to final destination
    }

    public override Destination GetDestination(Vector3 vec)
    {
        return _dest;
    }

    public override bool IsFull()
    {
        if (_guest == null) return false;
        return true;
    }
}