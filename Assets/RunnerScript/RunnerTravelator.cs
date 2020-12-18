using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class RunnerTravelator : Conveyance
{
    public Camera _camera;
    private Dictionary<Guest, List<Vector3>> _guests = new Dictionary<Guest, List<Vector3>>();

    
    public void Update()
    {
        Vector3 position = Vector3.zero;
        if (ClickObject("Travelator", ref position))
        {
            List<GameObject> gos = new List<GameObject>();
            foreach (GameObject go in Path)
            {
                gos.Add(go);
            }
            Debug.Log("pressed");
            
            //change the position of start and end 
            Vector3 endPos = Path[gos.Count-1].transform.position;
            Vector3 startPos = Path[0].transform.position;
            Path[0].transform.position = endPos;
            Path[gos.Count - 1].transform.position = startPos;


            Debug.Log("changed");

        }
    }
    public override void ConveyanceUpdate(Guest guest)
    {
        if (!_guests.ContainsKey(guest))
        {
            List<Vector3> vecs = new List<Vector3>();
            
            foreach (GameObject go in Path)
            {
                vecs.Add(go.transform.position);
            }

           
            _guests.Add(guest, vecs);
        }

        guest.transform.position = Vector3.MoveTowards(
            guest.transform.position,
            _guests[guest][0],
            Time.deltaTime * Speed
            );

        if (Vector3.Distance(guest.transform.position, _guests[guest][0]) < 0.01)
        {
            _guests[guest].RemoveAt(0);

            if (_guests[guest].Count == 0)
            {
                _guests.Remove(guest);
                guest.NextDestination();
            }
        }
    }
    private bool ClickObject(string layer, ref Vector3 vec)
    {

        //Debug.Log("1");
        if (!Input.GetMouseButtonDown(0)) { return false; }
        //Debug.Log("2");
        Vector3 screenPoint = Input.mousePosition; //mouse position on the screen
        Ray ray = _camera.ScreenPointToRay(screenPoint); //converting the mouse position to ray from mouse position
        RaycastHit hit;
        if (!Physics.Raycast(ray.origin, ray.direction, out hit)) return false; //was something hit?
        if (hit.transform.gameObject.layer != LayerMask.NameToLayer(layer)) return false; //was hit on the layer?

        //if a layer was hit, set the camera follow and lookat
        vec = hit.point;
        return true;
    }
}
