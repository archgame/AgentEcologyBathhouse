using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class CreatSkateboard : MonoBehaviour

{
    public GameObject skateboard;
    public int totalnumber;
    public float speed;
    public float delay;
    float time = 0;
        
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AddSkateboard", delay, 1/ speed);
        InvokeRepeating("AddTime", delay, 1 / speed);


    }
    private void Update()
    {
        Debug.Log(time);
        if (time >= totalnumber / speed)
        {
            CancelInvoke();
        }
    }

    void AddSkateboard()
    {
        GameObject.Instantiate(skateboard, this.transform.position, Quaternion.identity);
    }
    void AddTime()
    {
        time += 1 / speed;
    }

}
