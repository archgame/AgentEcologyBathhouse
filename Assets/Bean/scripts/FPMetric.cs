using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPMetric : MonoBehaviour
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
            Renderer rend = guest.GetComponent<Renderer>();
            if (rend.material.color != Color.red) continue;
            guestCount++;
        }

        Text.text = guestCount.ToString();
        Slider.value = guestCount;

    }

}