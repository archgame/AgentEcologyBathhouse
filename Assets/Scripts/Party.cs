using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party : MonoBehaviour
{
    public Renderer partyglow;
    private bool glow;
    public Destination partydest;
    float livetime = 3f;

    // Start is called before the first frame update
    void Start()
    {
        partyglow = GetComponent<Renderer>();
        partydest = GetComponent<Destination>();
        GuestManager.Instance._partydests.Add(partydest);
    }

    // Update is called once per frame
    void Update()
    {
        glow = GuestManager.Instance.GlassesToggle();

        if (glow == true)
        {
            partyglow.material.EnableKeyword("_EMISSION");
        }
        else
        {
            partyglow.material.DisableKeyword("_EMISSION");
        }

        partydest.DanceCheck();

        if (partydest.IsEmpty()) { livetime -= Time.deltaTime; }
        else { livetime = 3f; }
        if (livetime <= 0)
        {
            GuestManager.Instance._partydests.Remove(partydest);
            Destroy(this.gameObject);
        }
    }
}
