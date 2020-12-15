using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiderColor : Guest
{
    public Material RailwayMaterial;
    public Material VehicleMaterial;
    public Material RampMaterial;
    public Material Main;
    private Material LastCM;


    /*public void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Guest>()) return;
        if (other.GetComponent<Vehicle>()) return;
        MeshRenderer mr = other.GetComponent<MeshRenderer>();
        mr.material = Alt;
    }

    public void OnTriggerExit(Collider other)
    {
        if (!other.GetComponent<Guest>()) return;
        if (other.GetComponent<Vehicle>()) return;
        MeshRenderer mr = other.GetComponent<MeshRenderer>();
        mr.material = Main;
    }*/

    public override void FindPath(ref Conveyance currentConveyance, ref List<Destination> destinations)
    {
        if (currentConveyance.tag == "Untagged")
        {
            LastCM = RampMaterial;           
        }

        else if (currentConveyance.tag == "Vehicle")
        {
            LastCM = VehicleMaterial;
        }

        else if (currentConveyance.tag == "Railway")
        {
            LastCM = RailwayMaterial;
        }
    }

    public void OnTriggerEnter(Collider destination)
    {
        //if (other.GetComponent<Guest>())
        destination.GetComponent<MeshRenderer>().material = LastCM;
        Debug.Log("OK");
    }

    public void OnTriggerExit(Collider destination)
    {
        destination.GetComponent<MeshRenderer>().material = Main;
    }
}
