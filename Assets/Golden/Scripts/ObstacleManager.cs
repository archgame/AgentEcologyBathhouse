using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject ObstaclePrefab;
    public int ObstacleLimit;

    private List<GameObject> _obstacles= new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        Vector3 position = Vector3.zero;
        if(ClickObject("Floor",ref position))
        {
            Debug.Log("Click Floor");

            if (_obstacles.Count >= ObstacleLimit)
            {
                GameObject go = _obstacles[0];
                _obstacles.RemoveAt(0);
                Destroy(go);
            }

            GameObject obstacle = Instantiate(ObstaclePrefab, position, Quaternion.identity); //adding our gameobject to scene
            _obstacles.Add(obstacle);
            return;
        }

        ClickRemoveObstacle();
    }

    private bool ClickObject(string layer, ref Vector3 vec)
    {
        if (!Input.GetMouseButtonDown(0)) { return false; }
        Debug.Log("1");
        Vector3 screenPoint = Input.mousePosition; //mouse position on the screen
        Debug.Log("2");
        Ray ray = Camera.main.ScreenPointToRay(screenPoint); //converting the mouse position to ray from mouse position
        Debug.Log("3");
        RaycastHit hit;
        Debug.Log("4");
        if (!Physics.Raycast(ray.origin, ray.direction*1000, out hit)) return false; //was something hit?
        Debug.Log("5 " + LayerMask.LayerToName(hit.transform.gameObject.layer));
        Debug.Log(hit.transform.gameObject.name);

        if (hit.transform.gameObject.layer != LayerMask.NameToLayer(layer)) return false; //was hit on the layer?
        Debug.Log("6");

        vec = hit.point;
        Debug.Log("7");
        return true;
        

    }

    private void ClickRemoveObstacle()
    {
        if (!Input.GetMouseButtonDown(0)) { return; }
       
        Vector3 screenPoint = Input.mousePosition; //mouse position on the screen
        Ray ray = Camera.main.ScreenPointToRay(screenPoint); //converting the mouse position to ray from mouse position
        RaycastHit hit;
        if (!Physics.Raycast(ray.origin, ray.direction * 1000, out hit)) return; //was something hit?
        if (hit.transform.name.Replace("(Clone)","") != ObstaclePrefab.name) return ; //was hit on the layer?

        GameObject go = hit.transform.gameObject;
        if (_obstacles.Contains(go)) { _obstacles.Remove(go); }
        Destroy(go);
    }
}
