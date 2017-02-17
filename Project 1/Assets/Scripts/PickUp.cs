using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {
    public GameObject hand;
    private bool objectInHand = false;
    private Collision heldObject;
    private const int PICK_UP_COOLDOWN = 20;
    private int currentCooldDown = 0;
    private bool onCooldown = false;

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update()
    {
        if (!onCooldown)
        { 
            if (Input.GetKeyDown("space"))
            {
                if (heldObject != null)
                {
                    dropObject(heldObject);
                    onCooldown = true;
                }
            }
        }
        else
        {
            currentCooldDown++;
            if(currentCooldDown > PICK_UP_COOLDOWN)
            {
                currentCooldDown = 0;
                onCooldown = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!onCooldown)
        {
            if (objectInHand)
            {
                return;
            }

            if (collision.gameObject.tag == "intractable")
            {
                collision.gameObject.transform.parent = hand.transform;
                objectInHand = true;
                heldObject = collision;
                collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                //collision.gameObject.GetComponent<Rigidbody>().useGravity = false;
                //collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                //rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            }
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
        toDrop.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        objectInHand = false;
        heldObject = null;
    }
}
