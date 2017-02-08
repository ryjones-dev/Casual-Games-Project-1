using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Hand : MonoBehaviour
{
    public float horizontalSpeed = 5;
    public float verticalSpeed = 10;
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
        // Move vertically when holding LMB, or horizontally otherwise
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
    }

    void FixedUpdate()
    {
        float hMouseDelta = Input.GetAxis("Mouse X");
        float vMouseDelta = Input.GetAxis("Mouse Y");

        Vector3 dir = (targetPosition - transform.position).normalized;
        //float distance = (transform.position - targetPosition).magnitude;

        Vector3 speed = dir;
        speed.x *= horizontalSpeed;
        speed.y *= verticalSpeed;
        speed.z *= horizontalSpeed;

        body.velocity = speed * Time.fixedDeltaTime;

    }

    private void HandleHorizontalMovement()
    {
        targetPosition.x += Input.GetAxis("Mouse X");
        targetPosition.z += Input.GetAxis("Mouse Y");
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
        // Gets the vertical mouse delta
        float vMouseDelta = Input.GetAxis("Mouse Y");

        // Mouse has moved down
        if (vMouseDelta < 0)
        {
            // Move the hand down
            targetPosition.y -= verticalSpeed * Time.deltaTime;
        }
        else if (vMouseDelta > 0) // Mouse has moved up
        {
            // Move the hand up
            targetPosition.y += verticalSpeed * Time.deltaTime;
        }

        // Clamps the hand height between 0 and the max height
        targetPosition.y = Mathf.Clamp(targetPosition.y, 0, maxHeight);
    }
}
