using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoreColor1 : MonoBehaviour
{
    public Slider t;
    public Color Z = new Color ((float)1, (float)1, 1, (float)0.08627451);
    public Color J = new Color((float)0, (float)0, (float)0, (float)0.6);
    public Color L = new Color((float)0, (float)0, (float)0.99, (float)1);


    void Update()
    {
        Color lerpedColor = Z;
        int guestAtriumCount = 0;
        List<Guest> guests = GuestManager.Instance.GuestList();
        foreach (Guest guest in guests)
        {
            
            Renderer rend = guest.GetComponent<Renderer>();
            if (rend.material.color != L ) continue;
            guestAtriumCount++;
        }
        t.value = guestAtriumCount * (float)0.05;
        Renderer A = GetComponent<Renderer>();
        A.material.color = Color.Lerp(Z, J, t.value);
    }
    }

