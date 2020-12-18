using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveCamera : MonoBehaviour
{

    public float lookSpeed0 = 2f;
    public float Speed0 = 8f;
    private float yaw = 0f;
    private float hence = 0f;
    
    void Update()
    {
        float lookSpeed = lookSpeed0;
        float Speed = Speed0;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            lookSpeed = lookSpeed0*2;
            Speed = Speed0*2;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            lookSpeed = lookSpeed0 / 2;
            Speed = Speed0 / 2;
        }
        //Look around with Right Mouse
        if (Input.GetMouseButton(1))
        {
            yaw += lookSpeed * Input.GetAxis("Mouse X");
            transform.eulerAngles = new Vector3(hence, yaw, 0f);
        }
        //Look Up & Down with Left Mouse
        if (Input.GetMouseButton(0))
        {
            hence += lookSpeed * Input.GetAxis("Mouse Y");
            transform.eulerAngles = new Vector3(hence, yaw, 0f);
        }

        if (Input.GetKey("a"))
        {
            transform.Translate(new Vector3(-Speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey("d"))
        {
            transform.Translate(new Vector3(Speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey("q"))
        {
            transform.Translate(new Vector3(0, Speed * Time.deltaTime, 0));
        }
        if (Input.GetKey("e"))
        {
            transform.Translate(new Vector3(0, -Speed * Time.deltaTime, 0));
        }
        if (Input.GetKey("w"))
        {
            transform.Translate(new Vector3(0, 0, Speed * Time.deltaTime));
        }
        if (Input.GetKey("s"))
        {
            transform.Translate(new Vector3(0, 0, -Speed * Time.deltaTime));
        }
    }
}