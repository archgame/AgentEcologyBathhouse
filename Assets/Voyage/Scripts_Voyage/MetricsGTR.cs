using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetricsGTR : MonoBehaviour
{
    public Material Main;
    public Material Alt;

    [Header("Controls")]
    [Range(0, 1)]
    public float ScreenSlider = 0;
    public string ScreenText = "";
    

    [Header("UI")]
    
    public Slider Slider;
    public Text GuestText;

   

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        //UpdateSlider
        int vehicleGuest = 0;
        
        List<Guest> guests = GuestManager.Instance.GuestList();

        foreach (Guest guest in guests)
        {
            //guest.GetComponent<Renderer>().material.color = new Color(0, 0, 255);//DEFULT(WALK) TO BLUE
            //Debug.Log(guest.name);
            //vehicleGuest++;

            /*if (guest._currentConveyance.GetType() == typeof(Vehicle))
            //?? if (guest._currentState == State.RIDING)
            {
                
                //guest.GetComponent<Renderer>().material.color = new Color(255, 0, 0);//VEHICLE GUEST RED 
            }
            */

            if (guest._currentConveyance.GetType() == typeof(Vehicle))
            {
                if (!guest.GetComponent<Guest>()) return;
                MeshRenderer mr = guest.GetComponent<MeshRenderer>();

                mr.material = Alt;

                vehicleGuest++;
            }
            else 
            {
                if (!guest.GetComponent<Guest>()) return;
                MeshRenderer mr = guest.GetComponent<MeshRenderer>();
                mr.material = Main;
            }

         }
        Slider.value = vehicleGuest / guests.Count;
        GuestText.text = vehicleGuest.ToString();

        //Slider.value = 1 - vehicleGuest / guests.Count;
        //float usingVehiclePercentage = vehicleGuest / guests.Count;
        //Slider.value = vehicleGuest / guests.Count;
        //Slider.value = vehicleGuest;

    }
}