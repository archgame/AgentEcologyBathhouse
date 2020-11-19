using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentColorChange : MonoBehaviour
{
           
    public Material Main;
    public Material Alt;
    public Material Alt1;
    public Material Alt2;
    public float Score = 0;
    

    public void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Guest>()) return;
        Debug.Log("Guest Encounter");
        Score += 1;
        MeshRenderer mr = other.GetComponent<MeshRenderer>();
        if (Score < 2)
        {
            mr.material = Alt;
        }

        else if (Score < 50)
        {
            mr.material = Alt1;
        }

        else if (Score < 75)
        {
            mr.material = Alt2;
        }

        /*Guest guest = other.GetComponent<Guest>();
        if (guest.Status == Guest.Action.RANDOM) { other.GetComponent<Guest>().GuestWalkDestination(); }
        guest.SetText("Inside Atrium");
        guest.SetSlider(1);*/
    }



}
