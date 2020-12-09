using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BathInteract : MonoBehaviour
{
    public GameObject BathPrefab;
    //public int ObstacleLimit;

    private List<GameObject> _baths = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        Vector3 position = Vector3.zero;
        if (ClickObject("Bath", ref position))
        {
            Debug.Log("Click Bath");

           // if (_obstacles.Count >= ObstacleLimit)
           // {
            //    GameObject go = _obstacles[0];
             //   _obstacles.RemoveAt(0);
             //   Destroy(go);
            //}

           // GameObject obstacle = Instantiate(ObstaclePrefab, position, Quaternion.identity); //adding our gameobject to scene
           // _obstacles.Add(obstacle);
            //return;
        }

       // ClickRemoveObstacle();
    }

    private bool ClickObject(string layer, ref Vector3 vec)
    {
        if (!Input.GetMouseButtonDown(0)) { return false; }

        Vector3 screenPoint = Input.mousePosition; //mouse position on the screen
        Ray ray = Camera.main.ScreenPointToRay(screenPoint); //converting the mouse position to ray from mouse position
        RaycastHit hit;
        if (!Physics.Raycast(ray.origin, ray.direction * 1000, out hit)) return false; //was something hit?
        if (hit.transform.gameObject.tag!= "Bath") return false; //was hit on the layer?
        
        GameObject go = hit.transform.gameObject;

        go.transform.tag.Replace("Bath", "BathV");
        
        Renderer rend = go.GetComponent<Renderer>();
        //rend.material.SetColor = Color.green;
        //vec = hit.point;
        return true;


    }

   // private void ClickRemoveObstacle()
   // {
   //     if (!Input.GetMouseButtonDown(0)) { return; }

    //    Vector3 screenPoint = Input.mousePosition; //mouse position on the screen
    //    Ray ray = Camera.main.ScreenPointToRay(screenPoint); //converting the mouse position to ray from mouse position
     //   RaycastHit hit;
     //   if (!Physics.Raycast(ray.origin, ray.direction * 1000, out hit)) return; //was something hit?
     //   if (hit.transform.name.Replace("(Clone)", "") != BathPrefab.name) return; //was hit on the layer?

     //   GameObject go = hit.transform.gameObject;
      //  if (_baths.Contains(go)) { _baths.Remove(go); }
      //  Destroy(go);
   // }
}
