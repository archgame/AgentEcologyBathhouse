using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerCameraController : MonoBehaviour
{
    private Rigidbody m_Rigidbody;
    private Transform m_Transform;
    private float yaw = 0f;
    public float lookSpeed = 1f;
    public float speed = 1f;

    void Start()
    {
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
        m_Transform = gameObject.GetComponent<Transform>();

    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            yaw += lookSpeed * Input.GetAxis("Mouse X");


            transform.eulerAngles = new Vector3(0f, yaw, 0f);
        }

       
        if (Input.GetKey(KeyCode.W))
        {
            m_Rigidbody.MovePosition(m_Transform.position + Vector3.right * speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            m_Rigidbody.MovePosition(m_Transform.position + Vector3.left * speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            m_Rigidbody.MovePosition(m_Transform.position + Vector3.forward * speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            m_Rigidbody.MovePosition(m_Transform.position + Vector3.back * speed);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            m_Rigidbody.MovePosition(m_Transform.position + Vector3.up * speed);
        }
        if (Input.GetKey(KeyCode.E))
        {
            m_Rigidbody.MovePosition(m_Transform.position + Vector3.down * speed);
        }
       
    }

}
