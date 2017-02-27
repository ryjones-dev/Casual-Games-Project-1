using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private Canvas optionsMenu;


	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);

        GameObject optionsObject = this.transform.Find("OptionsMenu").gameObject;
        optionsMenu = optionsObject.GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update () {
		if(SceneManager.GetActiveScene().name == "Title"){
            optionsMenu.enabled = false;
        }
        else
        {
            optionsMenu.enabled = true;
        }
	}
}
