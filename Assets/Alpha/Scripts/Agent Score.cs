using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AgentScore : MonoBehaviour
{
    public Material Main;
    public Material Alt;

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


    private void Update()
    {
        //UpdateGuestUI
        List<Guest> guests = GuestManager.Instance.GuestList();
        foreach (Guest guest in guests)
        {
            if (guest.GetSliderValue() != GuestSlider)
                guest.SetSlider(GuestSlider);
            if (guest.GetText() != GuestText)
                guest.SetText(GuestText);
        }

        //Update Screen UI
        if (Slider == null) { Debug.Log("null Slider"); }
        if (Slider.value != ScreenSlider)
            Slider.value = ScreenSlider;
        if (Text.text != ScreenText)
            Text.text = ScreenText;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Guest>()) return;
        Debug.Log("Guest Encounter");
        MeshRenderer mr = other.GetComponent<MeshRenderer>();
        mr.material = Alt;
        Guest guest = other.GetComponent<Guest>();
        if (guest.Status == Guest.Action.RANDOM) { other.GetComponent<Guest>().GuestWalkDestination(); }
        guest.SetText("Inside Atrium");
        guest.SetSlider(1);
    }

    public void OnTriggerExit(Collider other)
    {
        if (!other.GetComponent<Guest>()) return;
        MeshRenderer mr = other.GetComponent<MeshRenderer>();
        mr.material = Main;

        Guest guest = other.GetComponent<Guest>();
        guest.SetText("Outside Atrium");
        guest.SetSlider(0);
    }
}
