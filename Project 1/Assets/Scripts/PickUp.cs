using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {
    public GameObject hand;
    private bool objectInHand = false;
    private GameObject heldObject;
    private const int PICK_UP_COOLDOWN = 20;
    private int currentCooldDown = 0;
    private bool onCooldown = false;

    Transform m_heldObjectParent;

    // Use this for initialization
    void Start () {
    }

    // Update is called once per frame
    void Update()
    {
        if (!onCooldown)
        { 
            if (Input.GetButtonDown("Fire1"))
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

    private void OnTriggerEnter(Collider other)
    {
        if (!onCooldown)
        {
            if (objectInHand)
            {
                return;
            }

            if (other.gameObject.tag == "Interactable")
            {
                pickUp(other.gameObject);
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

    void pickUp(GameObject obj)
    {
        m_heldObjectParent = obj.transform.parent;
        obj.transform.parent = hand.transform;
        objectInHand = true;
        heldObject = obj.gameObject;

        GameObject.Destroy(obj.GetComponent<Rigidbody>());
    }
    void dropObject(GameObject toDrop){
        toDrop.transform.parent = null;
        toDrop.transform.parent = m_heldObjectParent;
        toDrop.AddComponent<Rigidbody>();
        objectInHand = false;
        heldObject = null;
    }
}
