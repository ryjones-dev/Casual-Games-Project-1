using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsScript : MonoBehaviour {

    Canvas optionsMenu;

	// Use this for initialization
	void Start () {

        optionsMenu = GameObject.Find("OptionsMenu").GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("escape"))
        {
            optionsMenu.enabled = !optionsMenu.enabled;
        }
    }
}
