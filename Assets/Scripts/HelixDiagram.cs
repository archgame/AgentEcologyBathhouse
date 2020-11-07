using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixDiagram : MonoBehaviour
{
    public GameObject box; //prefab to create the points that constructs the helix
    public float height = 88;
    public float turn = 14;
    private List<GameObject> _path = new List<GameObject>();

    //private float _period = 0.05f;

    float DegreeIncerement = 15; //Degree between each point

    public int r = 2; //radius of the helix 
    public float c = 0.5f; //height constant of the helix
    float angleT; //angle to place the points of the helix

    void Start()
    {
        float PointCount = 24 * turn;
        float delheight = 88 / PointCount;

        for (int i = 0; i < PointCount; i += 1)
        {
            angleT = Mathf.Deg2Rad * i * DegreeIncerement;
            GameObject g = Instantiate(box, transform); //As a child of the game object create helix points
            //g.transform.localPosition = new Vector3(r * Mathf.Cos(angleT), c * angleT, -r * Mathf.Sin(angleT)); //y oriented helix  
            g.transform.localPosition = new Vector3(r * Mathf.Cos(angleT), delheight * i, -r * Mathf.Sin(angleT)); //y oriented helix
            _path.Add(g);
        }

        _path.Reverse();

        //_path.Insert(0,gameObject);


    }

    private SplineInterpolator SetupSpline(IEnumerable<GameObject> gos)
    {
        //converting gameobjects to a list of transforms
        List<Transform> transforms = new List<Transform>();
        foreach (GameObject go in gos)
        {
            transforms.Add(go.transform);
        }

        //setup spline
        SplineInterpolator interp = transform.GetComponent<SplineInterpolator>();
        interp.Reset();
        for (int c = 0; c < transforms.Count; c++)
        {
            interp.AddPoint(transforms[c].position, transforms[c].rotation, c, new Vector2(0, 1));
        }
        interp.StartInterpolation(null, false, eWrapMode.ONCE);
        return interp;
    }
}