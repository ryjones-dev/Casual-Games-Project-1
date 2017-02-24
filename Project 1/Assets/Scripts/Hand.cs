using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Hand : MonoBehaviour
{
    public float horizontalSpeed = 10;
    public float verticalSpeed = 10;
    public float maxHeight = 5;
    public float rotationSensitivity = 5.0f; //change to increase mouse sensitivity
    
    Rigidbody body;

    public float xRotationDelta = 0.0f; //total amount rotated along xAxis sense script started
    public float yRotationDelta = 0.0f; //total amount rotated along yAxis sense script started     NOTE: These two varaibles must be updated when roations are applied to the x or y axis of an object using this script
    const float ROTATION_LIMIT = 45.0f;

    private void Start()
    {
        // Hides the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Move vertically when holding LMB, handle rotation when holding RMB, or move horizontally otherwise
        if (Input.GetButton("Fire1"))
        {
            HandleVerticalMovement();
        }
        else if (Input.GetButton("Fire2"))
        {
            HandleRotation();
        }
        else if (Input.GetMouseButtonDown(2))
        {
            ResetRotation();
        }
        else
        {
            HandleHorizontalMovement();
        }

        // Clamps the hand's position between the min and max height (This is done in update intentionally so the clamp actually works)
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 0, maxHeight), transform.position.z);
    }

    private void ResetRotation()
    {
        transform.rotation = Quaternion.Euler(0, 90, 0);
        xRotationDelta = 0.0f;
        yRotationDelta = 0.0f;
    }

 /*   private void limitRotation(float xRot, float yRot)
    {
        this.transform.Rotate(-xRot, yRot, 0, Space.World);  //inverse rotation -x +y
        xRotationDelta -= xRot;
        yRotationDelta -= yRot;
    } May have possible use for this as a helper function in the future. Feel free to delete if this seems useless */

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
        xRotationDelta += xRot;
        yRotationDelta += yRot;

        //apply rotations in World space (for consistent rotations)
        //negative yRot feels more intuitive
        transform.Rotate(xRot, -yRot, 0, Space.World);

        if(xRotationDelta > ROTATION_LIMIT || yRotationDelta > ROTATION_LIMIT || xRotationDelta < -ROTATION_LIMIT || yRotationDelta < -ROTATION_LIMIT)
        {
            this.transform.Rotate(-xRot, yRot, 0, Space.World);  //inverse rotation -x +y
            xRotationDelta -= xRot;
            yRotationDelta -= yRot;
        }
    }

    private void HandleVerticalMovement()
    {
        // Gets the vertical hand delta
        float vDelta = Input.GetAxis("Mouse Y") * horizontalSpeed * Time.fixedDeltaTime;

        // Moves the hand vertically
        body.velocity += new Vector3(0, vDelta, 0);
    }
}
