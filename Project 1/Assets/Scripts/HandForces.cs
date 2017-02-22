using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HandForces : MonoBehaviour
{
    public float springForce = 10.0f;
    public float verticalSpeed = 5f;
    public float rotationSensitivity = 5.0f; //change to increase mouse sensitivity

    public float maxHeight = 5;
    private Vector3 targetPosition;

    private Vector2 prevMouseScreenPos;
    private GameObject hand;
    Rigidbody body;
    //test
    private void Start()
    {
        // Creates a sphere to represent the hand (delete this later when we have a hand model)
        hand = this.gameObject;
        body = GetComponent<Rigidbody>();

        // Starts the hand at the max height
        targetPosition.y = maxHeight;
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
        else
        {
            HandleHorizontalMovement();
        }
    }
    void FixedUpdate()
    {
        var dir = (targetPosition - hand.transform.position).normalized;
        var distance = (hand.transform.position - targetPosition).sqrMagnitude;
        body.AddForce(dir * distance * springForce * Time.fixedDeltaTime);

    }
    private void HandleHorizontalMovement()
    {
        // Gets a ray from the camera's position to the mouse's world position
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(mouseRay, out hit))
        {
            // Gets what the ray hits, and sets the hand's horizontal position to that hit point
            targetPosition = new Vector3(hit.point.x, targetPosition.y, hit.point.z);
        }
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
        // Gets the mouse delta
        Vector2 mouseDelta = (Vector2)Input.mousePosition - prevMouseScreenPos;

        // Mouse has moved up
        if (mouseDelta.y < 0)
        {
            // Move the hand up
            targetPosition.y -= verticalSpeed * Time.deltaTime;
        }
        else if (mouseDelta.y > 0) // Mouse has moved down
        {
            // Move the hand down
            targetPosition.y += verticalSpeed * Time.deltaTime;
        }

        // Clamps the hand height between 0 and the max height
        targetPosition.y = Mathf.Clamp(targetPosition.y, 0, maxHeight);

        // Saves the mouse position as the previous mouse position for next update
        prevMouseScreenPos = Input.mousePosition;
    }
}
