using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atriummetrics : MonoBehaviour
{

    public Material Alt;
    public float centercirculation = 0;
    // public Action Status;
    // public enum Action { BATHING, WALKING, FOLLOWING, RIDING, RANDOM }


    public void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Guest>()) return;
        //Debug.Log("Guest in atrium");


        Guest guest = other.GetComponent<Guest>();
       
        centercirculation++;
        Textchange.scoreValue++;

        //Debug.Log(centercirculation);

        MeshRenderer mr = other.GetComponent<MeshRenderer>();
        mr.material = Alt;
        guest.SetText("I am in atrium");
        //Debug.Log(" ");
        //guest.SetSlider();
        
    }



    public void OnTriggerExit(Collider other)
    {
        if (!other.GetComponent<Guest>()) return;
        Guest guest = other.GetComponent<Guest>();
        guest.SetText("I am out of atrium");
    }
}
