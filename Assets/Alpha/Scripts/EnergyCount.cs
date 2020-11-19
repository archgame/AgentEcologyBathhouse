using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnergyCount : MonoBehaviour
{
    public GameObject Height;
    
    public Material Main;
    public Material Alt;
    public Material Alt1;
    public Material Alt2;
    public float Score = 1f;
    private List<Guest> In = new List<Guest>();

    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        MeshRenderer mr = this.GetComponent<MeshRenderer>();
        
        List<Guest> guests = GuestManager.Instance.GuestList();
        
        foreach (Guest guest in guests)
        {
            if (guest.Status == Guest.Action.BATHING)
            {
               
                Vector3 Floor = Height.transform.position;
                if (!In.Contains(guest)  && Mathf.Abs(guest.transform.position.y - Floor.y) <1f )
                {
                    In.Add(guest);
                }
                
            }

            //else {if In.Contains(guest) {In.Remove(guest);}


            Score = In.Count;

            if (Score == 2)
            {
                mr.material = Main;
            }

            else if (Score == 8)
            {
                mr.material = Alt;
            }

            else if (Score == 15)
            {
                mr.material = Alt1;
            }

            else if (Score == 20)
            {
                mr.material = Alt2;
            }



        }
    }





}
