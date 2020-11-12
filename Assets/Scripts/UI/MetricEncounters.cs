using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class MetricEncounters : MonoBehaviour
{
    public static Canvas Instance { get; private set; }
    public Text riskText;
    public Dictionary<Guest, List<Guest>> guestEncounters = new Dictionary<Guest, List<Guest>>();
    private int risk;


    // Start is called before the first frame update
    void Start()
    {
        riskText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        risk = GuestManager.Instance.risk;
        riskText.text = "ENCOUNTERS: " + risk;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            risk--;
        }
    }
}  

