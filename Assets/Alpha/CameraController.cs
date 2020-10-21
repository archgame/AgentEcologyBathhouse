using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 20f; //the speed at which the camera will move
    public Vector3 panLimit; //assigning the pan limit (outer boudary)
    //public Camera[] _cameras;

  
    private void Update()
    {
        Vector3 position = transform.position; //x,y,z stored

        if (Input.GetKey("w")) // gets the keys from keyboard
        {
            position.y += panSpeed * Time.deltaTime; //transforming the position which will add onto the pan speed with the same speed
        }
        
        if (Input.GetKey("s"))
        {
            position.y -= panSpeed * Time.deltaTime; //'-' because to move in the opposite direction
        }
        
        if (Input.GetKey("d"))
        {
            position.x += panSpeed * Time.deltaTime;
        }
        
        if (Input.GetKey("a"))
        {
            position.x -= panSpeed * Time.deltaTime;
        }

        position.x = Mathf.Clamp(position.x, -panLimit.x, panLimit.x); // clamp to set the minimum and maximum values(- = minimum and + = maximum
        position.y = Mathf.Clamp(position.y, -panLimit.y, panLimit.y);
        position.z = Mathf.Clamp(position.z, -panLimit.z, panLimit.z); // may not be needed but might use when shifting between different cameras

        //if (Input.GetKey("1"))
        //{
            //_cameras.shi(guest, Path[1].transform.position);
            
            //position.x -= panSpeed * Time.deltaTime;
        //}

        transform.position = position; //current position is set to the new position
    }
}
