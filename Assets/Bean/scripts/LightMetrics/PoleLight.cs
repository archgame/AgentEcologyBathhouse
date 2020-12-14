using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoleLight : MonoBehaviour
{
    public Material materialOrigin;
    public Material materialLight;
    public enum Action { Normal, Popular }
    public Action Status;
    
    private void Start()
    {
        Status = Action.Normal;
        MeshRenderer _material = GetComponent<MeshRenderer>();
        _material.material = materialOrigin;
    }


    // Update is called once per frame

    private void Update()
    {


        List<Guest> guests = GuestManager.Instance.GuestList(); //this gives you a list of all the guests


        int poleCount = 0;
        foreach (Guest guest in guests)
        {
            Renderer rend = guest.GetComponent<Renderer>();
            if (rend.material.color != Color.red) continue;
            poleCount++;
        }

        int liftCount = 0;
        foreach (Guest guest in guests)
        {
            Renderer rend = guest.GetComponent<Renderer>();
            if (rend.material.color != Color.blue) continue;
            liftCount++;
        }

        int railCount = 0;
        foreach (Guest guest in guests)
        {
            Renderer rend = guest.GetComponent<Renderer>();
            if (rend.material.color != Color.green) continue;
            railCount++;
        }

        if (poleCount > liftCount & poleCount > railCount)
        {
            LightOn();
        }
        else
        { 
            LightOff();
        }

    }

    private void LightOn()
    {
        Status = Action.Popular;
        MeshRenderer _material = GetComponent<MeshRenderer>();
        _material.material = materialLight;
    }

    private void LightOff()
    {
        Status = Action.Normal;
        MeshRenderer _material = GetComponent<MeshRenderer>();
        _material.material = materialOrigin;
    }
}
