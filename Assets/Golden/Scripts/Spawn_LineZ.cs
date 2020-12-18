using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_LineZ : MonoBehaviour
{    
    private Vector3 CenterZ;
    private Vector3 SizeZ;

    public GameObject[] Spawned;

    //public Quaternion min;

        
    // Start is called before the first frame update
    void Start()
    {
        SizeZ = new Vector3(0, 0, (Random.Range(10, 20)));
      //CenterA = transform.position;        
    }


    // Update is called once per frame
    void Update()
    {        
        Vector3 mouse = Input.mousePosition;
        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
        {
            CenterZ = hit.point;
        }

        if (Input.GetKey(KeyCode.Z))
        {
            SpawnObjecttypeZ(); 
        }
    }


    public void SpawnObjecttypeZ()
    {
        Vector3 posA = CenterZ + new Vector3(Random.Range(-SizeZ.x / 2 , SizeZ.x / 2), Random.Range(-SizeZ.y / 2 , SizeZ.y / 2), Random.Range(-SizeZ.z / 2, SizeZ.z /2));    
        Instantiate(Spawned[Random.Range(0,Spawned.Length)], posA, Quaternion.identity);                
    }    


    public void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0, 1, 1, 0.75F);
        Gizmos.DrawCube(transform.position, SizeZ);       
    }
}
