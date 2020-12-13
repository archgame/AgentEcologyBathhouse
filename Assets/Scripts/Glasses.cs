using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glasses : MonoBehaviour
{

    public Renderer glassglow;
    public float socialcol = 100;
    private bool glow = true;
    public bool colortoggle = true;
    public Color colorhappy = new Color(255, 66, 128);
    public Guest parent;

    // Start is called before the first frame update
    void Start()
    {
        glassglow = GetComponent<Renderer>();
        parent = GetComponentInParent<Guest>();
    }

    // Update is called once per frame
    void Update()
    {
        if (parent.Status != Guest.Action.DANCING)
        {
            if (socialcol > 0)
            {
                glow = true;
            }
            else
            {
                glow = false;
            }
        }
        
        else
        {
            glow = GuestManager.Instance.GlassesToggle();
        }
                
        if (glow == true)
        {
            glassglow.material.EnableKeyword("_EMISSION");
        }
        else
        {
            glassglow.material.DisableKeyword("_EMISSION");
        }
    }
}
