using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Canvas : MonoBehaviour
{
    public static Canvas Instance { get; private set; }
    public Text riskText;
    public Dictionary<Guest, List<Guest>> guestEncounters = new Dictionary<Guest, List<Guest>>();


    // Start is called before the first frame update
    void Start()
    {
        _encountersObj = GetComponentInChildren<>;
    }

    // Update is called once per frame
    /*/void Update()
    {
        riskText.text = "ENCOUNTERS" + risk;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            risk--;
        }
    }/*/

}  

