using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Fpcam : MonoBehaviour
{
    private Guest storyteller;

    public static Fpcam Instance { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void FollowMe(Guest x)
    {
        Debug.Log("executed followme!!!");
        if (storyteller == null)
        {
            storyteller = x;
            return;
        }
    }

    public void EndCamFollow(Guest x)
    {
        Debug.Log("executed endcamfollow!!!");
        if (storyteller == x)
        {
            storyteller = null;
            return;
        }
    }

    public void CamOverride(Guest guest)
    {
        Debug.Log("executed camoverride!!!");
        storyteller = guest;
        return;
    }

    // Update is called once per frame
    public void CamUpdate()
    {
        Debug.Log("executed camupdate!!!");
        if (storyteller != null)
        {
            transform.position = storyteller.transform.position + (0.75f * Vector3.up) + (1.5f * storyteller.transform.forward);
            transform.rotation = storyteller.transform.rotation;
            return;
        }
    }
}
