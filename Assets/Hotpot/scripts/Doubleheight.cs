using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doubleheight : MonoBehaviour
{

    public Material Alt;
    public float guestnumber = 0;
    // public Action Status;
    // public enum Action { BATHING, WALKING, FOLLOWING, RIDING, RANDOM }
    

    public void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Guest>()) return;
        //Debug.Log("Guest in double height");


        Guest guest = other.GetComponent<Guest>();
        if (guest.Status == Guest.Action.BATHING) { other.GetComponent<Guest>().GuestWalkDestination(); }
        guestnumber++;
        //Debug.Log(guestnumber);
        MeshRenderer mr = other.GetComponent<MeshRenderer>();
        mr.material = Alt;
        guest.SetText("in double height!");
        //Debug.Log(" ");
        guest.SetSlider(1);
         
    }

    

    public void OnTriggerExit(Collider other)
    {
        if (!other.GetComponent<Guest>()) return;
        Guest guest = other.GetComponent<Guest>();
        guest.SetText("out of double height!");
    }
}
