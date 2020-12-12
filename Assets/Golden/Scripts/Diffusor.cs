using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Diffusor : MonoBehaviour
{
    Collider m_Collider;



    void Start()
    {

    }
  

    void OnTriggerEnter(Collider other)
    {
        if(other  != GameObject.Find("Agent-Diffusor(Clone)"))

        {
            DrawSpheres();
           
        }

        
    }

   


    void DrawSpheres()
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = this.transform.position;
        sphere.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);


        m_Collider = sphere.GetComponent<Collider>();
        m_Collider.enabled = false;

        var cubeRenderer = sphere.GetComponent<Renderer>();

        //Call SetColor using the shader property name "_Color" and setting the color to red
        cubeRenderer.material.SetColor("_Color", Color.yellow);

    }
}
