using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atrium : MonoBehaviour
{
    private int _collision;

    public void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Guest>()) return;
        //MeshRenderer mr = other.GetComponent<MeshRenderer>();
        //mr.material = Alt;

        _collision++;
        Debug.Log(_collision);
       

    }

    public void OnTriggerExit(Collider other)
    {
        if (!other.GetComponent<Guest>()) return;
        //MeshRenderer mr = other.GetComponent<MeshRenderer>();
        //mr.material = Main;
    }
    
}