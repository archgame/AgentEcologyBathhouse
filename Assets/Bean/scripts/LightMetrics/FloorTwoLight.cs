using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTwoLight : MonoBehaviour
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


        int floorOneCount = 0;
        foreach (Guest guest in guests)
        {
            float vector = guest.transform.position.y;
            if (vector > 10.0f) continue;
            floorOneCount++;
        }

        int floorTwoCount = 0;
        foreach (Guest guest in guests)
        {
            float vector2 = guest.transform.position.y;
            if (vector2 < 10.0f) continue;
            if (vector2 > 20.0f) continue;
            floorTwoCount++;
        }

        int floorThreeCount = 0;
        foreach (Guest guest in guests)
        {
            float vector3 = guest.transform.position.y;
            if (vector3 < 20.0f) continue;
            if (vector3 > 30.0f) continue;
            floorThreeCount++;
        }

        int floorFourCount = 0;
        foreach (Guest guest in guests)
        {
            float vector4 = guest.transform.position.y;
            if (vector4 < 30.0f) continue;
            if (vector4 > 40.0f) continue;
            floorFourCount++;
        }

        int floorFiveCount = 0;
        foreach (Guest guest in guests)
        {
            float vector5 = guest.transform.position.y;
            if (vector5 < 40.0f) continue;
            if (vector5 > 50.0f) continue;
            floorFiveCount++;
        }

        int floorSixCount = 0;
        foreach (Guest guest in guests)
        {
            float vector6 = guest.transform.position.y;
            if (vector6 < 50.0f) continue;
            if (vector6 > 60.0f) continue;
            floorSixCount++;
        }

        int floorSevenCount = 0;
        foreach (Guest guest in guests)
        {
            float vector7 = guest.transform.position.y;
            if (vector7 < 60.0f) continue;
            if (vector7 > 70.0f) continue;
            floorSevenCount++;
        }

        int floorEightCount = 0;
        foreach (Guest guest in guests)
        {
            float vector8 = guest.transform.position.y;
            if (vector8 < 70.0f) continue;
            floorEightCount++;
        }

        if (floorTwoCount > floorOneCount & floorTwoCount > floorThreeCount & floorTwoCount > floorFourCount & floorTwoCount > floorFiveCount & floorTwoCount > floorSixCount & floorTwoCount > floorSevenCount & floorTwoCount > floorEightCount)
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
