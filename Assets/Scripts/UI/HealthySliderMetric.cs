using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class HealthySliderMetric : MonoBehaviour
{
    public static Canvas Instance { get; private set; }
    public Slider healthySlider;
    public Dictionary<Guest, List<Guest>> guestEncounters = new Dictionary<Guest, List<Guest>>();
    public int healthycount;
    public int totalcount;
    public float healthypercent;


    // Start is called before the first frame update
    void Start()
    {
        healthySlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        healthycount = GuestManager.Instance.healthycount;
        totalcount = GuestManager.Instance.totalcount;
        if (totalcount == 0)
        { healthypercent = 0; }
        else
        { healthypercent = ((float)healthycount / (float)totalcount); }
        healthySlider.value = healthypercent;
    }

}