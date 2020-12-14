using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSeven : MonoBehaviour
{
    [Header("Controls")]
    [Range(0, 100)]

    public float ScreenSlider = 0;


    public string ScreenText = "";


    [Header("UI")]
    public Text Text;

    public Slider Slider;

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {


        List<Guest> guests = GuestManager.Instance.GuestList(); //this gives you a list of all the guests



        int guestCount = 0;
        foreach (Guest guest in guests)
        {
            float vector2 = guest.transform.position.y;
            if (vector2 < 60.0f) continue;
            if (vector2 > 70.0f) continue;
            guestCount++;
        }

        Text.text = guestCount.ToString();
        Slider.value = guestCount;

    }

}