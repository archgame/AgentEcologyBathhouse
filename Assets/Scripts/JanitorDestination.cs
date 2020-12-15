using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanitorDestination : MonoBehaviour
{
    public int OccupancyLimit = 1;
    public float DestroyTimer = 1;
    private int i;
    public GameObject Janitor;

    // Update is called once per frame
    public void Update()
    {
        /*if (!PuddleManager.Instance._listofpuddles.Contains(this.gameObject))
        {
            Destroy(this);
        }*/

        //Debug.Log("close");
        Janitor = GameObject.Find("Janitor");
        if (Vector3.Distance(transform.position, Janitor.transform.position) < 3.0f)

        {
            //Debug.Log("closer");
            DestroyTimer += Time.deltaTime; //_bathTime = _bathTime + Time.deltaTime
            if (DestroyTimer > 1.0f)
            {
                PuddleManager.Instance._listofpuddles.RemoveAt(0);
                Destroy(gameObject);
            }
        }
    }

}

