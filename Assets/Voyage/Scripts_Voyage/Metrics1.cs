using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetricsVoyage : MonoBehaviour
{
    [Header("Controls")]
    [Range(0, 1)]

    public float ScreenSlider = 0;

    public string ScreenText = "";

    //  public  class currentConveyance: Guest;

    [Header("UI")]
    public Text Text1;
    public Text Text2;
    public Text Text3;
    public Slider Slider;
    public Scrollbar scrollbar;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {

        int countVehicle = 0;
        int countRailway = 0;
        int countWalking = 0;
    }
}


  /*
if (currentConveyance.tag == "Vehicle")
{
    countVehicle++; 
}
 if (currentConveyance.tag == "Railway")
{
    countRailway++;
}
else
{
    countWalking++;
}

        /*if (currentConveyance == null)
        {
            LastCM = RampMaterial;
        }
        

        ScreenSlider = vehicleGuest / guests.Count;
        ScreenSlider = WalkingGuest / guests.Count;
       */
      
   





/*public class MetricsVoyage : MonoBehaviour
{
    [Header("Controls")]
    [Range(0, 1)]
    public float ScreenSlider = 0;

    [Range(0, 1)]
    public float GuestSlider = 0;
    public float GuestWalking = 0;
    public float GuestVehicle = 0;
    public float GuestRailway = 0;


    public string ScreenText = "";
    public string GuestText = "";

    [Header("UI")]
    public Text Text;
    public Slider Slider;
    public Scrollbar scrollbar;

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
       // foreach (Guest guest in guests)
        //{
        //    guest.GetComponent<Renderer>().sharedMaterial.color = new Color(0, 0, 255);//DEFULT(WALK) TO BLUE
         //   Debug.Log(guest.name);

           if (guest._currentConveyance.GetType() == typeof(Vehicle))
            //?? if (guest._currentState == State.RIDING)
        //    {
                vehicleGuest++;
       //         guest.GetComponent<Renderer>().sharedMaterial.color = new Color(255, 0, 0);//VEHICLE GUEST RED 
            }
       //
          if (guest.GetText() != GuestText)
                guest.SetText(GuestText);
     //   }
       // Debug.Log("BreakM1");

        //SET SCREENSLIDER COUNT
        //ScreenSlider = vehicleGuest / guests.Count;
        //GuestSlider = ScreenSlider;
        

          if (currentConveyance.tag == "Vehicle")

        foreach (Guest guest in guests)
        {
            //SET SLIDER COLOR=GUEST COLOR                     Color(255, 0, 0)
            if (guest._currentConveyance.GetType() == typeof(Vehicle))
            {
                if (guest.GetSliderValue() != GuestSlider)
                {
                    guest.SetSlider(GuestSlider);
                    guest.Slider.gameObject.transform.Find("Fill Area").Find("Fill").GetComponent<Image>().color = new Color(255, 127, 80);
                }
            }
            if  ()
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
                           */
