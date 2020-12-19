using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorChange2 : MonoBehaviour
{
    public Slider t;
    public Color W = new Color ((float)0.5647059, (float)0.6039216, 1, (float)0.0627451);
    public Color K = new Color((float)0.3137255, (float)0.5490196, (float)0.09019608, (float)0.7254902);
    public Color Aa = new Color((float)1, (float)0, (float)0.01 , (float)1);
    public Color Ab = new Color((float)1, (float)0, (float)0.02, (float)1);
    public Color Ac = new Color((float)1, (float)0, (float)0.03, (float)1);
    public Color Ad = new Color((float)1, (float)0, (float)0.04, (float)1);
    public Color Ae = new Color((float)1, (float)0, (float)0.05, (float)1);
    public Color Af = new Color((float)1, (float)0, (float)0.06, (float)1);
    public Color Ag = new Color((float)1, (float)0, (float)0.07, (float)1);

    void Update()
    {
        Color lerpedColor = W;
        int guestAtriumCount = 0;
        List<Guest> guests = GuestManager.Instance.GuestList();
        foreach (Guest guest in guests)
        {
            
            Renderer rend = guest.GetComponent<Renderer>();
            if (rend.material.color != Ac ) continue;
            guestAtriumCount++;
        }
        t.value = guestAtriumCount * (float)0.1;
        Renderer A = GetComponent<Renderer>();
        A.material.color = Color.Lerp(W, K, t.value);
    }
    }

