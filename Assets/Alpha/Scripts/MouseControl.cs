using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            MouseAdd();
        }
    }

    public void MouseAdd()
    {
          Vector3 screenPoint = Input.mousePosition; //mouse position on the screen
          Ray ray = Camera.main.ScreenPointToRay(screenPoint); //converting the mouse position to ray from mouse position
          RaycastHit hit;
          if (!Physics.Raycast(ray.origin, ray.direction * 1000, out hit)) return; //was something hit?
          Debug.Log(hit.transform.gameObject.name);
          if (!hit.transform.gameObject.GetComponentInParent<Conveyance>()) return; //was hit on the layer?
          //if a layer was hit, set the camera follow and lookat
          Conveyance   conveyance = hit.transform.gameObject.GetComponentInParent<Conveyance>();
          conveyance.Test += 1;
        if (conveyance.Test > 3) { conveyance.Test = 0; }

        


        //if IsActive is public, you can do this, conveyance.IsActive = !conveyance.IsActive;
          /*if (conveyance.IsConveyanceActive())
          {
              conveyance.Deactivate();
          }
          else
          {
              conveyance.Activate();
          } */
    }
}
