using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public float RotateY;
    public float RotateSpeed;
    public bool Clockwisetotation;
    public float A = 0;


    //public GameObject Target;
    
    // Start is called before the first frame update
    void Start()
    {
        //Target = GameObject.Find("A");
    }

    // Update is called once per frame
    private void Update()
    {
        /*{
            if (Clockwisetotation == false)
            {
                RotateY += Time.deltaTime * RotateSpeed;
            }

            else
            {
                RotateY += -Time.deltaTime * RotateSpeed;
            }

            transform.rotation = Quaternion.Euler(0, RotateY, 0);
    }*/
        //transform.Rotate(new Vector3(0, Time.deltaTime * 10, 0));
        //transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);


        if (Input.GetKey("r")) // gets the keys from keyboard
        {
            RotateChairlift();
            A += 1;
        }


        this.GetComponent<FinalChair>().enabled = true;
      
    }

    private void RotateChairlift()
    {
        this.GetComponent<FinalChair>().enabled = false;
        if (Clockwisetotation == false)
        {
            RotateY += Time.deltaTime * RotateSpeed;
        }

        else
        {
            RotateY += -Time.deltaTime * RotateSpeed;
        }

        transform.rotation = Quaternion.Euler(0, RotateY, 0);
    }
        

        /*if (Input.GetKey("UpArrow"))
            transform.Rotate(Vector3.up, -TurnSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.UpArrow))
            transform.Rotate(Vector3.up, TurnSpeed * Time.deltaTime);*/



    /*Vector3 TargetPosition = new Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z);
    transform.LookAt(TargetPosition);*/

}
