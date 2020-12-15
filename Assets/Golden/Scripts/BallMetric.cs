using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallMetric : MonoBehaviour
{
    public static BallMetric Instance { get; internal set; }

    private void Awake()
    {
        //Singleton Pattern
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    List<GameObject> spheres = new List<GameObject>();

    [Header("Controls")]
    [Range(0, 1000)]

    public float ScreenSlider = 0;


    public string ScreenText = "";

    //private float _vipguest;

    //private Material outsideAtriumColor
    public List<Guest> guests = GuestManager.Instance.GuestList(); //this gives you a list of all the guests

    [Header("UI")]
    public Text Text;

    public Slider Slider;


    // Start is called before the first frame update
    private void Start()
    {

    }

    public void DrawSpheres(GameObject go)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = go.transform.position;
        sphere.transform.localScale = new Vector3(2f, 2f, 2f);
        //GameObject game = sphere.GetComponent<GameObject>();
        spheres.Add(sphere); //adding the destination script to the list
        Collider m_Collider = sphere.GetComponent<Collider>();
        m_Collider.enabled = false;
        var cubeRenderer = sphere.GetComponent<Renderer>();
        //Call SetColor using the shader property name "_Color" and setting the color to red
        cubeRenderer.material.SetColor("_Color", Color.magenta);
    }

    // Update is called once per frame
    private void Update()
    {
       //List<Guest> guests = GuestManager.Instance.GuestList(); //this gives you a list of all the guests
       
         List<GameObject> sphere = spheres; //this gives you a list of all the guests

      
        int BallCount = 0;
        foreach (GameObject go in sphere)
        {
            Renderer rend = go.GetComponent<Renderer>();
            if (rend.material.color != Color.magenta) continue;

            BallCount++;
        }

        Text.text = BallCount.ToString();
        Slider.value = BallCount;
    }
}