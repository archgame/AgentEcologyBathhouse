using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Janitor3 : MonoBehaviour
{
    public GameObject JanitorsDestination;
    public float CleanTime = .9f;
    //public List<GameObject> _listofpuddles = new List<GameObject>();
    public int puddlecount = 0;
    
    public enum Action {  WAITING, WALKING, CLEANING }
    public Action JanitorStatus;
    private float _cleantime = 0;
    public NavMeshAgent _janitor;
    

    // Start is called before the first frame update
    void Start()
    {
        _janitor = GetComponent<NavMeshAgent>();
        JanitorStatus = Action.WAITING;
        //UpdateJanitorDestination();
        
    }

    private void UpdateJanitorDestination()
    {
        if (PuddleManager.Instance._listofpuddles.Count == 0)
        {
            //Debug.Log("debug1");
            return;
        }
        _janitor.SetDestination(PuddleManager.Instance._listofpuddles[0].transform.position);
        _janitor.isStopped = false;
    }
    // Update is called once per frame
    private void Update()

    {
        puddlecount = PuddleManager.Instance._listofpuddles.Count;

        /*if (JanitorStatus == Action.CLEANING)
        {
            Debug.Log("debug1debug1debug1");
            _cleantime += Time.deltaTime;
            if (_cleantime >= CleanTime)
            {
                Debug.Log("debug2debug2debug2");
                JanitorsDestination = null;
                JanitorStatus = Action.WAITING;
                
               // _janitor.SetDestination(PuddleManager.Instance._listofpuddles[0].transform.position);
            }
        }*/
        if (JanitorsDestination == null)
        {
            //Debug.Log("debug3");
            UpdateJanitorDestination();
            return;
        }
        if (Vector3.Distance(transform.position, JanitorsDestination.transform.position) < 3.0f)
        {
            StartClean();
        }
    }


    private void StartClean()
    {
        JanitorStatus = Action.CLEANING;
        _janitor.isStopped = true;
        JanitorsDestination = null;
    }
}
