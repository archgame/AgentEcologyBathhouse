using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfficiencyAlt : MonoBehaviour
{
    internal static object Instance;
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

        guest.SetText("Inside Atrium");
        guest.SetSlider(1);
    }
}

    