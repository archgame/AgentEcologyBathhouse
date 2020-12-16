using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaMetrics : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Controls")]
    [Range(0, 30)]

    public float ScreenSlider = 0;
    float time;
    public GameObject Level1;
    public GameObject Level2;
    public GameObject Level3;
    public GameObject Level4;
    public GameObject Level5;
    public GameObject Level6;
    public GameObject Level7;





    public string ScreenText = "";

    //private float _vipguest;

    //private Material outsideAtriumColor
    //public List<Guest> guests = GuestManager.Instance.GuestList(); //this gives you a list of all the guests

    [Header("UI")]
    public Text GuestText;

    public Slider Slider;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        float A = Level1.GetComponent<EnergyCount>().Score;
        float B = Level2.GetComponent<EnergyCount>().Score;
        float C = Level3.GetComponent<EnergyCount>().Score;
        float D = Level4.GetComponent<EnergyCount>().Score;
        float E = Level5.GetComponent<EnergyCount>().Score;
        float F = Level6.GetComponent<EnergyCount>().Score;
        float G = Level7.GetComponent<EnergyCount>().Score;

        float sum = A + B + C + D + E + F + G; 
        //EE = Level1;












        //float time_round = Mathf.Round(time * 10.0f) * 0.1f;

        //GuestText.text = ("Time Elapsed: " + (time_round) + " s");
        Slider.value = sum;
    }

    
 
}
