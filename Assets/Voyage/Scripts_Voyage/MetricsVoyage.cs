using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetricsVoyage : MonoBehaviour
{
    [Header("Controls")]
    [Range(0, 1)]
    public float ScreenSlider = 0;

    [Range(0, 1)]
    public float GuestSlider = 0;
    public float GuestWalking = 0;

    public string ScreenText = "";
    public string GuestText = "";

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
        int vehicleGuest = 0;
        //UpdateGuestUI
        List<Guest> guests = GuestManager.Instance.GuestList();
        foreach (Guest guest in guests)
        {
            guest.GetComponent<Renderer>().material.color = new Color(0, 0, 255);//DEFULT(WALK) TO BLUE

            if (guest._currentConveyance.GetType() == typeof(Vehicle))
            //?? if (guest._currentState == State.RIDING)
            {
                vehicleGuest++;
                guest.GetComponent<Renderer>().material.color = new Color(255, 0, 0);//VEHICLE GUEST RED 
            }

            if (guest.GetText() != GuestText)
                guest.SetText(GuestText);
        }

        //SET SCREENSLIDER COUNT
        ScreenSlider = vehicleGuest / guests.Count;
        GuestSlider = ScreenSlider;
        GuestWalking = 1 - GuestSlider;

        foreach (Guest guest in guests)
        {
            //SET SLIDER COLOR=GUEST COLOR
            if (guest._currentConveyance.GetType() == typeof(Vehicle))
            {
                if (guest.GetSliderValue() != GuestSlider)
                {
                    guest.SetSlider(GuestSlider);
                    guest.Slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = new Color(255, 0, 0);
                }
            }
            else
            {
                if (guest.GetSliderValue() != GuestWalking)
                {
                    guest.SetSlider(GuestWalking);
                    guest.Slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = new Color(0, 0, 255);
                }
            }
        }

        //Update Screen UI
        if (Slider == null) { Debug.Log("null Slider"); }

        if (Slider.value != ScreenSlider)
        { Slider.value = ScreenSlider; }
        if (Text.text != ScreenText)
        { Text.text = ScreenText; }
    }
}
