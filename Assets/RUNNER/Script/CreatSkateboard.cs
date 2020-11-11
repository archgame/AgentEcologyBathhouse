using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CreatSkateboard : MonoBehaviour

{
    public GameObject skateboard;
    public Vector3 position;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AddSkateboard", 0f, 2f);
        

    }
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CancelInvoke();
        }
    }
    
    void AddSkateboard()
    {
        GameObject.Instantiate(skateboard, position, Quaternion.identity);
    }
    
}
    