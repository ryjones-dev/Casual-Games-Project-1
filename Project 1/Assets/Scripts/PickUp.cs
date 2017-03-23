using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour {
    public GameObject hand;
    public GameObject defaultModel;
    public GameObject gripModel;
    public GameObject handBase;
    private bool objectInHand = false;
    private GameObject heldObject;
    private const int PICK_UP_COOLDOWN = 20;
    private int currentCooldDown = 0;
    private bool onCooldown = false;
    private Renderer defaultRenderer;
    private Renderer gripRenderer;
    private bool isHookedToPivot = false;
    private float rotationSpeed = 100.0f;
    private GameObject pivotObjectHeld;


    public AudioClip pickupSound;
    public AudioClip dropSound;
    private OptionsScript options;
    private AudioSource audio;

    Transform m_heldObjectParent;

    public bool HoldingObject { get { return objectInHand; } }

    // Use this for initialization
    void Start () {
        defaultRenderer = defaultModel.GetComponent<Renderer>();
        gripRenderer = gripModel.GetComponent<Renderer>();
        gripRenderer.enabled = false;

        options = GameObject.Find("OptionsMenu").GetComponent<OptionsScript>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Update()
    {
        if (GameSettings.STATE == GameSettings.GAME_STATE.PAUSED || GameSettings.STATE == GameSettings.GAME_STATE.FROZEN) return;

        if (isHookedToPivot)
        {
            pivotOnMovement();
        }

        if (!onCooldown )
        { 
            if (Input.GetButtonDown("Fire1"))
            {
                if (heldObject != null)
                {
                    dropObject(heldObject);
                    onCooldown = true;
                }

                if(pivotObjectHeld != null)
                {
                    unHookFromPivot(pivotObjectHeld);
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

            if(other.gameObject.tag == "Pivotable")
            {
                hookToPivot(other.gameObject);
            }
        }
    }

    private void pivotOnMovement()
    {
        float rotation = Input.GetAxis("Vertical") * rotationSpeed;
        rotation *= Time.deltaTime;
        Rigidbody body = pivotObjectHeld.GetComponent<Rigidbody>();
        if (body == null)
        {
            Debug.Log("Body couldn't be found fail " + pivotObjectHeld.gameObject.name );
            return;
        }
        body.AddForce(new Vector3(0,0, rotation) , ForceMode.Impulse);
        //pivotObjectHeld.transform.Rotate(rotation, 0, 0);
    }

    private void hookToPivot(GameObject obj)
    {
        isHookedToPivot = true;
        pivotObjectHeld = obj;
        handBase.GetComponent<Hand>().enabled = false;
        handBase.transform.parent = pivotObjectHeld.transform;

        defaultRenderer.enabled = false;
        gripRenderer.enabled = true;

        defaultRenderer.material.color = new Color(0.75f, 0.25f, 0.25f, 0.05f);
        gripRenderer.material.color = new Color(0.75f, 0.25f, 0.25f, 0.05f);
    }

    private void unHookFromPivot(GameObject obj)
    {
        isHookedToPivot = false;
        pivotObjectHeld = null;
        handBase.GetComponent<Hand>().enabled = true;
        handBase.transform.parent = null;

        defaultRenderer.enabled = true;
        gripRenderer.enabled = false;

        defaultRenderer.material.color = new Color(0.5f, 0.5f, 0.5f, 0.05f);
        gripRenderer.material.color = new Color(0.5f, 0.5f, 0.5f, 0.05f);
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
        Physics.IgnoreCollision(handBase.GetComponent<Collider>(), obj.GetComponent<Collider>());

        GameObject.Destroy(obj.GetComponent<Rigidbody>());

        defaultRenderer.enabled = false;
        gripRenderer.enabled = true;

        defaultRenderer.material.color = new Color(0.75f, 0.25f, 0.25f, 0.05f);
        gripRenderer.material.color = new Color(0.75f, 0.25f, 0.25f, 0.05f);
        
        audio.PlayOneShot(pickupSound, options.soundEffectVolume);
    }
    void dropObject(GameObject toDrop){
        toDrop.transform.parent = null;
        toDrop.transform.parent = m_heldObjectParent;
        toDrop.AddComponent<Rigidbody>();
        objectInHand = false;
        heldObject = null;
        Physics.IgnoreCollision(handBase.GetComponent<Collider>(), toDrop.GetComponent<Collider>(), false);

        defaultRenderer.enabled = true;
        gripRenderer.enabled = false;

        defaultRenderer.material.color = new Color(0.5f, 0.5f, 0.5f, 0.05f);
        gripRenderer.material.color = new Color(0.5f, 0.5f, 0.5f, 0.05f);
        
        audio.PlayOneShot(dropSound, options.soundEffectVolume);
    }
}
