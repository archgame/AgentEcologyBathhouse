using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class SickSliderMetric : MonoBehaviour
{
    public static Canvas Instance { get; private set; }
    public Slider sickSlider;
    public Dictionary<Guest, List<Guest>> guestEncounters = new Dictionary<Guest, List<Guest>>();
    private float sickNumber = 0f;


    // Start is called before the first frame update
    void Start()
    {
        sickSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        sickSlider.value = sickNumber;
    }

}
