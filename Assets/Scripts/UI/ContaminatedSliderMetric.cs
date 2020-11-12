using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class ContaminatedSliderMetric : MonoBehaviour
{
    public static Canvas Instance { get; private set; }
    public Slider contaminatedSlider;
    public Dictionary<Guest, List<Guest>> guestEncounters = new Dictionary<Guest, List<Guest>>();
    public int contamcount;
    public int totalcount;
    public float contampercent;


    // Start is called before the first frame update
    void Start()
    {
        contaminatedSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        contamcount = GuestManager.Instance.contamcount;
        totalcount = GuestManager.Instance.totalcount;
        if (totalcount == 0)
        { contampercent = 0; }
        else
        { contampercent = ((float)contamcount / (float)totalcount); }
        contaminatedSlider.value = contampercent;
    }

}
