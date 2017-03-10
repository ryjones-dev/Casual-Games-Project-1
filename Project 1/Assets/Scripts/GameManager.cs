using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    private Canvas optionsMenu;
    private OptionsScript options;
    
    private bool gamePaused = false;
    public static bool Paused { get { return instance.gamePaused; } }
    

	// Use this for initialization
	void Start () {
        if (instance == null)
        {
            DontDestroyOnLoad(this);

            GameObject optionsObject = this.transform.Find("OptionsMenu").gameObject;
            optionsMenu = optionsObject.GetComponent<Canvas>();
            options = optionsMenu.GetComponent<OptionsScript>();
            instance = this;
        }else
        {
            Destroy(gameObject);
        }
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

    public static void Pause()
    {
        instance.gamePaused = true;
        Debug.Log("Game paused");
    }

    public static void Unpause()
    {
        instance.gamePaused = false;
        Debug.Log("Game unpaused");
    }
}
