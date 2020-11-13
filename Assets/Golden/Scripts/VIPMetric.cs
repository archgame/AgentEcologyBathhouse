using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VIPMetric : MonoBehaviour
{
    [Header("Controls")]
    [Range(0, 30)]
    
    public float ScreenSlider = 0;

  
    public string ScreenText = "";

    //private float _vipguest;

    //private Material outsideAtriumColor
    //public List<Guest> guests = GuestManager.Instance.GuestList(); //this gives you a list of all the guests

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
        //List<Guest> guests = GuestManager.Instance.GuestList();
        //foreach (Guest guest in guests)
        // {
        //   if (guest.GetSliderValue() != GuestSlider)
        //       guest.SetSlider(GuestSlider);
        //   if (guest.GetText() != GuestText)
        //        guest.SetText(GuestText);
        // }

        List<Guest> guests = GuestManager.Instance.GuestList(); //this gives you a list of all the guests


        //int guestAtriumCount = 0;
        //foreach (Guest guest in guests)
        //{
        //    Renderer rend = guest.GetComponent<Renderer>();
        //    if (rend.material.color != Color.white) continue;
         //   guestAtriumCount++;
        //}

        //Text.text = guestAtriumCount.ToString();

        int guestCount = 0;
        foreach (Guest guest in guests)
        {
            Renderer rend = guest.GetComponent<Renderer>();
            if (rend.material.color != Color.red) continue;
            guestCount++;
        }
        
        Text.text = guestCount.ToString();
        Slider.value = guestCount;




        //int allguests = guests.Count;
        // foreach (Guest guest in guests)
        //{
        //    allguests++;
        // }

        //ScreenText = allguests.ToString();



        // float guestAtriumCount = 0;

        //foreach (Guest guest in guests)
        // {
        //   Renderer rend = guest.GetComponent<Renderer>();
        //   if (rend.material.color != Color.white) continue;
        //   guestAtriumCount++;
        // }

        // ScreenSlider = guestAtriumCount;





        //Update Screen UI
        //if (Slider == null) { Debug.Log("null Slider"); }
        //if (Slider.value != ScreenSlider)
        //   Slider.value = ScreenSlider;
        //if (Text.text != ScreenText)
        //   Text.text = ScreenText;
    }
}