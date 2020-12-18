using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaking : MonoBehaviour
{
    public Camera _camera;
    NavMeshSurface _surface;
    // Update is called once per frame
    void Update()
    {   
        _surface = transform.GetComponent<NavMeshSurface>();
        Vector3 position = Vector3.zero;
        if (ClickObject("Floor", ref position))
        {
            Debug.Log("NaveMeshChanged");
            ChangeNavMesh();
        }
    }
    private void ChangeNavMesh()
    {
        _surface.BuildNavMesh();
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
