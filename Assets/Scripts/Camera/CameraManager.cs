using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;

public class CameraManager : MonoBehaviour
{
    private GameObject fpCamObject;
    private CinemachineVirtualCamera fpCamVC;
    private Fpcam fpCamScript;
    private List<CinemachineVirtualCamera> _cameras;
    private int _currentIndex = 0;

    // Start is called before the first frame update
    private void Start()
    {
        fpCamObject = GameObject.FindGameObjectWithTag("First Person Camera");
        fpCamScript = fpCamObject.GetComponent<Fpcam>();
        fpCamVC = fpCamObject.GetComponent<CinemachineVirtualCamera>();

        _cameras = FindObjectsOfType<CinemachineVirtualCamera>().ToList();

        UpdateCamera(_cameras, _currentIndex);
        //fpCamVC.Priority = 9;
    }

    private void UpdateCamera(List<CinemachineVirtualCamera> cameras, int index)
    {
        //if (fpCamVC.Priority != 9) { fpCamVC.Priority = 9; }
        foreach (CinemachineVirtualCamera camera in cameras)
        {
            camera.Priority = 9;
        }
        cameras[index].Priority = 11;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_cameras.Count < 2) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _currentIndex--;
            if (_currentIndex < 0) { _currentIndex = _cameras.Count - 1; }
            UpdateCamera(_cameras, _currentIndex);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _currentIndex++;
            if (_currentIndex >= _cameras.Count) { _currentIndex = 0; }
            UpdateCamera(_cameras, _currentIndex);
        }

        if (Input.GetMouseButton(0))
        {
            SetFPCamera("Guest");
        }

        //when a guest leaves, the follow camera turns off
    }

    private void SetFPCamera(string layer)
    {
        Vector3 screenPoint = Input.mousePosition; //mouse position on the screen
        Ray ray = Camera.main.ScreenPointToRay(screenPoint); //converting the mouse position to ray from mouse position
        RaycastHit hit;
        if (!Physics.Raycast(ray.origin, ray.direction * 10000, out hit)) return; //was something hit?
        if (hit.transform.gameObject.layer != LayerMask.NameToLayer(layer)) return; //was hit on the layer?
        Guest guest = hit.transform.gameObject.GetComponent<Guest>();
        //if a layer was hit, set the camera follow and lookat
        fpCamScript.CamOverride(guest);
        foreach (CinemachineVirtualCamera s in _cameras)
        {
            s.Priority = 9;
        }
        fpCamVC.Priority = 11;
        return;
    }
}