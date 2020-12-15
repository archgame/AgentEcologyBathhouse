using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Testsr : MonoBehaviour
{
    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float NavMeshDistance(Vector3 start, Vector3 end)
    {
        //in case they are the same position
        if (Vector3.Distance(start, end) < 0.01f) { Debug.Log("SmallDistanceError"); return 0; }
        
        //move agent to the start position
        Vector3 initialPosition = transform.position;           //remembers where he was
        agent.enabled = false;
        transform.position = start;//_agent.Move(start - initialPosition);
        agent.enabled = true;
        
        //test to see if agent has path or not
        float distance = Mathf.Infinity;
        NavMeshPath navMeshPath = agent.path;
        if (!agent.CalculatePath(end, navMeshPath))
        {
            //reset agent to original position
            agent.enabled = false;
            transform.position = initialPosition;//_agent.Move(initialPosition - start);
            agent.enabled = true;
            //Debug.Log("Infinity1: " + distance);
            return distance;
        }
        
        //check to see if there is a path
        Vector3[] path = navMeshPath.corners;
        if (path.Length < 2 || Vector3.Distance(path[path.Length - 1], end) > 2) //2
        {
            //reset agent to original position
            agent.enabled = false;
            transform.position = initialPosition;//_agent.Move(initialPosition - start);
            agent.enabled = true;
            //Debug.Log("Infinity2: " + distance);
            return distance;
        }
        
        //get walking path distance
        distance = 0;
        for (int i = 1; i < path.Length; i++)
        {
            distance += Vector3.Distance(path[i - 1], path[i]);
            Debug.DrawLine(path[i - 1], path[i], Color.red); //visualizing the path, not necessary to return
            
        }
        
        //reset agent to original position
        agent.enabled = false;
        transform.position = initialPosition;//_agent.Move(initialPosition - start);
        agent.enabled = true;
        
        return distance;
    }
}
