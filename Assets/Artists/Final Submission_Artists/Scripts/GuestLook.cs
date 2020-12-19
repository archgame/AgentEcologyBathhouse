using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuestLook : MonoBehaviour
{
    public GameObject GuestPrefeb;
    public GameObject ScreenPrefeb;
    public Slider s;

    void Update()
    {
        int s = 0;
        if (Physics.Linecast(GuestPrefeb.transform.position, ScreenPrefeb.transform .position))
        {
            Debug.Log("blocked");
        } 
        else
        s++;
    }
  
}
