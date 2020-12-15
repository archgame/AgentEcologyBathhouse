using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyanceInteract : MonoBehaviour
{
    // Start is called before the first frame update

    //public Material ActiveMaterial;
    //public Material InactiveMaterial;

    public Material ActiveHelixCenterMaterial;
    public Material InactiveMaterial;
    public Material ActiveHelixLMaterial;
    public Material ActiveHelixMMaterial;
    public Material ActiveHelixSMaterial;

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ToggleConveyanceActivation();
        }
    }

    private void ToggleConveyanceActivation()
    {
        Debug.Log("Check0");
        Vector3 screenPoint = Input.mousePosition; //mouse position on the screen
        Ray ray = Camera.main.ScreenPointToRay(screenPoint); //converting the mouse position to ray from mouse position
        RaycastHit hit;
        if (!Physics.Raycast(ray.origin, ray.direction * 1000, out hit)) return; //was something hit?
        Debug.Log(hit.transform.gameObject.name);
        if (!hit.transform.gameObject.GetComponentInParent<Conveyance>()) return; //was hit on the layer?

        //if a layer was hit, set the camera follow and lookat
        Conveyance conveyance = hit.transform.gameObject.GetComponentInParent<Conveyance>();
        //if IsActive is public, you can do this, conveyance.IsActive = !conveyance.IsActive;
        conveyance.IsActive = !conveyance.IsActive;
        Debug.Log("Check1");
        if (conveyance.IsActive)
        {
            Debug.Log("Check2");
            if (conveyance.name == "Helix_Center")
            {
                //conveyance.GetComponent<MeshRenderer>().material = ActiveMaterial;   //Helix Center Material
                conveyance.GetComponent<MeshRenderer>().material = ActiveHelixCenterMaterial;
            }
            if (conveyance.name == "Helix_L")
            {
                //conveyance.GetComponent<MeshRenderer>().material = ActiveMaterial;   //Helix Center Material
                conveyance.GetComponent<MeshRenderer>().material = ActiveHelixLMaterial;   
            }
            if (conveyance.name == "Helix_M")
            {
                //conveyance.GetComponent<MeshRenderer>().material = ActiveMaterial;   //Helix Center Material
                conveyance.GetComponent<MeshRenderer>().material = ActiveHelixMMaterial;
            }
            if (conveyance.name == "Helix_S")
            {
                //conveyance.GetComponent<MeshRenderer>().material = ActiveMaterial;   //Helix Center Material
                conveyance.GetComponent<MeshRenderer>().material = ActiveHelixSMaterial;
            }
        }
        else
        {
            Debug.Log("Check3");
            //conveyance.GetComponent<MeshRenderer>().material = InactiveMaterial;
            conveyance.GetComponent<MeshRenderer>().material = InactiveMaterial;
        }
    }
}