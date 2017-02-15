using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {
    public GameObject hand;
    private bool objectInHand = false;
    private Collision heldObject;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (objectInHand) {
            return;
        }
        
        if (collision.gameObject.tag == "intractable"){
            collision.gameObject.transform.parent = hand.transform;
            objectInHand = true;
            heldObject = collision;
        }
    }

    //revist this and actually listen for input once we decide upon a key to bind the action of droping an object too
    private void OnPlayerInput(){
        if(heldObject == null || !objectInHand){
            return;
        }  //not sure if "or objectInHand" check is needed.  Can't hurt though, and statement will SC out anyway

        dropObject(heldObject);
        heldObject = null;
    }

    private void dropObject(Collision toDrop){
        toDrop.gameObject.transform.parent = null;
        objectInHand = false;
        heldObject = null;
    }

}
