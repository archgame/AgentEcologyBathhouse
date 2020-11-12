using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiderColor : MonoBehaviour
{
    public Material Main;
    public Material Alt;


    public void OnTriggerEnter(Collider guest)
    {
        if (!guest.GetComponent<Guest>()) return;
        MeshRenderer mr = guest.GetComponent<MeshRenderer>();
        mr.material = Alt;
    }

    public void OnTriggerExit(Collider guest)
    {
        if (!guest.GetComponent<Guest>()) return;
        MeshRenderer mr = guest.GetComponent<MeshRenderer>();
        mr.material = Main;
    }
}
