using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleManager : MonoBehaviour
{
    public GameObject PuddlePrefab;
    public int ObstacleLimit;
    public static PuddleManager Instance { get; private set; }
    public List<GameObject> _listofpuddles = new List<GameObject>();
 
    // Update is called once per frame
    private void Update()
    {
       // Debug.Log("click1success");
        Vector3 position = Vector3.zero;
        if (ClickObject("Floor", ref position))
        {
           // Debug.Log("Click Floor");
           /*/
            if(_listofpuddles.Count >= ObstacleLimit)
            {
                GameObject go = _listofpuddles[0];
                _listofpuddles.RemoveAt(0);//removing old puddles if there are too many in the building
                Destroy(go);
            }/*/

            GameObject puddle = Instantiate(PuddlePrefab, position, Quaternion.identity); //adding our gameobject to scene
            _listofpuddles.Add(puddle);
            //Debug.Log(_listofpuddles.Count);
        }
        ClickRemovePuddle();
    }

    private bool ClickObject(string layer, ref Vector3 vec)
    {
        if (!Input.GetMouseButtonDown(0)) { return false; }
       // Debug.Log("debug2");
        Vector3 screenPoint = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(screenPoint);
        RaycastHit hit;
       // Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
       // Debug.Break();
        if (!Physics.Raycast(ray.origin, ray.direction, out hit)) return false;
       //Debug.Log("debugtest3");
        if (hit.transform.gameObject.layer != LayerMask.NameToLayer(layer)) return false;
       // Debug.Log("debugtest4");
        vec = hit.point;
        return true;
    }
    private void ClickRemovePuddle()
    {
        if (!Input.GetMouseButtonDown(1)) { return; }
        Vector3 screenPoint = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(screenPoint);
        RaycastHit hit;
        if (!Physics.Raycast(ray.origin, ray.direction, out hit)) return;
        Debug.Log("clicking");
        if (hit.transform.name.Replace("(Clone)","") != PuddlePrefab.name) return;
        Debug.Log("destroyclick");
        GameObject go = hit.transform.gameObject;
        if (_listofpuddles.Contains(go)) {_listofpuddles.Remove(go);}
            Destroy(go);
        }

    /* public virtual List<JanitorDestination> PuddleList()
     {
         return _listofpuddles[i];
     }*/
    private void Start() //Awake()
    {
        //Singleton Pattern
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}

