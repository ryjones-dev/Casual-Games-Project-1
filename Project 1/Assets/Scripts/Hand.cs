using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Hand : MonoBehaviour
{
    public float horizontalSpeed = 10;
    public float verticalSpeed = 10;
    public float maxHeight = 5;
    public float minHeight = 0;
    public float rotationSensitivity = 5.0f; //change to increase mouse sensitivity

    private Rigidbody body;

    private void Start()
    {
        // Hides the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Handle rotation when holding RMB, or handle moving otherwise
        if (Input.GetButton("Fire2"))
        {
            HandleRotation();
        }
        else
        {
            HandleMovement();
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, minHeight, maxHeight), transform.position.z);
    }

    private void HandleMovement()
    {
        body.velocity += new Vector3(Input.GetAxis("Mouse X") * horizontalSpeed * Time.deltaTime, Input.mouseScrollDelta.y * verticalSpeed * Time.deltaTime, Input.GetAxis("Mouse Y") * horizontalSpeed * Time.deltaTime);
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
}
