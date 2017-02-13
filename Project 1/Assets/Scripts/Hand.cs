using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Hand : MonoBehaviour
{
    public float horizontalSpeed = 5;
    public float verticalSpeed = 10;
    public float frictionForce = 5;
    public float maxHeight = 5;
    public float rotationSensitivity = 5.0f; //change to increase mouse sensitivity

    Rigidbody body;

    private void Start()
    {
        // Hides the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Clamps the hand's position between the min and max height (This is done in update intentionally so the clamp actually works)
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 0, maxHeight), transform.position.z);
    }

    private void FixedUpdate()
    {
        // Move vertically when holding LMB, handle rotation when holding RMB, or move horizontally otherwise
        if(Input.GetButton("Fire1"))
        {
            HandleVerticalMovement();
        }
        else if(Input.GetButton("Fire2"))
        {
            HandleRotation();
        }
        else
        {
            HandleHorizontalMovement();
        }

        // Adds a friction force to decelerate the hand
        body.AddForce(-body.velocity.normalized * frictionForce);
    }

    //void FixedUpdate()
    //{
    //    float hMouseDelta = Input.GetAxis("Mouse X");
    //    float vMouseDelta = Input.GetAxis("Mouse Y");

    //    Vector3 dir = (targetPosition - transform.position).normalized;
    //    //float distance = (transform.position - targetPosition).magnitude;

    //    Vector3 speed = dir;
    //    speed.x *= horizontalSpeed;
    //    speed.y *= verticalSpeed;
    //    speed.z *= horizontalSpeed;

    //    body.velocity = speed * Time.fixedDeltaTime;

    //}

    private void HandleHorizontalMovement()
    {
        body.velocity += new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y")).normalized * horizontalSpeed * Time.fixedDeltaTime; 
    }

    private void HandleRotation()
    {
        //scale rotations based on screen width and screen height
        float wScale = 1.0f / Screen.width * 360 * rotationSensitivity;
        float hScale = 1.0f / Screen.height * 360 * rotationSensitivity;

        //get mouse movements from last frame
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        //calculate rotaion angles around X and Y axises
        float xRot = mouseDelta.y * wScale;
        float yRot = mouseDelta.x * hScale;

        //apply rotations in World space (for consistent rotations)
        //negative yRot feels more intuitive
        transform.Rotate(xRot, -yRot, 0, Space.World);
    }

    private void HandleVerticalMovement()
    {
        // Gets the vertical hand delta
        float vDelta = Input.GetAxis("Mouse Y") * horizontalSpeed * Time.fixedDeltaTime;

        // Moves the hand vertically
        body.velocity += new Vector3(0, vDelta, 0);
    }
}
