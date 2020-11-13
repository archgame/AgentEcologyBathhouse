using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotpotMetrics : MonoBehaviour
{
    public int Greennumber = 0;
    public int Yellownumber = 0;
    public Material Green;
    public Material Yellow;

    [Header("Controls")]
    [Range(0, 1)]
    public float ScreenSlider = 0;

    [Range(0, 1)]
    public float GuestSlider = 0;

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
        
        //UpdateGuestUI
        List<Guest> guests = GuestManager.Instance.GuestList();
        foreach (Guest guest in guests)
        {
            //Debug.Log("hello");
            if (guest.GetSliderValue() != GuestSlider)
                guest.SetSlider(GuestSlider);
            
            if (guest.GetText() != GuestText)
                guest.SetText(GuestText);
            
            

            if (guest.GetComponent<Renderer>().material == Yellow)
            
                Yellownumber++;
                Debug.Log("hello");
                Debug.Log(Yellownumber);

          //  if (guest.GetComponent<Renderer>().material = Green)
             //   Greennumber++;

            //Debug.Log(Greennumber);



            //Yellownumber++;
            //Debug.Log(Yellownumber);
        }

        ScreenSlider = Greennumber / guests.Count;

        Slider.value = Yellownumber;
        Text.text = Yellownumber.ToString();
        //Update Screen UI
        //if (Slider == null) { Debug.Log("null Slider"); }
       // if (Slider.value != ScreenSlider)
        // Slider.value = ScreenSlider;
        //if (Text.text != ScreenText)
          //Text.text = ScreenText;
    }
}