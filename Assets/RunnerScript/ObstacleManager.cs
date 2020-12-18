using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObstacleManager : MonoBehaviour
{
    public GameObject ObstalcePrefab;
    public int ObstacleLimit;
    public Camera _camera;
    private List<GameObject> _obstacles = new List<GameObject>();
    public AudioSource _SkateboardAudio;
    private Vehicle _vehicle;
    private void Start()
    {
        _SkateboardAudio = gameObject.GetComponent<AudioSource>();
    }
    

    // Update is called once per frame
    private void Update()
    {
        Vector3 position = Vector3.zero;
        if (ClickObject("Floor", ref position))
        {
            //Debug.Log("Click Floor");
            //if we are at obstacle limit, remove oldest obstacle
            if (_obstacles.Count >= ObstacleLimit)
            {
                GameObject go = _obstacles[0];
                _obstacles.RemoveAt(0);
                Destroy(go);
            }

            //instantiate obstacle
            Vector3 vec3 =new Vector3 (0f, 1f, 0f);
            GameObject obstacle = Instantiate(ObstalcePrefab, position, Quaternion.AngleAxis(Random.Range(0f,180f), vec3)); //adding our gameobject to scene
            _obstacles.Add(obstacle);
            _SkateboardAudio.Play();
            //_vehicle= GetComponent<Vehicle>();
            //_vehicle.Weight--;
            return;
        }

        ClickRemoveObstacle();               
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

    private void ClickRemoveObstacle()
    {
        if (!Input.GetMouseButtonDown(1)) { return; }

        Vector3 screenPoint = Input.mousePosition; //mouse position on the screen
        Ray ray = _camera.ScreenPointToRay(screenPoint); //converting the mouse position to ray from mouse position
        RaycastHit hit;
        if (!Physics.Raycast(ray.origin, ray.direction, out hit)) return; //was something hit?
        if (hit.transform.name.Replace("(Clone)", "") != ObstalcePrefab.name) return; //was hit on the layer?

        GameObject go = hit.transform.gameObject;
        if (_obstacles.Contains(go)) { _obstacles.Remove(go); }
        Destroy(go);
    }

    
}