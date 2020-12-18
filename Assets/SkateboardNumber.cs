using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkateboardNumber : MonoBehaviour
{
    [Header("Controls")]
    [Range(0, 200)]

    public int ScreenSlider = 0;
    public string ScreenText = "";
    public Material Alt;
    public Material Origin;
    public Material Jump;
    public Material Walk;

    [Header("UI")]
    public Text GuestText;
    public Slider Slider;
    
    private void Update()
    {
        List<Guest> guests = GuestManager.Instance.GuestList();
        Debug.Log(guests.Count);
        Add();
        //foreach (Guest guest in guests)
        //{
        //    //Renderer rend = guest.GetComponent<Renderer>();
        //    MeshRenderer mr = guest.GetComponent<MeshRenderer>();
        //    if (mr.material!=  Walk) continue;
        //    SkatingCount= SkatingCount+1;
        //}
     
    }

    private void Add()
    {
        List<Guest> guests = GuestManager.Instance.GuestList();
        int SkatingCount = 0;
        foreach (Guest guest in guests)
        {
            Renderer rend = guest.GetComponent<Renderer>();
            if (rend.material == Walk)
            {
                SkatingCount++;
            }
           
        }

        Slider.value = SkatingCount;
            GuestText.text = SkatingCount.ToString();
        
    }
}
