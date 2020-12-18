using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayControl : MonoBehaviour
{

    public GameObject cam_01;
    public GameObject cam_02;
    public GameObject cam_03;
    public GameObject cam_04;
    public Text cam_tex;
    Vector3 position1;
    Vector3 position2;
    Vector3 position3;
    Vector3 position4;
    Quaternion rotation2;
    Quaternion rotation3;



    void Start()
    {
        cam_01.SetActive(true);
        cam_02.SetActive(false);
        cam_03.SetActive(false);
        cam_04.SetActive(false);
        position1 = cam_01.transform.position;
        position2 = cam_02.transform.position;
        position3 = cam_03.transform.position;
        position4 = cam_04.transform.position;
        rotation2 = cam_02.transform.rotation;
        rotation3 = cam_03.transform.rotation;

        cam_tex.text = "Axonometric View";
    }

    
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            cam_01_active();
        }

        if (Input.GetKeyDown("2"))
        {
            cam_02_active();
        }

        if (Input.GetKeyDown("3"))
        {
            cam_03_active();
        }

        if (Input.GetKeyDown("4"))
        {
            cam_04_active();
        }

        if (Input.GetKeyDown("r"))
        {
            cam_01.transform.position = position1;
            cam_02.transform.position = position2;
            cam_03.transform.position = position3;
            cam_04.transform.position = position4;
            cam_02.transform.rotation = rotation2;
            cam_03.transform.rotation = rotation3;
        }
    }

    public void cam_01_active()
    {
        cam_01.SetActive(true);
        cam_02.SetActive(false);
        cam_03.SetActive(false);
        cam_04.SetActive(false);
        cam_tex.text = "Axonometric View";

    }

    public void cam_02_active()
    {
        cam_01.SetActive(false);
        cam_02.SetActive(true);
        cam_03.SetActive(false);
        cam_04.SetActive(false);
        cam_tex.text = "1st Person View";

    }

    public void cam_03_active()
    {
        cam_01.SetActive(false);
        cam_02.SetActive(false);
        cam_03.SetActive(true);
        cam_04.SetActive(false);
        cam_tex.text = "Aerial View";

    }

    public void cam_04_active()
    {
        cam_01.SetActive(false);
        cam_02.SetActive(false);
        cam_03.SetActive(false);
        cam_04.SetActive(true);
        cam_tex.text = "Whole View";

    }
}