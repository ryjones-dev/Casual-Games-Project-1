using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Hand : MonoBehaviour
{
    private GameObject persistent;
    private OptionsScript optionsScript;

    public float horizontalSpeed = 10;
    public float verticalSpeed = 10;
    public float maxHeight = 5;
    public float minHeight = 0;
    public float rotationSensitivity = 5.0f; //change to increase mouse sensitivity

    private Rigidbody body;

    public float xRotationDelta = 0.0f; //total amount rotated along xAxis sense script started
    public float yRotationDelta = 0.0f; //total amount rotated along yAxis sense script started     NOTE: These two varaibles must be updated when roations are applied to the x or y axis of an object using this script
    const float ROTATION_LIMIT = 45.0f;

    private void Start()
    {
        // Hides the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        body = GetComponent<Rigidbody>();

        //store persistent and options script
        persistent = GameObject.Find("Persistent");
        GameObject optionsMenu = persistent.transform.Find("OptionsMenu").gameObject;
        optionsScript = optionsMenu.GetComponent<OptionsScript>();
    }

    private void Update()
    {
        if (!GameManager.Paused) //do not register player input when game is paused
        {
            //handle options menu settings
            horizontalSpeed = optionsScript.mouseSensitivity * 10;
            verticalSpeed = optionsScript.mouseSensitivity * 10;
            if (optionsScript.mouseMovementInverted)
            {
                horizontalSpeed *= -1;
                verticalSpeed *= -1;
            }

            rotationSensitivity = optionsScript.mouseSensitivity * 5.0f;
            if (optionsScript.mouseRotationInverted)
            {
                rotationSensitivity *= -1;
            }

            // Handle rotation when holding RMB, or handle moving otherwise
            if (Input.GetButton("Fire2"))
            {
                HandleRotation();
            }
            else if (Input.GetMouseButtonDown(2))
            {
                ResetRotation();
            }
            else
            {
                HandleMovement();
            }

            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, minHeight, maxHeight), transform.position.z);
        }
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
        Vector2 mouseDelta = new Vector2(optionsScript.mouseSensitivity * Input.GetAxis("Mouse X"), optionsScript.mouseSensitivity * Input.GetAxis("Mouse Y"));

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
}
