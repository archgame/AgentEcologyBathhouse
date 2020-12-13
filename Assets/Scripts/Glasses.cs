using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glasses : MonoBehaviour
{

    public Renderer glassglow;
    public float socialcol = 100;
    public bool glow = true;
    public Color colorhappy = new Color(255, 66, 128);

    // Start is called before the first frame update
    void Start()
    {
       glassglow = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (socialcol > 20)
        {
            glassglow.material.EnableKeyword("_EMISSION");
        }
        else
        {
            glassglow.material.DisableKeyword("_EMISSION");
        }
    }
}
