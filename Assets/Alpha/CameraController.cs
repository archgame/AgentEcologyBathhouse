using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 20f;
    //public float panBoarderThickness = 10f;
    public Vector3 panLimit;
    //public Camera[] _cameras;

  
    private void Update()
    {
        Vector3 position = transform.position;

        if (Input.GetKey("w"))
        {
            position.y += panSpeed * Time.deltaTime;
        }
        
        if (Input.GetKey("s"))
        {
            position.y -= panSpeed * Time.deltaTime;
        }
        
        if (Input.GetKey("d"))
        {
            position.x += panSpeed * Time.deltaTime;
        }
        
        if (Input.GetKey("a"))
        {
            position.x -= panSpeed * Time.deltaTime;
        }

        position.x = Mathf.Clamp(position.x, -panLimit.x, panLimit.x);
        position.y = Mathf.Clamp(position.y, -panLimit.y, panLimit.y);
        position.z = Mathf.Clamp(position.z, -panLimit.z, panLimit.z);

        //if (Input.GetKey("1"))
        //{
            //_cameras.shi(guest, Path[1].transform.position);
            
            //position.x -= panSpeed * Time.deltaTime;
        //}

        transform.position = position;
    }
}
