﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public Material Main;
    public Material Alt;

    public void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Guest>()) return;
        Debug.Log("Guest Encounter");
        MeshRenderer mr = other.GetComponent<MeshRenderer>();
        mr.material = Alt;
        Guest guest = other.GetComponent<Guest>();
        if (guest.Status == Guest.Action.RANDOM) { other.GetComponent<Guest>().GuestWalkDestination(); }
        guest.SetText("I'm Skateboarding");
        guest.SetSlider(1);
    }

    /*public void OnTriggerExit(Collider other)
    {
        if (!other.GetComponent<Guest>()) return;
        MeshRenderer mr = other.GetComponent<MeshRenderer>();
        mr.material = Main;

        Guest guest = other.GetComponent<Guest>();
        guest.SetText("Just Walking");
        guest.SetSlider(0);
    }*/
}
