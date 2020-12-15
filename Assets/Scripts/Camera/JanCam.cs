using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanCam : MonoBehaviour
{
    private GameObject Janitor;

    // Start is called before the first frame update
    void Start()
    {
        Janitor = GameObject.Find("Janitor");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Janitor.transform.position + (0.75f * Vector3.up) + (1.5f * Janitor.transform.forward);
        transform.rotation = Janitor.transform.rotation;
        return;
    }
}
