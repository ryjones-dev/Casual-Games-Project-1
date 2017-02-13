using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountItems : MonoBehaviour {

    private List<GameObject> items;
    //private GameObject[] items; //array of game items to check against bounded area
    private float[] itemsContained; //floats identifying how long an object has been within bounded area (may want to set wait time before incrementing score)

    float width = 1.0f;
    float height = 1.0f;

    int score;

	// Use this for initialization
	void Start () {
        items = new List<GameObject>();
        //items = GameObject.FindGameObjectsWithTag("Item");
        //itemsContained = new float[items.Length];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider o)
    {
        if (o.gameObject.tag == "Item")
        {
            items.Add(o.gameObject);
        }
    }

    void OnTriggerExit(Collider o)
    {
        if (o.gameObject.tag == "Item")
        {
            items.Remove(o.gameObject);
        }
    }
}
