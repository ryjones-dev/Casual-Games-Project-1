using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    private Canvas optionsMenu;
    private OptionsScript options;

    public bool gamePaused;

	// Use this for initialization
	void Start () {
        if (instance == null)
        {
            DontDestroyOnLoad(this);

            GameObject optionsObject = this.transform.Find("OptionsMenu").gameObject;
            optionsMenu = optionsObject.GetComponent<Canvas>();
            options = optionsMenu.GetComponent<OptionsScript>();
            instance = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
        gamePaused = options.gamePaused;
		if(SceneManager.GetActiveScene().name == "Title"){
            optionsMenu.enabled = false;
        }
        else
        {
            optionsMenu.enabled = true;
        }
	}
}
