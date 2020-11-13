using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text2 : MonoBehaviour
{
    public static int scoreValue2 = 0;

    Text score2;


    void Start()
    {
        score2 = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        score2.text = "Double height number: " + scoreValue2;
    }
}
 