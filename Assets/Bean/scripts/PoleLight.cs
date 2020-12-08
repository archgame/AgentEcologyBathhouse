using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleLight : MonoBehaviour
{
    public Material materialOrigin;
    public Material materialLight;

    private void Start()
    {

    }

    private void Update()
    {
        List<Guest> guests = GuestManager.Instance.GuestList(); //this gives you a list of all the guests

        
    }
}
