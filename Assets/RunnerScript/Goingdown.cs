using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Goingdown : MonoBehaviour
{
       
    private List<Vector3> direction = new List<Vector3>();
    public Material Alt;
    public Material Origin;
    public Material Jump;
    public int jumpTime = 30;
    public int fallTime = 20;
    public float jumpHeight = 1f;
    private bool canJump=false;
    private int ObstacleLayer;
    private int FloorLayer;
    

    public void Start()
    {
        InvokeRepeating("GoingDown", 0f,0.1f);
    }
    public void FixedUpdate()
    {

        RaycastHit hitinfo;
        ObstacleLayer = LayerMask.GetMask("Obstacle");
        FloorLayer = LayerMask.GetMask("Floor");

        canJump = Physics.Raycast(transform.Find("Capsule").position, Vector3.forward, out hitinfo, 1f, ObstacleLayer) 
        && Physics.Raycast(transform.Find("Capsule").position, Vector3.down, out hitinfo, 0.6f, FloorLayer) 
        && transform.Find("Guest(Clone)");

        if (canJump)
        {
            Debug.Log("jump",gameObject);
            JumpForSeconds();
        }
               

    }


    public void GoingDown()
    {
        direction.Add(transform.position);

        int i = direction.Count;

        if (i < 2) return;
        Vector3 point1 = direction[i - 1];
        Vector3 point2 = direction[i - 2];

        float delta = point1.y - point2.y;

        if (transform.Find("Guest(Clone)")==null) return;
        Guest guest = transform.Find("Guest(Clone)").GetComponent<Guest>();
        MeshRenderer mr = transform.Find("Guest(Clone)").GetComponent<MeshRenderer>();

        if (delta < -0.05)
        {
            mr.material = Alt;
            guest.SetText("Speed Up!");
            guest.SetSlider(1);
        }
        else 
        {
            mr.material = Origin;
            guest.SetText("Let's roll!");
            guest.SetSlider(0.5f);
        }
        
         direction.RemoveAt(0);
           
    }

    void JumpForSeconds()
    {
          StartCoroutine(jumpForSeconds());
    }

    IEnumerator jumpForSeconds()
     {
       
         int time = jumpTime;
         float speed = jumpHeight / jumpTime;
         while(time>0)
         {
             time--;
            if (transform.Find("Guest(Clone)") )
            {
               transform.Find("Guest(Clone)").Translate(transform.up * speed);
               MeshRenderer mr = transform.Find("Guest(Clone)").GetComponent<MeshRenderer>();
               mr.material = Jump;
            }
             
            transform.Find("Capsule").Translate(transform.up * speed);
           
            yield return new WaitForEndOfFrame();
         }
         time = fallTime;
         speed = -jumpHeight / fallTime;
         while (time > 0)
         {
            time--;
            if (transform.Find("Guest(Clone)"))
            {
                transform.Find("Guest(Clone)").Translate(transform.up * speed);
            }
            transform.Find("Capsule").Translate(transform.up * speed);
            
            yield return new WaitForEndOfFrame();
         }
        
        MeshRenderer _mr = transform.Find("Guest(Clone)").GetComponent<MeshRenderer>();
        _mr.material = Origin;
        canJump = false;
     }

           
}
