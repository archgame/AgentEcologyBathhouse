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
    public int sickcount;
    public int totalcount;
    public float sickpercent;


    // Start is called before the first frame update
    void Start()
    {
        sickSlider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        sickcount = GuestManager.Instance.sickcount;
        totalcount = GuestManager.Instance.totalcount;
        if (totalcount == 0) 
            {sickpercent = 0;}
        else 
            {sickpercent = ((float)sickcount / (float)totalcount);}
        sickSlider.value = sickpercent;
    }

}
