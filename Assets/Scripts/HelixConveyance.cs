using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

[RequireComponent(typeof(SplineInterpolator))]
public class HelixConveyance : Conveyance
{
    public GameObject[] StartPoints;
    public GameObject EndPoint;
    public float helixHeight = 88;
    public float helixTurn = 14;
    public float helixRadius = 4;
    public float Testing = 0;

    private List<List<Transform>> allPathPts = new List<List<Transform>>();
    private List<SplineInterpolator> splinePaths = new List<SplineInterpolator>();
    private List<Transform> overallPts = new List<Transform>();
    private List<Transform> _templist = new List<Transform>();
    
    private float DegreeIncerement = 15;
    private float angleT;
        
    private List<SplineInterpolator> _mSplineInterps;
    private Dictionary<Guest, float> _guests = new Dictionary<Guest, float>();
    private float _period = 0.05f;

    private float SplineLength(SplineInterpolator spline, float period)
    {
        float length = 0;
        Vector3 lastPosition = spline.GetHermiteAtTime(0);
        int nodeCount = spline.GetNodeCount();
        if (nodeCount % 2 != 0) { nodeCount--; } //test if the node count is even, if odd subtract one
        for (float t = _period; t <= nodeCount; t += _period)
        {
            Vector3 curretPosition = spline.GetHermiteAtTime(t);
            float distance = Vector3.Distance(lastPosition, curretPosition);
            length += distance;
            lastPosition = curretPosition;
        }
        return length;
    }

    private List<SplineInterpolator> SetupSpline(List<Transform> ctlpts, IEnumerable<GameObject> startpts)
    {
        //converting gameobjects to a list of transforms

        //get spline interpolator
        SplineInterpolator interp = transform.GetComponent<SplineInterpolator>();

        //make smaller splines per each start
        foreach (GameObject start in startpts)
        {
            _templist = ctlpts;
            for (int i = 0; i < ctlpts.Count; i += 1)
            {
                if (_templist[i].transform.position.y < start.transform.position.y)
                {
                    _templist[i] = null;
                }
            }

            _templist.RemoveAll(null);
            _templist.Insert(0, start.transform);
            _templist.Add(EndPoint.transform);
            allPathPts.Add(_templist);

            //setup spline
            interp.Reset();
            for (int c = 0; c < _templist.Count; c++)
            {
                interp.AddPoint(_templist[c].transform.position, _templist[c].transform.rotation, c, new Vector2(0, 1));
            }
            interp.StartInterpolation(null, false, eWrapMode.ONCE);
            splinePaths.Add(interp);
        }

        return splinePaths;
    }

    // Start is called before the first frame update
    private void Start()
    {
        SetDestination();
        
        overallPts = null;
        float PointCount = 24 * helixTurn;
        float delheight = 88 / PointCount;

        //creates helix empty object points
        for (int i = 0; i < PointCount; i += 1)
        {
            angleT = Mathf.Deg2Rad * i * DegreeIncerement;
            Transform t = transform;
            t.localPosition = new Vector3(helixRadius * Mathf.Cos(angleT), delheight * i, -helixRadius * Mathf.Sin(angleT));
            overallPts.Add(t);
        }

        //flips list and creates splines
        overallPts.Reverse();
        _mSplineInterps = SetupSpline(overallPts, StartPoints);
    }

    // Update is called once per frame
    public override void ConveyanceUpdate(Guest guest)
    {

        int lvl = 0;
        if (allPathPts[lvl].Count < 2) return;

        //add guest to dictionary
        if (!_guests.ContainsKey(guest))
        {
            _guests.Add(guest, 0);
            guest.transform.position = allPathPts[lvl][0].transform.position;
            return;
        }

        //move guest along
        _guests[guest] += Time.deltaTime * Speed;
        Vector3 position = _mSplineInterps[lvl].GetHermiteAtTime(_guests[guest]); //+++
        guest.transform.forward = position - guest.transform.position; //make sure guest is facing movement direction
        guest.transform.position = position;

        //once we reach end, remove the guest
        int nodeCount = _mSplineInterps[lvl].GetNodeCount(); //+++
        if (nodeCount % 2 != 0) { nodeCount--; } //+++
        if (_guests[guest] >= nodeCount)
        {
            _guests.Remove(guest);
            guest.NextDestination();
        }
    }
}