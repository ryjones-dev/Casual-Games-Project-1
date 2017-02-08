using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;

public class MouseRotation : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {

        float scaleFactor = 5.0f; //change to increase mouse sensitivity

        //scale rotations based on screen width and screen height
        float wScale = 1.0f / Screen.width * 360 * scaleFactor;
        float hScale = 1.0f / Screen.height * 360 * scaleFactor;

        //get mouse movements from last frame
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        //calculate rotaion angles around X and Y axises
        float xRot = mouseDelta.y * wScale;
        float yRot = mouseDelta.x * hScale;

        //apply rotations in World space (for consistent rotations)
        //negative yRot feels more intuitive
        transform.Rotate(xRot, -yRot, 0,Space.World);
    }
}
