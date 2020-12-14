using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentColorChange : MonoBehaviour
{           
    public Material Main;
    public Material Alt;
    public Material Alt1;
    public Material Alt2;
    public Material Alt3;
    public Material Alt4;
    public float Score = 0;
    
    public void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Guest>()) return;
        Debug.Log("Guest Encounter");
        Score += 1;
        MeshRenderer mr = other.GetComponent<MeshRenderer>();
        if (Score == 3)
        {
            mr.material = Alt;
        }

        else if (Score == 6)
        {
            mr.material = Alt1;
        }

        else if (Score == 10)
        {
            mr.material = Alt2;
        }

        else if (Score == 15)
        {
            mr.material = Alt3;
        }

        else if (Score == 20)
        {
            mr.material = Alt4;
        }        
    }
}
