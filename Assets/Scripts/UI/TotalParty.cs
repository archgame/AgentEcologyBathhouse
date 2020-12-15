using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class TotalParty : MonoBehaviour
{
    public static Canvas Instance { get; private set; }
    public Text _text;
    public int totalparties;


    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        totalparties = GuestManager.Instance.totalparties;

        _text.text = "TOTAL PARTIES: " + totalparties;
    }
}

