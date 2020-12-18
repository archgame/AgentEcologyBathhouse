using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Diffusor : MonoBehaviour
{


   

    public void Start()
    {
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other  != GameObject.Find("vipguest(Clone)"))
        {
            BallMetric.Instance.DrawSpheres(this.gameObject);
        }    
    }
  
}
