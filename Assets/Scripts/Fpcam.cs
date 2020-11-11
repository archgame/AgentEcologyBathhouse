using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Fpcam : MonoBehaviour
{
    private Guest storyteller;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void FollowMe(Guest x)
    {
        if (storyteller == null)
        {
            storyteller = x;
            return;
        }
    }

    public void EndCamFollow(Guest x)
    {
        if (storyteller == x)
        {
            storyteller = null;
            return;
        }
    }

    public void CamOverride(Guest guest)
    {
        storyteller = guest;
        return;
    }

    // Update is called once per frame
    public void CamUpdate()
    {
        if (storyteller != null)
        {
            transform.position = storyteller.transform.position;
            transform.rotation = storyteller.transform.rotation;
            return;
        }
    }
}
