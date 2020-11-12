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
    private float contaminatedNumber = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        contaminatedSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        contaminatedSlider.value = contaminatedNumber;
    }

}
