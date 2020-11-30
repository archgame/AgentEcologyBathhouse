using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class MetricSafeExits : MonoBehaviour
{
    public static Canvas Instance { get; private set; }
    public Text riskText;
    public Dictionary<Guest, List<Guest>> guestEncounters = new Dictionary<Guest, List<Guest>>();
    public int safeexitcount;
    public int exitcount;
    public float exitpercent;


    // Start is called before the first frame update
    void Start()
    {
        riskText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        safeexitcount = GuestManager.Instance.safeExit;
        exitcount = GuestManager.Instance.exitCount;
        if (exitcount == 0)
        { exitpercent = 0; }
        else
        { exitpercent = Mathf.Floor(100 * ((float)safeexitcount / (float)exitcount)); }
        riskText.text = "SAFE EXITS: " + safeexitcount + " (" + exitpercent + "%)";

        if(exitcount == 5)
        {
            //Debug.Log(exitpercent);
            //Debug.Break();
        }
    }
}

