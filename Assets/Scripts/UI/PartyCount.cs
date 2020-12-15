using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class PartyCount : MonoBehaviour
{
    public static Canvas Instance { get; private set; }
    public Text _text;
    public Dictionary<Guest, List<Guest>> guestEncounters = new Dictionary<Guest, List<Guest>>();
    public int partycount;



    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        partycount = GuestManager.Instance._partydests.Count;

        _text.text = "PARTY COUNT: " + partycount;
    }
}

