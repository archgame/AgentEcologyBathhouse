using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToClick : MonoBehaviour
{
    public float Speed;
    public GameObject B;
    public GameObject C;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey("p"))
        {
            MovePaternoster();
        }

        if (Input.GetKey("o"))
        {
            MoveBack();
        }
    }

    private void MovePaternoster()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position,
                B.transform.position,
                Time.deltaTime * Speed * 8);



        //this.transform.position = new Vector3(transform.position.x + 0.0001f, transform.position.y, transform.position.z);


        //Vector3 a = Car.transform.position;

        /*Vector3 b = new Vector3(a.x + 2, a.y, a.z);
        Car.transform.position = Vector3.MoveTowards(a, b, Time.deltaTime * Speed);


        /*if (Input.GetKey("p")) // gets the keys from keyboard
          {
              transform.position = Vector3.MoveTowards(a, Vector3.Lerp(a, b, EndPosition), Speed);
          }*/


        /*Vector3 screenPoint = Input.mousePosition; //mouse position on the screen
          Ray ray = Camera.main.ScreenPointToRay(screenPoint); //converting the mouse position to ray from mouse position
          RaycastHit hit;
          if (!Physics.Raycast(ray.origin, ray.direction * 1000, out hit)) return; //was something hit?
          Debug.Log(hit.transform.gameObject.name);
          if (!hit.transform.gameObject.GetComponentInParent<Conveyance>()) return; //was hit on the layer?
          //if a layer was hit, set the camera follow and lookat
          Conveyance conveyance = hit.transform.gameObject.GetComponentInParent<Conveyance>();*/



        /*//if IsActive is public, you can do this, conveyance.IsActive = !conveyance.IsActive;
          if (conveyance.IsConveyanceActive())
          {
              conveyance.Deactivate();
          }
          else
          {
              conveyance.Activate();
          } */
    }

    private void MoveBack()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position,
                C.transform.position,
                Time.deltaTime * Speed * 8);
    }
}
