using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RiskDisplay : MonoBehaviour
{
    public static RiskDisplay Instance { get; private set; }
    public int risk = 5;
    public Text riskText;
    public Dictionary<Guest, List<Guest>> guestEncounters = new Dictionary<Guest, List<Guest>>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        riskText.text = "ENCOUNTERS" + risk;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            risk--;
        }
    }

}  

