using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class GuestHappiness : MonoBehaviour
{
    public static Canvas Instance { get; private set; }
    public Text _text;
    public int happyguests;
    public int totalguests;
    public float happypercent;


    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        happyguests = GetHappy();
        totalguests = GuestManager.Instance._guest.Count;
        if (totalguests == 0)
        { happypercent = 0; }
        else
        {
            happypercent = Mathf.Floor(100 * ((float)happyguests / ((float)totalguests)));
        }
        _text.text = "HAPPY GUESTS: " + happyguests + " (" + happypercent + "%)";
    }

    public int GetHappy()
    {
        int count = 0;
        for (int i = 0; i < GuestManager.Instance._guest.Count; i++)
        {
            if (GuestManager.Instance._guest[i].iamsad == false)
            {
                count += 1;
            }
        }
        return count;
    }
}

