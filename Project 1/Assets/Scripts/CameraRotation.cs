using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public float speed;
    public float angularSpeed;

    private Rigidbody rbody;
    private float startX;

    private void Start()
    {
        startX = transform.position.x;

        rbody = GetComponentInChildren<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float mouseAngle = Input.GetAxis("Mouse X") * angularSpeed;
        float mouseMovement = Input.GetAxis("Mouse Y") * speed;

        rbody.AddForce(Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized * mouseMovement, ForceMode.Impulse);
        //rbody.AddTorque(transform.right * mouseAngle);
        transform.Rotate(Vector3.up, mouseAngle, Space.World);
        rbody.AddForce((new Vector3(startX, transform.position.y, transform.position.z) - transform.position).normalized * speed);
    }
}
