using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class iMovingCamera : MonoBehaviour
{

    public float lookSpeed = 2f;
    private float yaw = 0f;
    public float Speed = 5.0f;

    void Update()
    {
        //Look around with Right Mouse
        //if (Input.GetMouseButton(1))
        //{
            //yaw += lookSpeed * Input.GetAxis("Mouse X");
            //transform.eulerAngles = new Vector3(0f, yaw, 0f);
        //}
        
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