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
    private float healthyNumber = 1f;


    // Start is called before the first frame update
    void Start()
    {
        healthySlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
       healthySlider.value = healthyNumber;
    }

}